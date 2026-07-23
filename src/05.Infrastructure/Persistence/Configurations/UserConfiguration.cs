using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.Property(e => e.Role)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Data seeding
            entity.HasData(
                // --- MANAGERS ---
                new User
                {
                    Id = Guid.Parse("A1B2C3D4-E5F6-4A5B-8C9D-0E1F2A3B4C5D"),
                    Name = "Azwar Manager",
                    Email = "azwar@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.Manager
                },
                new User
                {
                    Id = Guid.Parse("C3D4E5F6-A7B8-4C9D-8E1F-2A3B4C5D6E7F"),
                    Name = "Sarah Miller",
                    Email = "sarah.m@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.Manager
                },
                new User
                {
                    Id = Guid.Parse("D4E5F6A7-B8C9-4D0E-9F2A-3B4C5D6E7F8A"),
                    Name = "David Chen",
                    Email = "david.c@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.Manager
                },

                // --- SUPPORT AGENTS ---
                new User
                {
                    Id = Guid.Parse("B2C3D4E5-F6A7-4B6C-9D0E-1F2A3B4C5D6E"),
                    Name = "Budi Agent",
                    Email = "budi@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("E5F6A7B8-C9D0-4E1F-B2A3-B4C5D6E7F8A9"),
                    Name = "Alice Johnson",
                    Email = "alice.j@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("F6A7B8C9-D0E1-4F2A-C3B4-C5D6E7F8A9B0"),
                    Name = "Robert Smith",
                    Email = "robert.s@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("0A1B2C3D-4E5F-6A7B-8C9D-0E1F2A3B4C5D"),
                    Name = "Emily Davis",
                    Email = "emily.d@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("1B2C3D4E-5F6A-7B8C-9D0E-1F2A3B4C5D6E"),
                    Name = "Michael Brown",
                    Email = "michael.b@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("2C3D4E5F-6A7B-8C9D-0E1F-2A3B4C5D6E7F"),
                    Name = "Jessica Wilson",
                    Email = "jessica.w@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                },
                new User
                {
                    Id = Guid.Parse("3D4E5F6A-7B8C-9D0E-1F2A-3B4C5D6E7F8A"),
                    Name = "Kevin Lee",
                    Email = "kevin.l@support.com",
                    PasswordHash = "hashed_pass",
                    Role = UserRole.SupportAgent
                }
            );
        }
    }
}
