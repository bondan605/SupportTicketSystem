using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public static readonly Guid Ticket1Id = Guid.Parse("E1111111-1111-1111-1111-111111111111");
        public static readonly Guid Ticket2Id = Guid.Parse("E2222222-2222-2222-2222-222222222222");
        public static readonly Guid Ticket3Id = Guid.Parse("E3333333-3333-3333-3333-333333333333");

        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.ToTable("Tickets");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.TicketNumber)
                .IsRequired()
                .HasMaxLength(10);
            //entity.HasIndex(e => e.TicketNumber)
            //    .IsUnique(); // Business Rule: Unique TKT-XXXXX

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
                .HasDefaultValue(Domain.Enums.TicketStatus.Open)
                .IsRequired();

            // One-to-Many Relationship: User (Manager/Agent) to Tickets
            entity.HasOne(t => t.Assignee)
                  .WithMany(u => u.AssignedTickets)
                  .HasForeignKey(t => t.AssignedTo)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent accidental user deletion


            entity.HasMany<TicketHistory>()
                .WithOne(th => th.Ticket)
                .HasForeignKey(th => th.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(t => new { t.CreatedDate, t.Status, t.AssignedTo });

            // Data Seeding
            // Seed Data
            entity.HasData(
                // Scenario 1: Unassigned "Open" Ticket (New Issue)
                new Ticket
                {
                    Id = Ticket1Id,
                    TicketNumber = "TKT-00001",
                    Title = "System Down",
                    CustomerName = "John Doe",
                    CustomerEmail = "john@client.com",
                    Description = "Urgent: Server is not responding.",
                    Status = TicketStatus.Open,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                // Scenario 2: "In Progress" assigned to Budi Agent (Test Workload)
                new Ticket
                {
                    Id = Ticket2Id,
                    TicketNumber = "TKT-00002",
                    Title = "UI Bug",
                    CustomerName = "Jane Smith",
                    CustomerEmail = "jane@client.com",
                    Description = "Button color is wrong on dark mode.",
                    Status = TicketStatus.InProgress,
                    AssignedTo = Guid.Parse("B2C3D4E5-F6A7-4B6C-9D0E-1F2A3B4C5D6E"), // Budi Agent
                    CreatedAt = DateTime.UtcNow.AddHours(-12)
                },
                // Scenario 3: "Closed" Ticket (Test Business Rule: Cannot be modified)
                new Ticket
                {
                    Id = Ticket3Id,
                    TicketNumber = "TKT-00003",
                    Title = "Password Reset",
                    CustomerName = "Mark Lee",
                    CustomerEmail = "mark@client.com",
                    Description = "User forgot password.",
                    Status = TicketStatus.Closed,
                    AssignedTo = Guid.Parse("B2C3D4E5-F6A7-4B6C-9D0E-1F2A3B4C5D6E"), // Budi Agent
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                // Scenario 4: Assigned to Alice Johnson (Manager Report Filter Test)
                new Ticket
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "TKT-00004",
                    Title = "Payment Issue",
                    CustomerName = "Emily Blunt",
                    CustomerEmail = "emily@client.com",
                    Description = "Credit card rejected.",
                    Status = TicketStatus.InProgress,
                    AssignedTo = Guid.Parse("E5F6A7B8-C9D0-4E1F-B2A3-B4C5D6E7F8A9"), // Alice Johnson
                    CreatedAt = DateTime.UtcNow.AddHours(-5)
                },
                // Scenario 5: Multiple tickets for Budi to test Manager visibility
                new Ticket
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "TKT-00005",
                    Title = "Export Failure",
                    CustomerName = "Kevin Hart",
                    CustomerEmail = "kevin@client.com",
                    Description = "CSV export is empty.",
                    Status = TicketStatus.Resolved,
                    AssignedTo = Guid.Parse("B2C3D4E5-F6A7-4B6C-9D0E-1F2A3B4C5D6E"), // Budi Agent
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                }
            );
        }
    }
}
