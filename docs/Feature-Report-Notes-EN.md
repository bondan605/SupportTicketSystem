# Report Feature — Design Notes (EN)

Internal notes documenting what was designed and built today for the **Manager Report**
feature, so teammates can understand the design decisions without re-deriving them.

---

## 1. What This Feature Is

An analytics endpoint for Managers, returning 11 report components in a single response:
ticket overview cards, status/category/priority breakdowns, trends, average response time,
and SLA compliance. Built with DTO → Repository → Service (with caching) → Controller
layering.

## 2. Architecture

```
Controllers/
  ReportController.cs        # thin HTTP layer, delegates to the service
  DevDataController.cs       # dev-only: clear/seed/reset demo ticket data

Application/
  DTOs/Reports/               # 1 DTO per report component + root ReportSummaryDto
  Validators/Reports/         # FluentValidation rules for the date-range filter
  Services/Reports/           # orchestrates repository calls, applies caching

Infrastructure/
  Repositories/Reports/       # 1 EF Core aggregate query per component
  Persistence/Seeding/        # bulk demo data generator (Tickets + TicketHistories)
```

**Key structural decisions:**
- DTOs are split **per component**, not one flat object, so the frontend can consume and
  evolve each chart/card independently.
- **One repository with many methods** — not one service class per component — keeps the
  structure lean without over-engineering for a feature with ~11 read-only queries.
- Queries are **awaited sequentially** in the service layer, not run concurrently via
  `Task.WhenAll`. A scoped `DbContext` is not thread-safe and cannot execute multiple
  operations concurrently on the same instance — running these queries in parallel would
  require a separate `DbContext` per query (e.g. via `IDbContextFactory`), which isn't
  justified for this feature's query volume/latency profile.
- Aggregation happens **in the database** (`GroupBy().Select(...)`), not in memory.

## 3. Report Components → Data Source

| # | Component | Filtered by | Key logic |
|---|-----------|-------------|-----------|
| 1 | Ticket Overview (cards) | `CreatedAt` | Counts per `TicketStatus` (Open/InProgress/Resolved/Closed — no "Cancelled" exists in this domain) + active user count |
| 2 | Tickets per Status | `CreatedAt` | Count + % per `TicketStatus` |
| 3 | Tickets Trend | `CreatedAt` (created) / `ClosedAt` (closed) | Per-day series, each metric queried independently |
| 4 | Tickets per Assignee | `CreatedAt` | Top 6 assignees by count + "Unassigned" bucket |
| 5 | Tickets per Category | `CreatedAt` | Count + % per `TicketCategory` |
| 6 | Tickets per Priority | `CreatedAt` | Count + % per `TicketPriority` |
| 7 | Average Response Time | `CreatedAt` | Avg. minutes between `Ticket.CreatedAt` and the first `TicketHistory` entry where `Action == StatusChanged` into `InProgress` |
| 8 | SLA Compliance | `ClosedAt` | % of closed tickets where `ClosedAt <= EstimatedDueDate`. Tickets without `EstimatedDueDate` are excluded |
| 9 | Tickets by Category (table) | `CreatedAt` | Same source as #5, tabular shape |
| 10 | Recent Closed Tickets | `ClosedAt` | Latest closed tickets; "Closed By" = the ticket's `Assignee.Name` |
| 11 | SLA Compliance Trend | `ClosedAt` | Per-day series with **both** daily and cumulative compliance % — pending confirmation on which one the UI should use |

## 4. Business Rules & Design Decisions Made Today

- **No "Cancelled" status.** The reference mockup showed one, but `TicketStatus` only has
  `Open`, `InProgress`, `Resolved`, `Closed`. All status-based components use these 4 values.
- **"Tickets Selesai" (top card) = `Closed` count only** (not `Resolved`).
- **SLA target = `Ticket.EstimatedDueDate`**, an existing per-ticket field — not a fixed
  policy per Priority level.
- **Average Response Time ≠ resolution time.** It measures time-to-first-action (Open →
  InProgress via `TicketHistory`), not time-to-close.
- **Validation lives in the service layer**, using `ValidateAndThrowAsync` (FluentValidation),
  not in the controller. The controller has no manual `if` validation checks.
- **Bug fixed today:** the SLA/date validator originally compared `EndDate` (which is
  extended to `23:59:59.999` for inclusive-range queries) directly against `DateTime.Now`,
  causing a false "endDate cannot be in the future" error even when no future date was
  requested. Fixed by comparing `.Date` against `DateTime.Today` instead of comparing exact
  instants.
- **Bug fixed today:** `AppDbContext.ApplyAuditInformation()` was overwriting any manually
  set `CreatedAt` with `DateTime.UtcNow` on every `SaveChanges`, which would have destroyed
  the historical timestamps used for demo seeding. Fixed by adding an `IsSeeding` bypass flag
  on `AppDbContext`.

## 5. Caching

`ReportService` caches the full `ReportSummaryDto` per date-range for **5 minutes** using
`IMemoryCache`, keyed by `report-summary:{startDate}:{endDate}`.

**Limitation to be aware of:** `IMemoryCache` is in-process — fine for a single-instance
deployment, but won't stay consistent across multiple app instances behind a load balancer.
Would need `IDistributedCache` (e.g. Redis) for that scenario. Report data may lag up to 5
minutes behind the latest ticket changes; this is an accepted trade-off, not a bug.

## 6. DevDataController — Purpose & Endpoints

A development-only utility controller for generating/clearing demo ticket data, since real
production data doesn't exist yet and the report needs volume to be meaningful (trends, SLA
%, per-assignee distribution, etc. look meaningless with only a handful of tickets).

**Why a separate bulk seeder instead of EF Core `HasData`:** `HasData` (used for `Users`)
requires fully static values baked into migrations, which doesn't work for hundreds of
randomly-distributed rows spanning 45 days. `ReportDemoDataSeeder` generates that volume
programmatically instead, called on-demand via these endpoints rather than at startup.

| Method | Endpoint | Description |
|--------|----------|--------------|
| POST | `/api/dev/report-data/seed` | Generates ~45 days of demo Tickets + TicketHistories. Skipped (no-op) if the DB already has more than 20 tickets — call `/clear` first to force a fresh reseed. |
| DELETE | `/api/dev/report-data/clear` | Deletes all `Tickets` and `TicketHistories`. **`Users` are never touched** — they're managed via migration seed data, and removing them would break `AssignedTo`/`ChangedBy` foreign keys on new tickets. |
| POST | `/api/dev/report-data/reset` | Convenience wrapper: `clear` followed immediately by `seed`, in one call. |

**Safety:** every action in this controller checks `IWebHostEnvironment.IsDevelopment()`
first and returns `403 Forbidden` otherwise — this must never be reachable in
Staging/Production.

**What the generated data looks like:** ~180 tickets over 45 days, with status distribution
weighted by ticket age (older tickets skew toward `Closed`, newer ones skew toward
`Open`/`InProgress`), full `TicketHistory` lifecycles (Created → InProgress → Resolved →
Closed) with randomized realistic time gaps, and roughly 85% of closed tickets meeting their
SLA target (the other 15% intentionally close late) — so SLA Compliance shows a realistic
~85–90% instead of a meaningless flat 100%.

## 7. Sample API Calls

**Seed demo data:**
```bash
curl -X POST https://localhost:5001/api/dev/report-data/seed
```

**Clear demo data:**
```bash
curl -X DELETE https://localhost:5001/api/dev/report-data/clear
```

**Get report summary:**
```bash
curl -X GET "https://localhost:5001/api/reports/summary?startDate=2026-06-26&endDate=2026-07-24" \
  -H "Authorization: Bearer {your_jwt_token}"
```

## 8. Still Pending / Not Decided Yet

- `ChangePercent` fields (previous-period comparison) in `TicketOverviewDto`,
  `AverageResponseTimeDto`, and `SlaComplianceDto` currently return `null` — implementation
  deferred pending a scope decision (revisit once all 11 components are confirmed working
  end-to-end).
- `SlaComplianceTrendPointDto` exposes both `DailyCompliancePercentage` and
  `CumulativeCompliancePercentage` — waiting on confirmation from senior on which
  interpretation the frontend should actually render.
