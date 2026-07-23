using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;

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
        }
    }
}