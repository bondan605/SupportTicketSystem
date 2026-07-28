# Customer Support Ticket Management System

A simple web application for managing customer support tickets, built as a Lead Programmer assessment case study. It replaces email-based issue tracking with a structured system that gives Support Agents and Managers clear visibility into ticket status and workload.

## Overview

- **Backend:** ASP.NET Core Web API (.NET 8)
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core (Code-First Migrations)
- **Authentication:** JWT Bearer Token
- **Roles:** `Manager`, `SupportAgent`
- **UI:** Blazor Server (MudBlazor)

## Features Implemented

- JWT-based authentication and login (`/api/auth/login`)
- Role-based access control (`Manager` / `SupportAgent`)
- Ticket listing with filtering (status, assignee, priority, category, search) and paging
- Ticket creation and update, with status-flow business rules enforced
- Ticket history tracking (audit trail of status/assignee/priority changes)
- Manager-only ticket report endpoint
- Dashboard summary report (ticket counts by status + weekly trend)

## Tech Stack

| Layer          | Technology                              |
|----------------|-------------------------------------------|
| Runtime        | .NET 8 (LTS)                              |
| API Framework  | ASP.NET Core Web API 8                    |
| UI             | Blazor Server + MudBlazor 9.5.0           |
| Database       | Microsoft SQL Server                      |
| ORM            | Entity Framework Core                     |
| Validation     | FluentValidation                          |
| Mapping        | AutoMapper                                |
| Auth           | JWT Bearer Authentication (BCrypt hashing)|
| API Docs       | Swagger / OpenAPI                         |

## Business Rules & Ticket Status Flow

Tickets move through 4 statuses: `Open` → `In Progress` → `Resolved` / `Closed`.

| Status        | Meaning                                                        |
|---------------|------------------------------------------------------------------|
| Open          | Just created, not yet assigned to any agent.                    |
| In Progress   | Assigned to an agent and currently being worked on.              |
| Resolved      | Issue has been resolved by the assigned agent.                   |
| Closed        | Officially closed; fully locked from further changes.            |

Key rules:

- A new ticket always starts as `Open`.
- A **Manager** assigns an agent to an `Open` ticket, which automatically moves it to `In Progress`.
- While `In Progress`, a **Manager** can reassign the ticket to a different agent.
- Only the **currently assigned agent** on an `In Progress` ticket can change its status to `Resolved` or `Closed`.
- Once a ticket becomes `Resolved` or `Closed`, the **Manager** can no longer make any changes (including reassignment).
- Once a ticket becomes `Closed`, **no role** (Agent or Manager) can modify it — view only.
- Ticket detail view is always available to both roles, at any status.
- On the **Ticket List**, a Support Agent only sees tickets they created or are assigned to; a Manager sees all tickets without restriction.
- Ticket numbers are auto-generated in the format `TKT-00001` and must be unique.
- Customer email must pass validation before a ticket is created.
- Tickets can only be assigned to existing, active users.

## Project Structure

Solution: `SupportTicketSystem.sln` — 8 projects following a layered/clean-architecture split:

```
SupportTicketSystem/
├── src/
│   ├── 01.Base/                  # SupportTicketSystem.Base
│   │   └── Entities/               #   BaseEntity (Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
│   │
│   ├── 02.Domain/                # SupportTicketSystem.Domain
│   │   ├── Entities/               #   User, Ticket, TicketHistory
│   │   └── Enums/                  #   UserRole, TicketStatus, TicketType, TicketCategory,
│   │                               #   TicketImpact, TicketPriority, TicketApplication,
│   │                               #   TicketHistoryAction
│   │
│   ├── 03.Shared/                # SupportTicketSystem.Shared
│   │   ├── Constants/              #   ApiRoutes
│   │   ├── DTOs/                   #   Auth/, Tickets/, Users/, Dashboard/, Reports/
│   │   ├── Exceptions/             #   BusinessException, NotFoundException
│   │   ├── Extensions/             #   ClaimsPrincipalExtensions (GetUserId/GetRole)
│   │   └── Models/                 #   ApiResponse<T>, PagedRequest, PagedResult<T>
│   │
│   ├── 04.Application/           # SupportTicketSystem.Application
│   │   ├── Interfaces/              #   Abstractions for Services + Repositories
│   │   ├── Mappings/                #   AutoMapper profiles (Ticket, User, TicketHistory)
│   │   ├── Services/                #   AuthService, TicketService, UserService,
│   │   │                            #   DashboardService, TicketHistoryService, ReportService
│   │   └── Validators/              #   FluentValidation validators
│   │
│   ├── 05.Infrastructure/        # SupportTicketSystem.Infrastructure
│   │   ├── Migrations/              #   EF Core migrations + AppDbContextModelSnapshot
│   │   └── Persistence/             #   AppDbContext, Configurations/ (Fluent API),
│   │                                #   Repositories/, Seeder/
│   │
│   ├── 06.WebApi/                # SupportTicketSystem.WebApi (ASP.NET Core Web API, JWT auth)
│   │   ├── Controllers/             #   AuthController, TicketsController, UserController,
│   │   │                            #   TicketHistoriesController, DashboardController,
│   │   │                            #   ReportController, DevController
│   │   ├── Extensions/              #   SwaggerExtensions
│   │   ├── Middleware/
│   │   └── Properties/
│   │
│   ├── 07.Client/                # SupportTicketSystem.Client
│   │   └── Features/                #   Typed HttpClient wrappers (TicketClient, UserClient,
│   │                                #   AuthClient, DashboardClient, TicketHistoryClient, ...)
│   │       (+ JwtForwardingHandler, SessionExpiryHandler/Notifier at project root)
│   │
│   └── 08.Bsui/                  # SupportTicketSystem.Bsui (Blazor Server UI, MudBlazor)
│       ├── Components/              #   Pages/, Layout/, Shared/, Dialogs/, Models/
│       ├── Constants/               #   AppRoutes
│       ├── Extensions/
│       ├── Properties/
│       ├── Services/                #   ServerJwtAccessor
│       └── wwwroot/                 #   js/, css/
│
├── docs/                         # Feature notes / reference docs (Markdown)
├── SupportTicketSystem.sln
└── global.json
```

**Layer dependency direction:** `08.Bsui` → `07.Client` → `03.Shared`; `06.WebApi` → `04.Application` → `02.Domain` → `01.Base`; `05.Infrastructure` implements `04.Application`'s repository interfaces and is wired up only in `06.WebApi`'s composition root (`Program.cs`).

## Database Design

All three tables inherit four audit columns from `BaseEntity`: `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy` (populated automatically, not enforced foreign keys).

### Users

| Column | Type | Nullable | Constraints |
|---|---|---|---|
| Id | `Guid` (uniqueidentifier) | No | **PK** |
| Name | `string` (nvarchar(150)) | No | Required |
| Username | `string` (nvarchar(50)) | No | Required, unique |
| Email | `string` (nvarchar(150)) | No | Required, unique |
| PasswordHash | `string` (nvarchar(max)) | No | BCrypt hash |
| Role | `UserRole` (nvarchar(30)) | No | `SupportAgent` \| `Manager` |
| PhoneNumber | `string?` (nvarchar(20)) | Yes | — |
| BirthDate | `DateTime?` (datetime2) | Yes | — |
| JobTitle | `string?` (nvarchar(100)) | Yes | — |
| Address | `string?` (nvarchar(500)) | Yes | — |
| IsActive | `bool` (bit) | No | Default `true` |
| AvatarUrl | `string?` (nvarchar(500)) | Yes | — |
| LastLoginAt | `DateTime?` (datetime2) | Yes | — |
| CreatedAt / CreatedBy / UpdatedAt / UpdatedBy | — | — | Audit (BaseEntity) |

### Tickets

| Column | Type | Nullable | Constraints |
|---|---|---|---|
| Id | `Guid` (uniqueidentifier) | No | **PK** |
| TicketNumber | `string` (nvarchar(20)) | No | Unique, format `TKT-00001` |
| CustomerName | `string` (nvarchar(150)) | No | Required |
| CustomerEmail | `string` (nvarchar(150)) | No | Required |
| CustomerPhone | `string?` (nvarchar(20)) | Yes | — |
| Title | `string` (nvarchar(255)) | No | Required |
| Description | `string` (nvarchar(max)) | No | Required |
| Status | `TicketStatus` (nvarchar(30)) | No | `Open` \| `InProgress` \| `Resolved` \| `Closed` |
| Priority | `TicketPriority` (nvarchar(30)) | No | `Low` \| `Medium` \| `High` |
| Type | `TicketType` (nvarchar(30)) | No | `Incident` \| `ServiceRequest` \| `Problem` \| `ChangeRequest` |
| Category | `TicketCategory` (nvarchar(30)) | No | `Application` \| `Access` \| `Report` \| `Hardware` \| `Other` |
| Impact | `TicketImpact` (nvarchar(30)) | No | `SingleUser` \| `SomeUsers` \| `AllUsers` |
| Application | `TicketApplication` (nvarchar(30)) | No | `None` \| `CRM` \| `ERP` \| `HRIS` \| `Email` \| `FileServer` \| `Website` \| `InternalPortal` \| `Other` |
| AssignedTo | `Guid?` (uniqueidentifier) | Yes | **FK → Users.Id**, `ON DELETE RESTRICT`, indexed |
| EstimatedDueDate | `DateTime?` (datetime2) | Yes | — |
| ClosedAt | `DateTime?` (datetime2) | Yes | — |
| CreatedAt / CreatedBy / UpdatedAt / UpdatedBy | — | — | Audit (BaseEntity) |

### TicketHistories

| Column | Type | Nullable | Constraints |
|---|---|---|---|
| Id | `Guid` (uniqueidentifier) | No | **PK** |
| TicketId | `Guid` (uniqueidentifier) | No | **FK → Tickets.Id**, `ON DELETE CASCADE`, indexed |
| Action | `TicketHistoryAction` (nvarchar(30)) | No | `TicketCreated` \| `StatusChanged` \| `AssigneeChanged` \| `PriorityChanged` \| `CommentAdded` \| `TicketUpdated` |
| OldValue | `string?` (nvarchar(255)) | Yes | — |
| NewValue | `string?` (nvarchar(255)) | Yes | — |
| Note | `string?` (nvarchar(1000)) | Yes | — |
| ChangedBy | `Guid` (uniqueidentifier) | No | **FK → Users.Id**, `ON DELETE RESTRICT`, indexed |
| Timestamp | `DateTime` (datetime2) | No | When the change actually happened |
| CreatedAt / CreatedBy / UpdatedAt / UpdatedBy | — | — | Audit (BaseEntity) |

### Relationships

| Relationship | Type | Delete behavior |
|---|---|---|
| `Ticket.AssignedTo` → `User.Id` | many-to-one, optional | Restrict |
| `TicketHistory.TicketId` → `Ticket.Id` | many-to-one, required | Cascade |
| `TicketHistory.ChangedBy` → `User.Id` | many-to-one, required | Restrict |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express, or full instance)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Clone the repository

```bash
git clone <repository-url>
cd SupportTicketSystem
```

### 2. Configure the connection string

Update `appsettings.json` (or `appsettings.Development.json`) in `src/06.WebApi`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SystemTicketManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "<your-secret-key>",
    "Issuer": "SupportTicketSystem",
    "Audience": "SupportTicketSystem",
    "ExpiryMinutes": 60
  }
}
```

### 3. Apply EF Core migrations

```bash
dotnet ef database update --project src/05.Infrastructure --startup-project src/06.WebApi
```

This creates the `SystemTicketManagementDB` database with all required tables (`Users`, `Tickets`, `TicketHistories`).

### 4. Run the application

```bash
# API
dotnet run --project src/06.WebApi

# Blazor Server UI (in a separate terminal)
dotnet run --project src/08.Bsui
```

The API will be available at `https://localhost:{port}`, with Swagger UI at `https://localhost:{port}/swagger`. The Blazor UI runs on its own port and talks to the API through `SupportTicketSystem.Client`.

## API Endpoints

All endpoints are prefixed `api/` and every response is wrapped in a standard `ApiResponse<T>` envelope (`{ success, message, data, errors }`).

| Method | Endpoint                     | Role                     | Description                                       |
|--------|-------------------------------|---------------------------|-----------------------------------------------------|
| POST   | `/api/auth/login`             | Public                    | Authenticate and receive a JWT token                |
| GET    | `/api/tickets`                | Manager, SupportAgent     | Get all tickets (scoped for SupportAgent)           |
| GET    | `/api/tickets/list`           | Manager, SupportAgent     | Paged/filtered ticket list (scoped for SupportAgent)|
| GET    | `/api/tickets/report`         | Manager                   | Ticket report with filters                           |
| GET    | `/api/tickets/{id}`           | Manager, SupportAgent     | Get ticket details                                   |
| POST   | `/api/tickets`                | Manager, SupportAgent     | Create a new ticket                                  |
| PUT    | `/api/tickets/{id}`           | Manager, SupportAgent     | Update a ticket (creator, assignee, or Manager only) |
| PUT    | `/api/tickets/{id}/assign`    | Manager                   | Assign/reassign a ticket to a user                   |
| GET    | `/api/dashboard/summary`      | Manager                   | Ticket summary + weekly trend                        |

## Sample API Calls

### 1. Login

`POST /api/auth/login` — anonymous.

Request:
```json
{
  "email": "admin@company.com",
  "password": "Demo@123"
}
```

Response `200 OK`:
```json
{
  "success": true,
  "message": "Login successful. Welcome back!",
  "data": {
    "userId": "11111111-1111-1111-1111-111111111111",
    "name": "Admin User",
    "email": "admin@company.com",
    "role": "Manager",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresAt": "2026-07-29T16:30:00Z"
  },
  "errors": []
}
```

### 2. Create Ticket

`POST /api/tickets` — Manager, SupportAgent.

Request:
```json
{
  "customerName": "Budi Hartono",
  "customerEmail": "budi.hartono@customer.com",
  "customerPhone": "0812-3456-7801",
  "title": "Login gagal pada aplikasi",
  "description": "User tidak bisa login setelah update aplikasi versi terbaru.",
  "status": "Open",
  "type": "Incident",
  "category": "Access",
  "impact": "SingleUser",
  "priority": "High",
  "application": "InternalPortal",
  "assignedTo": null,
  "estimatedDueDate": "2026-08-02"
}
```

Response `201 Created`:
```json
{
  "success": true,
  "message": "Ticket created successfully.",
  "data": {
    "id": "a1b2c3d4-0000-0000-0000-000000000001",
    "ticketNumber": "TKT-00013",
    "createdByName": "Admin User",
    "assignedToName": null,
    "customerName": "Budi Hartono",
    "customerEmail": "budi.hartono@customer.com",
    "status": "Open",
    "priority": "High",
    "assignedTo": null,
    "estimatedDueDate": "2026-08-02T00:00:00",
    "createdAt": "2026-07-29T09:10:00Z",
    "createdBy": "11111111-1111-1111-1111-111111111111"
  },
  "errors": []
}
```

### 3. Update Ticket

`PUT /api/tickets/{id}` — creator, assignee, or Manager only.

Request:
```json
{
  "title": "Login gagal pada aplikasi",
  "description": "User tidak bisa login setelah update aplikasi versi terbaru. Sudah dicoba reset password.",
  "status": "InProgress",
  "priority": "High",
  "type": "Incident",
  "category": "Access",
  "impact": "SingleUser",
  "application": "InternalPortal",
  "estimatedDueDate": "2026-08-02",
  "statusChangeNote": "Sedang diinvestigasi oleh tim akses.",
  "assignedTo": "22222222-2222-2222-2222-222222222222"
}
```

Response `200 OK`:
```json
{
  "success": true,
  "message": "Ticket updated successfully.",
  "data": null,
  "errors": []
}
```

### 4. Assign Ticket

`PUT /api/tickets/{id}/assign` — Manager only. Body is a bare `Guid`; status is automatically forced to `InProgress`.

Request:
```json
"22222222-2222-2222-2222-222222222222"
```

Response `200 OK`:
```json
{
  "success": true,
  "message": "Ticket assigned successfully.",
  "data": null,
  "errors": []
}
```

### 5. Get Ticket List

`GET /api/tickets/list?status=Open&pageNumber=1&pageSize=10` — Manager sees all tickets; SupportAgent only sees tickets they created or are assigned to.

Response `200 OK`:
```json
{
  "success": true,
  "message": "Ticket list retrieved successfully.",
  "data": {
    "items": [
      {
        "id": "a0000000-0000-0000-0000-000000000001",
        "ticketNumber": "TKT-00001",
        "createdByName": "Admin User",
        "assignedToName": "Andi Pratama",
        "customerName": "Budi Hartono",
        "status": "Open",
        "priority": "High",
        "assignedTo": "22222222-2222-2222-2222-222222222222",
        "createdAt": "2026-07-01T09:00:00Z"
      }
    ],
    "pageNumber": 1,
    "pageSize": 10,
    "totalCount": 1,
    "totalPages": 1,
    "hasPreviousPage": false,
    "hasNextPage": false
  },
  "errors": []
}
```

### 6. Dashboard Summary

`GET /api/dashboard/summary` — Manager only.

Response `200 OK`:
```json
{
  "success": true,
  "message": "Dashboard summary retrieved successfully.",
  "data": {
    "totalTickets": 42,
    "openTickets": 12,
    "inProgressTickets": 15,
    "resolvedTickets": 8,
    "closedTickets": 7,
    "unassignedTickets": 3,
    "weeklyTrends": [
      { "dayName": "Mon", "count": 5 },
      { "dayName": "Tue", "count": 8 },
      { "dayName": "Wed", "count": 6 },
      { "dayName": "Thu", "count": 9 },
      { "dayName": "Fri", "count": 7 },
      { "dayName": "Sat", "count": 3 },
      { "dayName": "Sun", "count": 4 }
    ]
  },
  "errors": []
}
```

## Author's Note

This project was built and is ready for review/demo by the recruiter or evaluator. All core features from the requirements — ticket listing, ticket history, ticket create/update, and dashboard summary — are implemented and functional.
