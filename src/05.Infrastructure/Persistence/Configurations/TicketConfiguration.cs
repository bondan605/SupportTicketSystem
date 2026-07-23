using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.ToTable("Tickets");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.TicketNumber)
                .IsRequired()
                .HasMaxLength(10);
            entity.HasIndex(e => e.TicketNumber)
                .IsUnique(); // Business Rule: Unique TKT-XXXXX

            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CustomerEmail)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            // One-to-Many Relationship: User (Manager/Agent) to Tickets
            entity.HasOne(t => t.Assignee)
                  .WithMany(u => u.AssignedTickets)
                  .HasForeignKey(t => t.AssignedTo)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent accidental user deletion
        }
    }
}
