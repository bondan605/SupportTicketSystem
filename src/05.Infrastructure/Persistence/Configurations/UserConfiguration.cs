using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Fluent API configuration for the <see cref="User"/> entity: schema constraints,
    /// indexes, and seed data.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(u => u.JobTitle)
                .HasMaxLength(100);

            builder.Property(u => u.Address)
                .HasMaxLength(500);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(500);

            builder.HasData(GetSeedData());
        }

        /// <summary>
        /// Seed data representing a realistic team of managers and support agents for
        /// demo/presentation purposes. Ids here must stay in sync with the AssignedTo/
        /// CreatedBy references used in TicketConfiguration's seed data.
        /// Note: PasswordHash values below are placeholder hashes for demo/seed purposes only
        /// and must never be used as real credentials in a production environment.
        /// </summary>
        private static List<User> GetSeedData()
        {
            var seedDate = new DateTime(2026, 6, 10, 8, 0, 0, DateTimeKind.Utc);

            return new List<User>
            {
                new User
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "Super Admin",
                    Username = "superadmin",
                    Email = "superadmin@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SuperAdmin,
                    PhoneNumber = "0812-3456-7890",
                    JobTitle = "Administrator",
                    Address = "Jl. Jenderal Sudirman No. 10, Jakarta Pusat, DKI Jakarta",
                    IsActive = true,
                    CreatedAt = seedDate,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin User",
                    Username = "admin",
                    Email = "admin@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.Manager,
                    PhoneNumber = "0812-3456-7890",
                    JobTitle = "System Administrator",
                    Address = "Jl. Jenderal Sudirman No. 10, Jakarta Pusat, DKI Jakarta",
                    IsActive = true,
                    CreatedAt = seedDate,
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Andi Pratama",
                    Username = "andi.pratama",
                    Email = "andi.pratama@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    PhoneNumber = "0812-3456-7891",
                    JobTitle = "Support Agent",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(2),
                },
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Siti Aisyah",
                    Username = "siti.aisyah",
                    Email = "siti.aisyah@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    PhoneNumber = "0812-3456-7892",
                    JobTitle = "Support Agent",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(2),
                },
                new User
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Budi Santoso",
                    Username = "budi.santoso",
                    Email = "budi.santoso@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    PhoneNumber = "0812-3456-7893",
                    JobTitle = "Support Agent",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(3),
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Dewi Lestari",
                    Username = "dewi.lestari",
                    Email = "dewi.lestari@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    PhoneNumber = "0812-3456-7894",
                    JobTitle = "Support Agent",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(4),
                },
                new User
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Name = "Rizky Hidayat",
                    Username = "rizky.hidayat",
                    Email = "rizky.hidayat@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    PhoneNumber = "0812-3456-7895",
                    JobTitle = "Support Agent",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(3),
                },
                new User
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Name = "Nanda Triana",
                    Username = "nanda.triana",
                    Email = "nanda.triana@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.Manager,
                    PhoneNumber = "0812-3456-7896",
                    JobTitle = "Support Manager",
                    IsActive = true,
                    CreatedAt = seedDate.AddDays(6),
                },
                new User
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Name = "Support Team",
                    Username = "support.team",
                    Email = "support@company.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    Role = UserRole.SupportAgent,
                    IsActive = false,
                    CreatedAt = seedDate.AddDays(5),
                },
            };
        }
    }
}