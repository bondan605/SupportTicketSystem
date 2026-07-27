using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

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
                .HasConversion<string>()
                .HasMaxLength(30);

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
        }
    }
}