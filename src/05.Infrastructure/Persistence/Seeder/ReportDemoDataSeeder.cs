using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Seeding;

/// <summary>
/// Generates a large, realistic volume of demo Ticket and TicketHistory data spanning the
/// past ~45 days, so the Report feature has enough data to visualize trends, SLA compliance,
/// and per-assignee/category/priority breakdowns meaningfully.
///
/// Intentionally separate from TicketConfiguration/TicketHistoryConfiguration (EF Core
/// HasData): HasData requires fully static, deterministic values compiled into migrations,
/// which is impractical for hundreds of randomly-distributed rows. This keeps the
/// IEntityTypeConfiguration classes focused purely on schema, with a bulk generator handling
/// volume separately.
/// </summary>
public static class ReportDemoDataSeeder
{
    private const int DaysToGenerate = 45; // covers "last month" plus the current period
    private const int MinTicketsPerDay = 2;
    private const int MaxTicketsPerDay = 6;
    private const int SeedThreshold = 20; // skip if the DB already has more than this many tickets

    // Ids must match UserConfiguration seed data.
    private static readonly Guid AdminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid[] AgentIds =
    {
        Guid.Parse("22222222-2222-2222-2222-222222222222"), // Andi Pratama
        Guid.Parse("33333333-3333-3333-3333-333333333333"), // Siti Aisyah
        Guid.Parse("44444444-4444-4444-4444-444444444444"), // Budi Santoso
        Guid.Parse("55555555-5555-5555-5555-555555555555"), // Dewi Lestari
        Guid.Parse("66666666-6666-6666-6666-666666666666"), // Rizky Hidayat
    };

    private static readonly string[] CustomerNames =
    {
        "Rina Wijaya", "Hendra Saputra", "Maya Sari", "Fajar Nugroho", "Lina Marlina",
        "Agus Setiawan", "Dian Puspita", "Bayu Aditya", "Wulan Sari", "Rudi Hartono",
        "Fitri Handayani", "Yusuf Ibrahim", "Nita Kusuma", "Eko Prasetyo", "Sari Indah"
    };

    private static readonly (string Title, TicketCategory Category)[] TitleTemplates =
    {
        ("Data tidak tersimpan", TicketCategory.Application),
        ("Aplikasi lambat", TicketCategory.Application),
        ("Error saat submit form", TicketCategory.Application),
        ("Fitur export tidak berfungsi", TicketCategory.Application),
        ("Reset password user", TicketCategory.Access),
        ("Tidak bisa login", TicketCategory.Access),
        ("Butuh akses modul baru", TicketCategory.Access),
        ("Akun terkunci", TicketCategory.Access),
        ("Laporan tidak muncul", TicketCategory.Report),
        ("Data laporan tidak sesuai", TicketCategory.Report),
        ("Export laporan gagal", TicketCategory.Report),
        ("Laptop tidak menyala", TicketCategory.Hardware),
        ("Printer error", TicketCategory.Hardware),
        ("Monitor bermasalah", TicketCategory.Hardware),
        ("Integrasi API gagal", TicketCategory.Other),
        ("Permintaan informasi sistem", TicketCategory.Other),
    };

    /// <summary>
    /// Generates and inserts demo tickets and their history. No-op if the database already
    /// has more than <see cref="SeedThreshold"/> tickets, so this only fires once against a
    /// freshly migrated database and stays safe to call on every startup.
    /// </summary>
    public static async Task<int> SeedAsync(AppDbContext context)
    {
        if (await context.Tickets.CountAsync() > SeedThreshold)
        {
            return 0;
        }

        var random = new Random(20260728); // fixed seed so demo data is reproducible across runs
        var tickets = new List<Ticket>();
        var histories = new List<TicketHistory>();

        var nextTicketSequence = await GetNextTicketSequenceAsync(context);
        var today = DateTime.UtcNow.Date;

        for (var dayOffset = DaysToGenerate; dayOffset >= 0; dayOffset--)
        {
            var day = today.AddDays(-dayOffset);
            var ticketsToday = random.Next(MinTicketsPerDay, MaxTicketsPerDay + 1);

            for (var i = 0; i < ticketsToday; i++)
            {
                var (ticket, ticketHistories) = GenerateTicket(random, day, dayOffset, nextTicketSequence);
                tickets.Add(ticket);
                histories.AddRange(ticketHistories);
                nextTicketSequence++;
            }
        }

        context.IsSeeding = true;
        await context.Tickets.AddRangeAsync(tickets);
        await context.TicketHistories.AddRangeAsync(histories);
        await context.SaveChangesAsync();
        context.IsSeeding = false;

        return tickets.Count;
    }

    /// <summary>
    /// Deletes all Ticket and TicketHistory rows. Users are intentionally untouched, since
    /// they are managed via EF Core migration HasData, not by this seeder.
    /// TicketHistories are deleted first to satisfy the FK constraint even though Ticket ->
    /// TicketHistory is configured with cascade delete (explicit order avoids relying on
    /// cascade behavior for a bulk delete operation).
    /// </summary>
    public static async Task ClearAsync(AppDbContext context)
    {
        await context.TicketHistories.ExecuteDeleteAsync();
        await context.Tickets.ExecuteDeleteAsync();
    }

    private static async Task<int> GetNextTicketSequenceAsync(AppDbContext context)
    {
        var existingNumbers = await context.Tickets.Select(t => t.TicketNumber).ToListAsync();

        var maxSequence = existingNumbers
            .Select(n => int.TryParse(n.Replace("TKT-", ""), out var seq) ? seq : 0)
            .DefaultIfEmpty(0)
            .Max();

        return maxSequence + 1;
    }

    private static (Ticket Ticket, List<TicketHistory> Histories) GenerateTicket(Random random, DateTime day, int ageInDays, int sequence)
    {
        var createdAt = day.AddHours(random.Next(8, 18)).AddMinutes(random.Next(0, 60));
        var template = TitleTemplates[random.Next(TitleTemplates.Length)];
        var priority = WeightedPriority(random);
        var status = DetermineStatus(random, ageInDays);
        var assignedTo = status == TicketStatus.Open && random.NextDouble() < 0.3
            ? (Guid?)null
            : AgentIds[random.Next(AgentIds.Length)];

        var estimatedDueDate = createdAt.Add(SlaTargetFor(priority));
        var ticketId = Guid.NewGuid();

        var ticket = new Ticket
        {
            Id = ticketId,
            TicketNumber = $"TKT-{sequence:D5}",
            CustomerName = CustomerNames[random.Next(CustomerNames.Length)],
            CustomerEmail = $"customer{sequence}@example.com",
            Title = template.Title,
            Description = $"{template.Title} - dilaporkan oleh pelanggan, membutuhkan penanganan tim support.",
            Status = status,
            Priority = priority,
            Type = (TicketType)random.Next(0, 4),
            Category = template.Category,
            Impact = (TicketImpact)random.Next(0, 3),
            Application = template.Category == TicketCategory.Hardware
                ? TicketApplication.None
                : (TicketApplication)random.Next(1, 9),
            AssignedTo = assignedTo,
            EstimatedDueDate = estimatedDueDate,
            CreatedAt = createdAt,
        };

        var histories = new List<TicketHistory>
        {
            new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                Action = TicketHistoryAction.TicketCreated,
                Note = "Ticket created by customer request.",
                ChangedBy = AdminId,
                Timestamp = createdAt,
                CreatedAt = createdAt,
            }
        };

        if (status is TicketStatus.InProgress or TicketStatus.Resolved or TicketStatus.Closed && assignedTo.HasValue)
        {
            // Response time: 15 minutes to 6 hours after creation. This is what
            // GetAverageResponseTimeAsync in the repository measures.
            var respondedAt = createdAt.AddMinutes(random.Next(15, 360));
            histories.Add(new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                Action = TicketHistoryAction.StatusChanged,
                OldValue = nameof(TicketStatus.Open),
                NewValue = nameof(TicketStatus.InProgress),
                ChangedBy = assignedTo.Value,
                Timestamp = respondedAt,
                CreatedAt = respondedAt,
            });

            if (status is TicketStatus.Resolved or TicketStatus.Closed)
            {
                // Resolution time: 2 hours to 5 days after work started.
                var resolvedAt = respondedAt.AddMinutes(random.Next(120, 7200));
                histories.Add(new TicketHistory
                {
                    Id = Guid.NewGuid(),
                    TicketId = ticketId,
                    Action = TicketHistoryAction.StatusChanged,
                    OldValue = nameof(TicketStatus.InProgress),
                    NewValue = nameof(TicketStatus.Resolved),
                    ChangedBy = assignedTo.Value,
                    Timestamp = resolvedAt,
                    CreatedAt = resolvedAt,
                });

                if (status == TicketStatus.Closed)
                {
                    // 85% chance of closing within SLA, 15% chance of a late close — keeps
                    // SLA Compliance realistic (~85-90%) instead of a meaningless flat 100%.
                    var closedAt = random.NextDouble() < 0.85
                        ? resolvedAt.AddMinutes(random.Next(10, 180))
                        : estimatedDueDate.AddHours(random.Next(1, 48));

                    ticket.ClosedAt = closedAt;

                    histories.Add(new TicketHistory
                    {
                        Id = Guid.NewGuid(),
                        TicketId = ticketId,
                        Action = TicketHistoryAction.StatusChanged,
                        OldValue = nameof(TicketStatus.Resolved),
                        NewValue = nameof(TicketStatus.Closed),
                        ChangedBy = assignedTo.Value,
                        Timestamp = closedAt,
                        CreatedAt = closedAt,
                    });
                }
            }
        }

        return (ticket, histories);
    }

    /// <summary>
    /// Older tickets are more likely to have progressed further in their lifecycle; recent
    /// tickets are more likely to still be Open/InProgress. This produces a realistic-looking
    /// Tickets Trend chart instead of a flat/random distribution.
    /// </summary>
    private static TicketStatus DetermineStatus(Random random, int ageInDays)
    {
        if (ageInDays <= 1) return random.NextDouble() < 0.6 ? TicketStatus.Open : TicketStatus.InProgress;
        if (ageInDays <= 3) return WeightedPick(random, (TicketStatus.Open, 0.25), (TicketStatus.InProgress, 0.45), (TicketStatus.Resolved, 0.30));
        if (ageInDays <= 10) return WeightedPick(random, (TicketStatus.InProgress, 0.20), (TicketStatus.Resolved, 0.25), (TicketStatus.Closed, 0.55));

        return WeightedPick(random, (TicketStatus.Resolved, 0.10), (TicketStatus.Closed, 0.90));
    }

    private static TicketPriority WeightedPriority(Random random) => WeightedPick(random, (TicketPriority.Low, 0.40), (TicketPriority.Medium, 0.40), (TicketPriority.High, 0.20));

    private static TimeSpan SlaTargetFor(TicketPriority priority) => priority switch
    {
        TicketPriority.High => TimeSpan.FromDays(1),
        TicketPriority.Medium => TimeSpan.FromDays(3),
        TicketPriority.Low => TimeSpan.FromDays(7),
        _ => TimeSpan.FromDays(3)
    };

    private static T WeightedPick<T>(Random random, params (T Value, double Weight)[] options)
    {
        var roll = random.NextDouble();
        var cumulative = 0.0;

        foreach (var (value, weight) in options)
        {
            cumulative += weight;
            if (roll <= cumulative)
            {
                return value;
            }
        }

        return options[^1].Value;
    }
}