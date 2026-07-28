using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Fluent API configuration for the <see cref="Ticket"/> entity: schema constraints,
    /// indexes, relationships, and seed data.
    /// </summary>
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            // TicketNumber must follow the "TKT-XXXXX" format (enforced at the application/
            // service layer via regex, not at the database level). Here we only enforce
            // length and uniqueness.
            builder.Property(t => t.TicketNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(t => t.TicketNumber)
                .IsUnique();

            builder.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.CustomerEmail)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.CustomerPhone)
                .HasMaxLength(20);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(t => t.Priority)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(t => t.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(t => t.Category)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(t => t.Impact)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(t => t.Application)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            // A ticket may be unassigned (AssignedTo is null), so the relationship to User
            // is optional. If the assigned user is deleted, we restrict deletion rather than
            // cascade, to avoid silently losing ticket ownership history.
            builder.HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // A ticket can have many history entries. If the ticket is deleted, its history
            // is deleted along with it (cascade), since history has no meaning without the
            // parent ticket.
            builder.HasMany(t => t.Histories)
                .WithOne(h => h.Ticket)
                .HasForeignKey(h => h.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}