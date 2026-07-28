# Report Calculation Reference

Dokumen ini menjelaskan **parameter yang dibutuhkan** dan **formula perhitungan** yang
dipakai `IReportRepository` untuk tiap komponen report, supaya gampang dijelaskan ke
tim/reviewer tanpa perlu baca kode langsung.

Semua method menerima `ReportFilterDto(StartDate, EndDate)` sebagai parameter dasar, kecuali
disebutkan lain.

---

## 1. Ticket Overview (Cards)

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `Users.IsActive`

**Formula:**
```
TotalTickets     = COUNT(Tickets WHERE CreatedAt BETWEEN StartDate AND EndDate)
OpenCount        = COUNT(... AND Status = Open)
InProgressCount  = COUNT(... AND Status = InProgress)
ResolvedCount    = COUNT(... AND Status = Resolved)
ClosedCount      = COUNT(... AND Status = Closed)
TotalUsers       = COUNT(Users WHERE IsActive = true)   // tidak difilter tanggal
```
**Catatan:** `TotalUsers` sengaja tidak ikut difilter rentang tanggal — jumlah user aktif
bukan metrik yang time-bound seperti ticket.

---

## 2. Tickets per Status

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `Tickets.Status`

**Formula (per status S):**
```
Count(S)      = COUNT(Tickets WHERE CreatedAt BETWEEN StartDate AND EndDate AND Status = S)
Percentage(S) = Count(S) / TotalTickets * 100        // dibulatkan 1 desimal
                (0 jika TotalTickets = 0)
```

---

## 3. Tickets Trend

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `Tickets.ClosedAt`

**Formula (per hari D dalam rentang):**
```
CreatedCount(D) = COUNT(Tickets WHERE CreatedAt.Date = D)
ClosedCount(D)  = COUNT(Tickets WHERE ClosedAt IS NOT NULL AND ClosedAt.Date = D)
```
**Catatan:** dua metrik ini **independen** — satu ticket bisa muncul di `CreatedCount` pada
hari X dan di `ClosedCount` pada hari Y yang berbeda.

---

## 4. Tickets per Assignee

**Parameter:** `StartDate`, `EndDate`, `topN` (default 6)

**Sumber data:** `Tickets.CreatedAt`, `Tickets.AssignedTo`

**Formula:**
```
Count(agentX) = COUNT(Tickets WHERE CreatedAt BETWEEN StartDate AND EndDate
                       AND AssignedTo = agentX)

TopAssignees  = TOP topN agent BY Count(agent) DESC

UnassignedCount = COUNT(Tickets WHERE CreatedAt BETWEEN StartDate AND EndDate
                         AND AssignedTo IS NULL)
```
**Catatan:** `UnassignedCount` ditambahkan sebagai baris terpisah di luar Top N, tidak ikut
bersaing rank dengan agent asli.

---

## 5 & 9. Tickets per Category

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `Tickets.Category`

**Formula (per kategori C):**
```
Count(C)      = COUNT(Tickets WHERE CreatedAt BETWEEN StartDate AND EndDate AND Category = C)
Percentage(C) = Count(C) / TotalTickets * 100        // dibulatkan 1 desimal
```
**Catatan:** komponen #5 (donut chart) dan #9 (tabel) memakai method & formula yang **sama
persis** — cuma beda bentuk presentasi di frontend.

---

## 6. Tickets per Priority

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `Tickets.Priority`

**Formula (per priority P):** identik dengan formula per-Category di atas, di-`GROUP BY`
`Priority` alih-alih `Category`.

---

## 7. Average Response Time

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.CreatedAt`, `TicketHistories.Action`, `TicketHistories.NewValue`,
`TicketHistories.Timestamp`

**Formula (per ticket T):**
```
FirstInProgressAt(T) = MIN(TicketHistories.Timestamp
                            WHERE TicketId = T.Id
                            AND Action = StatusChanged
                            AND NewValue = "InProgress")

ResponseMinutes(T) = FirstInProgressAt(T) - T.CreatedAt     // dalam menit
```
**Formula agregat:**
```
AverageResponseMinutes = AVG(ResponseMinutes(T))
                          FOR ALL T WHERE T.CreatedAt BETWEEN StartDate AND EndDate
                          AND FirstInProgressAt(T) IS NOT NULL
```
**Catatan:** ticket yang **tidak pernah** punya history `StatusChanged → InProgress`
dikecualikan dari perhitungan (belum ada "respons" yang bisa diukur).

---

## 8. SLA Compliance

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** `Tickets.Status`, `Tickets.ClosedAt`, `Tickets.EstimatedDueDate`

**Formula:**
```
Evaluable = { Tickets WHERE Status = Closed
                       AND ClosedAt IS NOT NULL
                       AND ClosedAt BETWEEN StartDate AND EndDate
                       AND EstimatedDueDate IS NOT NULL }

IsCompliant(T) = (T.ClosedAt <= T.EstimatedDueDate)   // TRUE/FALSE per ticket

EvaluatedTicketCount  = COUNT(Evaluable)
CompliantCount        = COUNT(Evaluable WHERE IsCompliant(T) = TRUE)
CompliancePercentage  = CompliantCount / EvaluatedTicketCount * 100
                         (0 jika EvaluatedTicketCount = 0)
```
**Catatan:** ticket **tanpa `EstimatedDueDate`** dikecualikan total dari numerator maupun
denominator — tidak ada target untuk dinilai kepatuhannya, jadi tidak dihitung sebagai
"gagal" ataupun "berhasil".

---

## 10. Recent Closed Tickets

**Parameter:** `StartDate`, `EndDate`, `count` (default 5)

**Sumber data:** `Tickets.Status`, `Tickets.ClosedAt`, `Tickets.Assignee.Name`

**Logika (bukan agregasi numerik, cuma filter + sort):**
```
Result = TOP count Tickets
         WHERE Status = Closed
         AND ClosedAt IS NOT NULL
         AND ClosedAt BETWEEN StartDate AND EndDate
         ORDER BY ClosedAt DESC

ClosedBy(T) = T.Assignee.Name  IF T.AssignedTo IS NOT NULL
              ELSE "Unassigned"
```

---

## 11. SLA Compliance Trend

**Parameter:** `StartDate`, `EndDate`

**Sumber data:** sama seperti komponen #8, dipecah per hari

**Formula (per hari D, evaluable ticket = definisi sama seperti komponen #8):**
```
DayTickets(D) = { t IN Evaluable WHERE t.ClosedAt.Date = D }

DailyCompliancePercentage(D) = COUNT(DayTickets(D) WHERE IsCompliant) 
                                / COUNT(DayTickets(D)) * 100
                                (0 jika DayTickets(D) kosong)
```

**Formula kumulatif (akumulasi dari StartDate sampai hari D):**
```
CumulativeTotal(D)     = SUM( COUNT(DayTickets(d)) )      FOR ALL d <= D
CumulativeCompliant(D) = SUM( COUNT(DayTickets(d) WHERE IsCompliant) )   FOR ALL d <= D

CumulativeCompliancePercentage(D) = CumulativeCompliant(D) / CumulativeTotal(D) * 100
                                     (0 jika CumulativeTotal(D) = 0)
```
**Catatan:** dua metrik ini disediakan **berdampingan** di response (`DailyCompliancePercentage`
dan `CumulativeCompliancePercentage`) — pilihan mana yang dipakai di frontend masih pending
konfirmasi (lihat Report-Feature-Notes).

---

## Ringkasan Field yang Dipakai per Komponen

| # | Komponen | Field Ticket yang dipakai | Field lain |
|---|----------|---------------------------|------------|
| 1 | Overview | `CreatedAt`, `Status` | `Users.IsActive` |
| 2 | Per Status | `CreatedAt`, `Status` | - |
| 3 | Trend | `CreatedAt`, `ClosedAt` | - |
| 4 | Per Assignee | `CreatedAt`, `AssignedTo` | `User.Name` |
| 5, 9 | Per Category | `CreatedAt`, `Category` | - |
| 6 | Per Priority | `CreatedAt`, `Priority` | - |
| 7 | Avg Response Time | `CreatedAt` | `TicketHistory.Action`, `.NewValue`, `.Timestamp` |
| 8 | SLA Compliance | `Status`, `ClosedAt`, `EstimatedDueDate` | - |
| 10 | Recent Closed | `Status`, `ClosedAt` | `Assignee.Name` |
| 11 | SLA Trend | `Status`, `ClosedAt`, `EstimatedDueDate` | - |

---

## Ideas to Expand (Not Implemented Yet)

Ide-ide berikut **belum diimplementasikan** — dicatat di sini sebagai referensi diskusi,
bukan bagian dari formula yang sudah berjalan di atas.

### SLA Variance (usulan dari senior)

Formula SLA Compliance yang ada sekarang (komponen #8 & #11) itu **binary/boolean** — cuma
menjawab "tepat waktu atau tidak" per ticket, tanpa info seberapa jauh selisihnya:
```
IsCompliant(T) = (ClosedAt <= EstimatedDueDate)   // TRUE / FALSE saja
```

Usulannya: tambah dimensi **besaran selisih (variance)**, bukan cuma yes/no:
```
Variance(T) = EstimatedDueDate(T) - ClosedAt(T)     // dalam jam, bisa desimal

  Variance(T) > 0  →  ticket closed LEBIH AWAL dari due date (margin aman)
  Variance(T) < 0  →  ticket closed TELAT dari due date (overrun)
  Variance(T) = 0  →  closed persis di due date
```

Dari situ bisa dipecah jadi **3 metrik** yang menjawab pertanyaan berbeda:

```
// 1. Average — gabungan SEMUA ticket evaluable
AverageVarianceHours = AVG(Variance(T))  FOR ALL T in Evaluable
// Menjawab: "secara keseluruhan rata-rata selisih ke target berapa jam?"
// CATATAN: angka ini bisa menyesatkan karena mencampur populasi cepat & telat
// jadi satu — 90 ticket cepat 1 jam + 10 ticket telat 20 jam bisa keliatan
// "cuma telat 1.1 jam" padahal ada kelompok yang telat parah.

// 2. Positif — hanya ticket yang tepat waktu / lebih cepat
EarlyOrOnTimeCount      = COUNT(T WHERE Variance(T) >= 0)
AverageEarlyMarginHours = AVG(Variance(T))  FOR T WHERE Variance(T) >= 0
// Menjawab: "dari yang berhasil, seberapa besar marginnya?" (selalu positif)

// 3. Negatif — hanya ticket yang telat
LateCount            = COUNT(T WHERE Variance(T) < 0)
AverageOverrunHours  = AVG(Variance(T))  FOR T WHERE Variance(T) < 0
// Menjawab: "dari yang telat, rata-rata separah apa?" (selalu negatif)
```

**Catatan penting:** metrik #2 dan #3 harus selalu ditampilkan **berpasangan dengan count**
(`EarlyOrOnTimeCount`, `LateCount`) — angka rata-rata saja tanpa tahu jumlah ticket di
belakangnya tidak informatif (misal `-18.6 jam` dari 2 ticket vs dari 40 ticket punya bobot
cerita yang sangat berbeda).

**Rekomendasi bila diimplementasikan:** `AverageVarianceHours` (gabungan) sebaiknya jadi
info tambahan saja, bukan headline metric — karena mencampur dua populasi berbeda cerita.
Fokus utama tetap `CompliancePercentage` (sudah ada) dilengkapi `AverageOverrunHours` untuk
menyorot seberapa parah kelompok yang bermasalah.

**Status:** didiskusikan, belum masuk ke `SlaComplianceDto`/`SlaComplianceTrendPointDto`
maupun repository. Perlu keputusan lanjutan sebelum diimplementasikan (lihat percakapan
terkait untuk detail diskusi).
