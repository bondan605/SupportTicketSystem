using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> entity)
        {
            entity.ToTable("TicketHistories");

            entity.HasKey(th => th.Id);

            entity.Property(th => th.Action)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(th => th.PreviousStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(th => th.NewStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(th => th.Timestamp)
                .IsRequired();

            entity.HasOne(th => th.Ticket)
                .WithMany()
                .HasForeignKey(th => th.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(th => th.ChangedByUser)
                .WithMany()
                .HasForeignKey(th => th.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Data Seeding
            entity.HasData(
                // History for TKT-00001 (Created by Manager Azwar)
                new TicketHistory
                {
                    Id = Guid.NewGuid(),
                    TicketId = TicketConfiguration.Ticket1Id,
                    Action = "Created",
                    NewStatus = TicketStatus.Open,
                    ChangedBy = Guid.Parse("A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D"), // Azwar Manager
                    Timestamp = DateTime.UtcNow.AddDays(-1)
                },
                // History for TKT-00002 (Assigned to Budi by Manager Sarah Miller)
                new TicketHistory
                {
                    Id = Guid.NewGuid(),
                    TicketId = TicketConfiguration.Ticket2Id,
                    Action = "Assigned",
                    NewStatus = TicketStatus.InProgress,
                    ChangedBy = Guid.Parse("C3D4E5F6-A7B8-4C9D-8E1F-2A3B4C5D6E7F"), // Sarah Miller
                    Timestamp = DateTime.UtcNow.AddHours(-10)
                },
                // History for TKT-00003 (Closed by Budi Agent)
                new TicketHistory
                {
                    Id = Guid.NewGuid(),
                    TicketId = TicketConfiguration.Ticket3Id,
                    Action = "StatusChanged",
                    PreviousStatus = TicketStatus.InProgress,
                    NewStatus = TicketStatus.Closed,
                    ChangedBy = Guid.Parse("B2C3D4E5-F6A7-4B6C-9D0E-1F2A3B4C5D6E"), // Budi Agent
                    Timestamp = DateTime.UtcNow.AddHours(-2)
                }
            );
        }
    }
}