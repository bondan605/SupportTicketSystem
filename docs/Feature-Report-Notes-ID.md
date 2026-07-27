# Catatan Desain Fitur Report (ID)

Catatan internal yang mendokumentasikan apa yang sudah dirancang dan dibangun hari ini untuk
fitur **Manager Report**, supaya rekan tim bisa memahami keputusan desainnya tanpa perlu
menurunkan ulang dari awal.

---

## 1. Apa Fitur Ini

Endpoint analitik untuk role Manager, mengembalikan 11 komponen report dalam satu response:
card overview ticket, breakdown per status/kategori/prioritas, tren, average response time,
dan SLA compliance. Dibangun dengan layering DTO → Repository → Service (dengan caching) →
Controller.

## 2. Arsitektur

```
Controllers/
  ReportController.cs        # HTTP layer tipis, delegasi ke service
  DevDataController.cs       # khusus dev: clear/seed/reset data ticket demo

Application/
  DTOs/Reports/               # 1 DTO per komponen report + root ReportSummaryDto
  Validators/Reports/         # rule FluentValidation untuk filter rentang tanggal
  Services/Reports/           # orkestrasi pemanggilan repository, terapkan caching

Infrastructure/
  Repositories/Reports/       # 1 query agregat EF Core per komponen
  Persistence/Seeding/        # generator data demo dalam jumlah besar (Tickets + TicketHistories)
```

**Keputusan struktural utama:**
- DTO dipecah **per komponen**, bukan satu object flat, supaya frontend bisa mengonsumsi dan
  mengembangkan tiap chart/card secara independen.
- **Satu repository dengan banyak method** — bukan satu class service per komponen — supaya
  struktur tetap ramping tanpa over-engineering untuk fitur dengan ~11 query read-only.
- Query di-`await` **secara berurutan (sequential)** di service layer, bukan dijalankan
  paralel lewat `Task.WhenAll`. `DbContext` yang bersifat scoped tidak thread-safe dan tidak
  bisa menjalankan beberapa operasi bersamaan di instance yang sama — menjalankan query ini
  secara paralel akan butuh `DbContext` terpisah per query (misal lewat
  `IDbContextFactory`), yang tidak sepadan untuk volume/profil latency query fitur ini.
- Agregasi terjadi **di level database** (`GroupBy().Select(...)`), bukan di memory.

## 3. Komponen Report → Sumber Data

| # | Komponen | Difilter berdasarkan | Logika utama |
|---|-----------|-------------|-----------|
| 1 | Ticket Overview (cards) | `CreatedAt` | Hitungan per `TicketStatus` (Open/InProgress/Resolved/Closed — tidak ada status "Cancelled" di domain ini) + jumlah user aktif |
| 2 | Tickets per Status | `CreatedAt` | Count + % per `TicketStatus` |
| 3 | Tickets Trend | `CreatedAt` (dibuat) / `ClosedAt` (selesai) | Series per hari, tiap metrik di-query independen |
| 4 | Tickets per Assignee | `CreatedAt` | Top 6 assignee berdasarkan count + bucket "Unassigned" |
| 5 | Tickets per Category | `CreatedAt` | Count + % per `TicketCategory` |
| 6 | Tickets per Priority | `CreatedAt` | Count + % per `TicketPriority` |
| 7 | Average Response Time | `CreatedAt` | Rata-rata menit antara `Ticket.CreatedAt` dengan entry `TicketHistory` pertama di mana `Action == StatusChanged` menuju `InProgress` |
| 8 | SLA Compliance | `ClosedAt` | % ticket closed di mana `ClosedAt <= EstimatedDueDate`. Ticket tanpa `EstimatedDueDate` dikecualikan |
| 9 | Tickets by Category (tabel) | `CreatedAt` | Sumber sama dengan #5, bentuk tabular |
| 10 | Recent Closed Tickets | `ClosedAt` | Ticket closed terbaru; "Closed By" = `Assignee.Name` dari ticket tersebut |
| 11 | SLA Compliance Trend | `ClosedAt` | Series per hari dengan **dua-duanya** compliance % harian dan kumulatif — menunggu konfirmasi mana yang dipakai UI |

## 4. Business Rules & Keputusan Desain Hari Ini

- **Tidak ada status "Cancelled".** Mockup referensi menampilkannya, tapi `TicketStatus`
  cuma punya `Open`, `InProgress`, `Resolved`, `Closed`. Semua komponen berbasis status
  memakai 4 nilai ini.
- **"Tickets Selesai" (card atas) = hitungan `Closed` saja** (bukan `Resolved`).
- **Target SLA = `Ticket.EstimatedDueDate`**, field yang sudah ada per-ticket — bukan policy
  tetap per level Priority.
- **Average Response Time ≠ resolution time.** Mengukur waktu-ke-aksi-pertama (Open →
  InProgress lewat `TicketHistory`), bukan waktu-ke-selesai.
- **Validasi ada di service layer**, pakai `ValidateAndThrowAsync` (FluentValidation), bukan
  di controller. Controller tidak ada pengecekan `if` manual untuk validasi.
- **Bug yang diperbaiki hari ini:** validator SLA/tanggal awalnya membandingkan `EndDate`
  (yang di-extend jadi `23:59:59.999` untuk inclusive-range query) langsung terhadap
  `DateTime.Now`, menyebabkan error palsu "endDate cannot be in the future" padahal tidak
  ada tanggal masa depan yang diminta. Diperbaiki dengan membandingkan `.Date` terhadap
  `DateTime.Today`, bukan membandingkan instant presisi.
- **Bug yang diperbaiki hari ini:** `AppDbContext.ApplyAuditInformation()` menimpa
  `CreatedAt` yang di-set manual dengan `DateTime.UtcNow` setiap `SaveChanges` dipanggil,
  yang akan merusak timestamp historis yang dipakai untuk seeding demo. Diperbaiki dengan
  menambahkan flag bypass `IsSeeding` di `AppDbContext`.

## 5. Caching

`ReportService` meng-cache `ReportSummaryDto` lengkap per rentang tanggal selama **5 menit**
pakai `IMemoryCache`, dengan key `report-summary:{startDate}:{endDate}`.

**Batasan yang perlu diketahui:** `IMemoryCache` sifatnya in-process — cukup untuk deployment
single-instance, tapi tidak akan konsisten di banyak instance app di belakang load balancer.
Perlu `IDistributedCache` (misal Redis) untuk skenario itu. Data report bisa lag sampai 5
menit dari perubahan ticket terbaru; ini trade-off yang disengaja, bukan bug.

## 6. Aturan Lifecycle Ticket yang Dipakai Seeder (mengikuti dokumen Role & Scenario)

Seeder diperbarui supaya mengikuti `Role_Scenario_Ticket_Management_EN.md`, bukan lifecycle
sembarangan, supaya data demo yang dihasilkan mencerminkan aturan permission/alur yang nyata:

- **Ticket dibuat oleh Support Agent** (berdasarkan komplain pelanggan), bukan oleh
  admin/manager. `Ticket.CreatedBy` = Id agent pembuat.
- **Assignment adalah aksi Manager yang otomatis mengubah status.** Saat Manager meng-assign
  agent ke ticket `Open`, tercatat 2 history entry sekaligus (`AssigneeChanged` lalu
  `StatusChanged` Open→InProgress), keduanya `ChangedBy` = Manager tersebut — karena
  perubahan status adalah *efek samping* dari aksi assignment Manager, bukan aksi terpisah
  dari agent.
- **Reassignment hanya terjadi saat status `InProgress`**, dan hanya oleh Manager.
- **Hanya agent yang sedang assigned** saat itu yang bisa mengubah ticket ke `Resolved` atau
  `Closed` (langsung, atau Resolved → Closed).
- **`Ticket.UpdatedAt`/`UpdatedBy` di-set di setiap perubahan berikutnya** (assignment,
  reassignment, resolve, close) — bukan cuma sekali saat dibuat. Ticket yang tidak pernah
  keluar dari `Open` punya `UpdatedAt`/`UpdatedBy` tetap `null`, karena memang tidak pernah
  diubah setelah dibuat.
- **`TicketHistory.CreatedBy`** juga diisi (sama dengan `ChangedBy`) untuk tiap history
  entry, konsisten dengan prinsip audit-preservation yang sama (`IsSeeding` bypass) yang
  diterapkan ke `Ticket`.

## 7. DevDataController — Tujuan, Endpoint & Konfigurasi

Controller utilitas khusus development untuk generate/hapus data ticket demo, karena data
produksi sungguhan belum ada dan report butuh volume data supaya bermakna (tren, SLA %,
distribusi per-assignee, dll. terlihat tidak berarti kalau cuma ada segelintir ticket).

**Kenapa bulk seeder terpisah, bukan pakai EF Core `HasData`:** `HasData` (dipakai untuk
`Users`) butuh nilai statis penuh yang di-bake ke migration, yang tidak praktis untuk ratusan
baris dengan distribusi acak sepanjang rentang tanggal yang bisa dikonfigurasi.
`ReportDemoDataSeeder` men-generate volume itu secara programatik, dipanggil on-demand lewat
endpoint ini, bukan saat startup.

| Method | Endpoint | Deskripsi |
|--------|----------|--------------|
| POST | `/api/dev/report-data/seed` | Generate data demo Tickets + TicketHistories, mengikuti aturan lifecycle di Section 6. Menerima query parameter opsional (lihat di bawah). Di-skip (no-op) kalau DB sudah punya lebih dari 20 ticket — panggil `/clear` dulu untuk memaksa reseed baru. |
| DELETE | `/api/dev/report-data/clear` | Hapus semua `Tickets` dan `TicketHistories`. **`Users` tidak pernah disentuh** — dikelola lewat migration seed data, dan kalau ikut terhapus akan merusak foreign key `AssignedTo`/`ChangedBy` di ticket baru. |
| POST | `/api/dev/report-data/reset` | Wrapper praktis: `clear` langsung diikuti `seed`, dalam satu panggilan. Menerima query parameter opsional yang sama seperti `/seed`. |

**Keamanan:** setiap action di controller ini mengecek `IWebHostEnvironment.IsDevelopment()`
dulu dan mengembalikan `403 Forbidden` kalau tidak — ini wajib tidak bisa diakses di
Staging/Production.

### Mengatur volume (`/seed` dan `/reset` query parameter)

Semua parameter opsional — kalau tidak diisi, pakai default di bawah (sama seperti perilaku
hardcoded sebelumnya).

| Parameter | Default | Deskripsi |
|-----------|---------|--------------|
| `daysToGenerate` | `45` | Berapa hari ke belakang dari hari ini untuk generate ticket. |
| `minTicketsPerDay` | `2` | Minimum ticket dibuat per hari (diabaikan kalau `totalTickets` diisi). |
| `maxTicketsPerDay` | `6` | Maksimum ticket dibuat per hari (diabaikan kalau `totalTickets` diisi). |
| `totalTickets` | *(kosong)* | Kalau diisi, generate persis sejumlah ini, tersebar acak sepanjang `daysToGenerate` — meng-override `minTicketsPerDay`/`maxTicketsPerDay` sepenuhnya. |

Response menyertakan `estimatedTicketCount` (perkiraan sebelum insert — persis kalau
`totalTickets` diisi, perkiraan berbasis rata-rata kalau tidak) dan `actualTicketsInserted`
(jumlah asli setelah generate), supaya kamu bisa cek volume tanpa perlu query database
terpisah.

**Seperti apa data yang dihasilkan (dengan setting default):** ±180 ticket sepanjang 45 hari,
distribusi status dibobotkan berdasarkan umur ticket (ticket lama cenderung `Closed`, ticket
baru cenderung `Open`/`InProgress`), siklus `TicketHistory` lengkap mengikuti aturan di
Section 6, dengan jeda waktu acak yang realistis, dan sekitar 85% ticket closed memenuhi
target SLA-nya (15% sisanya sengaja closed telat) — supaya SLA Compliance menampilkan angka
realistis ~85–90%, bukan 100% terus-menerus yang tidak berguna.

## 8. Contoh Pemanggilan API

**Seed dengan volume default (45 hari, 2-6 ticket/hari):**
```bash
curl -X POST https://localhost:5001/api/dev/report-data/seed
```

**Seed dengan rentang harian custom:**
```bash
curl -X POST "https://localhost:5001/api/dev/report-data/seed?daysToGenerate=60&minTicketsPerDay=3&maxTicketsPerDay=8"
```

**Seed jumlah pasti (misal 300 ticket tersebar di 60 hari):**
```bash
curl -X POST "https://localhost:5001/api/dev/report-data/seed?daysToGenerate=60&totalTickets=300"
```

**Hapus data demo:**
```bash
curl -X DELETE https://localhost:5001/api/dev/report-data/clear
```

**Clear + reseed dengan volume custom dalam satu panggilan:**
```bash
curl -X POST "https://localhost:5001/api/dev/report-data/reset?totalTickets=300"
```

**Ambil report summary:**
```bash
curl -X GET "https://localhost:5001/api/reports/summary?startDate=2026-06-26&endDate=2026-07-24" \
  -H "Authorization: Bearer {your_jwt_token}"
```

## 9. Masih Pending / Belum Diputuskan

- Field `ChangePercent` (perbandingan periode sebelumnya) di `TicketOverviewDto`,
  `AverageResponseTimeDto`, dan `SlaComplianceDto` saat ini masih `null` — implementasinya
  ditunda menunggu keputusan scope (diingatkan kembali setelah semua 11 komponen dipastikan
  berjalan end-to-end).
- `SlaComplianceTrendPointDto` menyediakan dua-duanya `DailyCompliancePercentage` dan
  `CumulativeCompliancePercentage` — menunggu konfirmasi dari senior mana interpretasi yang
  sebenarnya dipakai di frontend.
