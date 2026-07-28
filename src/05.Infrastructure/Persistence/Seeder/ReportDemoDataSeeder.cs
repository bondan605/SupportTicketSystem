using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Seeding;

/// <summary>
/// Generates a large, realistic volume of demo Ticket and TicketHistory data, following the
/// lifecycle and permission rules defined in the Role &amp; Scenario document: tickets are
/// created by Support Agents, assigned/reassigned by Managers (which drives the automatic
/// Open → InProgress transition), and resolved/closed only by the agent currently assigned
/// at that point in time.
/// </summary>
public static class ReportDemoDataSeeder
{
    private const int SeedThreshold = 20; // skip if the DB already has more than this many tickets

    // Ids must match UserConfiguration seed data.
    // "Admin User" and "Nanda Triana" both have UserRole.Manager.
    private static readonly Guid[] ManagerIds =
    {
        Guid.Parse("11111111-1111-1111-1111-111111111111"), // Admin User
        Guid.Parse("77777777-7777-7777-7777-777777777777"), // Nanda Triana
    };

    private static readonly Guid[] AgentIds =
    {
        Guid.Parse("22222222-2222-2222-2222-222222222222"), // Andi Pratama
        Guid.Parse("33333333-3333-3333-3333-333333333333"), // Siti Aisyah
        Guid.Parse("44444444-4444-4444-4444-444444444444"), // Budi Santoso
        Guid.Parse("55555555-5555-5555-5555-555555555555"), // Dewi Lestari
        Guid.Parse("66666666-6666-6666-6666-666666666666"), // Rizky Hidayat
    };

    private static readonly Dictionary<Guid, string> AgentNames = new()
    {
        [AgentIds[0]] = "Andi Pratama",
        [AgentIds[1]] = "Siti Aisyah",
        [AgentIds[2]] = "Budi Santoso",
        [AgentIds[3]] = "Dewi Lestari",
        [AgentIds[4]] = "Rizky Hidayat",
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
    /// Deletes all Ticket and TicketHistory rows. Users are intentionally untouched — they
    /// are managed via EF Core migration HasData, not by this seeder.
    /// </summary>
    public static async Task ClearAsync(AppDbContext context)
    {
        await context.TicketHistories.ExecuteDeleteAsync();
        await context.Tickets.ExecuteDeleteAsync();
    }

    /// <summary>
    /// Generates and inserts demo tickets and their full history following the Role &amp;
    /// Scenario lifecycle rules. No-op (returns 0) if the database already has more than
    /// SeedThreshold tickets — call ClearAsync first to force a fresh reseed.
    /// </summary>
    public static async Task<int> SeedAsync(AppDbContext context, ReportSeedOptions options)
    {
        if (await context.Tickets.CountAsync() > SeedThreshold)
        {
            return 0;
        }

        var random = new Random(20260728); // fixed seed so demo data is reproducible across runs
        var today = DateTime.UtcNow.Date;

        // Build the list of creation days first (either exact TotalTickets, randomly spread
        // across the range, or the per-day Min/Max random range), then sort chronologically
        // so ticket numbers stay roughly correlated with creation date.
        var ticketDays = new List<DateTime>();

        if (options.TotalTickets.HasValue)
        {
            for (var i = 0; i < options.TotalTickets.Value; i++)
            {
                var dayOffset = random.Next(0, options.DaysToGenerate + 1);
                ticketDays.Add(today.AddDays(-dayOffset));
            }
        }
        else
        {
            for (var dayOffset = options.DaysToGenerate; dayOffset >= 0; dayOffset--)
            {
                var ticketsToday = random.Next(options.MinTicketsPerDay, options.MaxTicketsPerDay + 1);
                for (var i = 0; i < ticketsToday; i++)
                {
                    ticketDays.Add(today.AddDays(-dayOffset));
                }
            }
        }

        ticketDays.Sort();

        var tickets = new List<Ticket>();
        var histories = new List<TicketHistory>();
        var nextTicketSequence = await GetNextTicketSequenceAsync(context);

        foreach (var day in ticketDays)
        {
            var ageInDays = (today - day).Days;
            var (ticket, ticketHistories) = GenerateTicket(random, day, ageInDays, nextTicketSequence);
            tickets.Add(ticket);
            histories.AddRange(ticketHistories);
            nextTicketSequence++;
        }

        context.IsSeeding = true; // preserve the historical CreatedAt/UpdatedAt values below
        await context.Tickets.AddRangeAsync(tickets);
        await context.TicketHistories.AddRangeAsync(histories);
        await context.SaveChangesAsync();
        context.IsSeeding = false;

        return tickets.Count;
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

    private static (Ticket Ticket, List<TicketHistory> Histories) GenerateTicket(
        Random random, DateTime day, int ageInDays, int sequence)
    {
        var createdAt = day.AddHours(random.Next(8, 18)).AddMinutes(random.Next(0, 60));
        var template = TitleTemplates[random.Next(TitleTemplates.Length)];
        var priority = WeightedPriority(random);
        var finalStatus = DetermineFinalStatus(random, ageInDays);

        // Per the Role & Scenario document, tickets are created by a Support Agent based on
        // a customer complaint — not by an admin/manager.
        var creatorAgentId = AgentIds[random.Next(AgentIds.Length)];
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
            Status = TicketStatus.Open, // always starts Open; updated below as the lifecycle progresses
            Priority = priority,
            Type = (TicketType)random.Next(0, 4),
            Category = template.Category,
            Impact = (TicketImpact)random.Next(0, 3),
            Application = template.Category == TicketCategory.Hardware
                ? TicketApplication.None
                : (TicketApplication)random.Next(1, 9),
            AssignedTo = null,
            EstimatedDueDate = estimatedDueDate,
            CreatedAt = createdAt,
            CreatedBy = creatorAgentId, // first creation: CreatedBy is set
        };

        var histories = new List<TicketHistory>
        {
            new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                Action = TicketHistoryAction.TicketCreated,
                Note = $"Ticket created by {AgentNames[creatorAgentId]} based on customer complaint.",
                ChangedBy = creatorAgentId,
                Timestamp = createdAt,
                CreatedAt = createdAt,
                CreatedBy = creatorAgentId,
            }
        };

        if (finalStatus == TicketStatus.Open)
        {
            // Ticket never got assigned. No further updates — Ticket.UpdatedAt/UpdatedBy stay
            // null, since the record was never modified after its initial creation.
            return (ticket, histories);
        }

        // --- Manager assigns an agent: Open -> InProgress (automatic status change) ---
        var assigningManagerId = ManagerIds[random.Next(ManagerIds.Length)];
        var currentAssigneeId = AgentIds[random.Next(AgentIds.Length)];
        var assignTimestamp = createdAt.AddMinutes(random.Next(15, 360));

        histories.Add(new TicketHistory
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            Action = TicketHistoryAction.AssigneeChanged,
            OldValue = "Unassigned",
            NewValue = AgentNames[currentAssigneeId],
            ChangedBy = assigningManagerId,
            Timestamp = assignTimestamp,
            CreatedAt = assignTimestamp,
            CreatedBy = assigningManagerId,
        });
        histories.Add(new TicketHistory
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            Action = TicketHistoryAction.StatusChanged,
            OldValue = nameof(TicketStatus.Open),
            NewValue = nameof(TicketStatus.InProgress),
            Note = "Status automatically changed to InProgress upon agent assignment.",
            ChangedBy = assigningManagerId,
            Timestamp = assignTimestamp,
            CreatedAt = assignTimestamp,
            CreatedBy = assigningManagerId,
        });

        ticket.AssignedTo = currentAssigneeId;
        ticket.Status = TicketStatus.InProgress;
        ticket.UpdatedAt = assignTimestamp;
        ticket.UpdatedBy = assigningManagerId;

        var lastEventTimestamp = assignTimestamp;

        // --- Optional reassignment while still InProgress (Manager only) ---
        if (finalStatus != TicketStatus.Open && random.NextDouble() < 0.25)
        {
            var reassigningManagerId = ManagerIds[random.Next(ManagerIds.Length)];
            var newAssigneeId = AgentIds[random.Next(AgentIds.Length)];
            var reassignTimestamp = lastEventTimestamp.AddMinutes(random.Next(30, 480));

            histories.Add(new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                Action = TicketHistoryAction.AssigneeChanged,
                OldValue = AgentNames[currentAssigneeId],
                NewValue = AgentNames[newAssigneeId],
                ChangedBy = reassigningManagerId,
                Timestamp = reassignTimestamp,
                CreatedAt = reassignTimestamp,
                CreatedBy = reassigningManagerId,
            });

            currentAssigneeId = newAssigneeId;
            ticket.AssignedTo = currentAssigneeId;
            ticket.UpdatedAt = reassignTimestamp;
            ticket.UpdatedBy = reassigningManagerId;
            lastEventTimestamp = reassignTimestamp;
        }

        if (finalStatus == TicketStatus.InProgress)
        {
            return (ticket, histories);
        }

        // --- Assigned Agent resolves and/or closes the ticket ---
        // Per the scenario doc, the agent can go InProgress -> Resolved -> Closed, or
        // directly InProgress -> Closed. 70% take the Resolved step first.
        var goesThroughResolved = finalStatus == TicketStatus.Resolved || random.NextDouble() < 0.70;

        if (goesThroughResolved)
        {
            var resolvedAt = lastEventTimestamp.AddMinutes(random.Next(120, 4320));
            histories.Add(new TicketHistory
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                Action = TicketHistoryAction.StatusChanged,
                OldValue = nameof(TicketStatus.InProgress),
                NewValue = nameof(TicketStatus.Resolved),
                ChangedBy = currentAssigneeId,
                Timestamp = resolvedAt,
                CreatedAt = resolvedAt,
                CreatedBy = currentAssigneeId,
            });

            ticket.Status = TicketStatus.Resolved;
            ticket.UpdatedAt = resolvedAt;
            ticket.UpdatedBy = currentAssigneeId;
            lastEventTimestamp = resolvedAt;

            if (finalStatus == TicketStatus.Resolved)
            {
                return (ticket, histories);
            }
        }

        // --- Close the ticket ---
        var closedAt = random.NextDouble() < 0.85
            ? lastEventTimestamp.AddMinutes(random.Next(10, 2880)) // on-time: SLA usually met
            : estimatedDueDate.AddHours(random.Next(1, 48));       // late: 15% intentionally miss SLA

        histories.Add(new TicketHistory
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            Action = TicketHistoryAction.StatusChanged,
            OldValue = goesThroughResolved ? nameof(TicketStatus.Resolved) : nameof(TicketStatus.InProgress),
            NewValue = nameof(TicketStatus.Closed),
            ChangedBy = currentAssigneeId,
            Timestamp = closedAt,
            CreatedAt = closedAt,
            CreatedBy = currentAssigneeId,
        });

        ticket.Status = TicketStatus.Closed;
        ticket.ClosedAt = closedAt;
        ticket.UpdatedAt = closedAt;
        ticket.UpdatedBy = currentAssigneeId;

        return (ticket, histories);
    }

    /// <summary>
    /// Older tickets are more likely to have progressed further in their lifecycle; recent
    /// tickets are more likely to still be Open/InProgress.
    /// </summary>
    private static TicketStatus DetermineFinalStatus(Random random, int ageInDays)
    {
        if (ageInDays <= 1) return random.NextDouble() < 0.5 ? TicketStatus.Open : TicketStatus.InProgress;
        if (ageInDays <= 3) return WeightedPick(random, (TicketStatus.Open, 0.20), (TicketStatus.InProgress, 0.45), (TicketStatus.Resolved, 0.35));
        if (ageInDays <= 10) return WeightedPick(random, (TicketStatus.InProgress, 0.20), (TicketStatus.Resolved, 0.25), (TicketStatus.Closed, 0.55));

        return WeightedPick(random, (TicketStatus.Resolved, 0.10), (TicketStatus.Closed, 0.90));
    }

    private static TicketPriority WeightedPriority(Random random) =>
        WeightedPick(random, (TicketPriority.Low, 0.40), (TicketPriority.Medium, 0.40), (TicketPriority.High, 0.20));

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