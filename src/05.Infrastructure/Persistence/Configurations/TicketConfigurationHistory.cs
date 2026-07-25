using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Fluent API configuration for the <see cref="TicketHistory"/> entity: schema constraints,
    /// relationships, and seed data.
    /// </summary>
    public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            builder.ToTable("TicketHistories");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(h => h.OldValue)
                .HasMaxLength(255);

            builder.Property(h => h.NewValue)
                .HasMaxLength(255);

            builder.Property(h => h.Note)
                .HasMaxLength(1000);

            builder.Property(h => h.Timestamp)
                .IsRequired();

            // Relationship to Ticket is configured from the Ticket side (see
            // TicketConfiguration: HasMany(t => t.Histories) with cascade delete), so it is
            // intentionally not repeated here to avoid conflicting configuration.

            // A history entry must always record who made the change. If that user is
            // deleted, we restrict deletion rather than cascade, so historical records are
            // never silently lost.
            builder.HasOne(h => h.ChangedByUser)
                .WithMany()
                .HasForeignKey(h => h.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasData(GetSeedData());
        }

        /// <summary>
        /// Seed data representing a realistic activity trail across several seeded tickets:
        /// a mix of status changes, assignee changes, priority changes, and comments.
        /// Note: TicketId and ChangedBy values reference fixed Ids that must match the Ids
        /// used in TicketConfiguration and UserConfiguration seed data.
        /// </summary>
        private static List<TicketHistory> GetSeedData()
        {
            // Fixed user Ids, must stay in sync with UserConfiguration seed data.
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var andiId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var sitiId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var budiId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var rizkyId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            // Fixed ticket Ids, must stay in sync with TicketConfiguration seed data.
            var ticket1 = Guid.Parse("a0000000-0000-0000-0000-000000000001"); // TKT-00001
            var ticket2 = Guid.Parse("a0000000-0000-0000-0000-000000000002"); // TKT-00002
            var ticket4 = Guid.Parse("a0000000-0000-0000-0000-000000000004"); // TKT-00004
            var ticket5 = Guid.Parse("a0000000-0000-0000-0000-000000000005"); // TKT-00005
            var ticket6 = Guid.Parse("a0000000-0000-0000-0000-000000000006"); // TKT-00006
            var ticket8 = Guid.Parse("a0000000-0000-0000-0000-000000000008"); // TKT-00008

            var seedDate = new DateTime(2026, 7, 1, 9, 0, 0, DateTimeKind.Utc);

            return new List<TicketHistory>
            {
                // TKT-00001: created, then assigned
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000001"),
                    TicketId = ticket1,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate,
                    CreatedAt = seedDate,
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000002"),
                    TicketId = ticket1,
                    Action = "AssigneeChanged",
                    OldValue = "Unassigned",
                    NewValue = "Andi Pratama",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddMinutes(15),
                    CreatedAt = seedDate.AddMinutes(15),
                },

                // TKT-00002: created, status changed to InProgress, priority changed
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000003"),
                    TicketId = ticket2,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-1),
                    CreatedAt = seedDate.AddDays(-1),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000004"),
                    TicketId = ticket2,
                    Action = "PriorityChanged",
                    OldValue = "Low",
                    NewValue = "Medium",
                    ChangedBy = sitiId,
                    Timestamp = seedDate.AddDays(-1).AddHours(1),
                    CreatedAt = seedDate.AddDays(-1).AddHours(1),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000005"),
                    TicketId = ticket2,
                    Action = "StatusChanged",
                    OldValue = "Open",
                    NewValue = "InProgress",
                    ChangedBy = sitiId,
                    Timestamp = seedDate.AddDays(-1).AddHours(2),
                    CreatedAt = seedDate.AddDays(-1).AddHours(2),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000006"),
                    TicketId = ticket2,
                    Action = "CommentAdded",
                    Note = "User sudah mencoba solusi tetapi masih gagal.",
                    ChangedBy = sitiId,
                    Timestamp = seedDate.AddDays(-1).AddHours(3),
                    CreatedAt = seedDate.AddDays(-1).AddHours(3),
                },

                // TKT-00004: full lifecycle to Closed
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000007"),
                    TicketId = ticket4,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-5),
                    CreatedAt = seedDate.AddDays(-5),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000008"),
                    TicketId = ticket4,
                    Action = "StatusChanged",
                    OldValue = "Open",
                    NewValue = "InProgress",
                    ChangedBy = budiId,
                    Timestamp = seedDate.AddDays(-4),
                    CreatedAt = seedDate.AddDays(-4),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000009"),
                    TicketId = ticket4,
                    Action = "StatusChanged",
                    OldValue = "InProgress",
                    NewValue = "Resolved",
                    ChangedBy = budiId,
                    Timestamp = seedDate.AddDays(-3).AddHours(-2),
                    CreatedAt = seedDate.AddDays(-3).AddHours(-2),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000a"),
                    TicketId = ticket4,
                    Action = "StatusChanged",
                    OldValue = "Resolved",
                    NewValue = "Closed",
                    ChangedBy = budiId,
                    Timestamp = seedDate.AddDays(-3),
                    CreatedAt = seedDate.AddDays(-3),
                },

                // TKT-00005: created, resolved, closed
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000b"),
                    TicketId = ticket5,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-6),
                    CreatedAt = seedDate.AddDays(-6),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000c"),
                    TicketId = ticket5,
                    Action = "StatusChanged",
                    OldValue = "Open",
                    NewValue = "Closed",
                    ChangedBy = rizkyId,
                    Timestamp = seedDate.AddDays(-5),
                    CreatedAt = seedDate.AddDays(-5),
                },

                // TKT-00006: created, high impact, assignee changed
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000d"),
                    TicketId = ticket6,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-4),
                    CreatedAt = seedDate.AddDays(-4),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000e"),
                    TicketId = ticket6,
                    Action = "AssigneeChanged",
                    OldValue = "Unassigned",
                    NewValue = "Andi Pratama",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-4).AddMinutes(10),
                    CreatedAt = seedDate.AddDays(-4).AddMinutes(10),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-00000000000f"),
                    TicketId = ticket6,
                    Action = "StatusChanged",
                    OldValue = "Open",
                    NewValue = "InProgress",
                    ChangedBy = andiId,
                    Timestamp = seedDate.AddDays(-4).AddHours(1),
                    CreatedAt = seedDate.AddDays(-4).AddHours(1),
                },

                // TKT-00008: created, resolved and closed quickly
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000010"),
                    TicketId = ticket8,
                    Action = "TicketCreated",
                    Note = "Ticket created by Admin User.",
                    ChangedBy = adminId,
                    Timestamp = seedDate.AddDays(-7),
                    CreatedAt = seedDate.AddDays(-7),
                },
                new TicketHistory
                {
                    Id = Guid.Parse("b0000000-0000-0000-0000-000000000011"),
                    TicketId = ticket8,
                    Action = "StatusChanged",
                    OldValue = "Open",
                    NewValue = "Closed",
                    ChangedBy = budiId,
                    Timestamp = seedDate.AddDays(-7).AddHours(2),
                    CreatedAt = seedDate.AddDays(-7).AddHours(2),
                },
            };
        }
    }
}