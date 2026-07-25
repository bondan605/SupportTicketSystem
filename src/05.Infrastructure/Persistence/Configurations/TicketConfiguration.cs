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

            builder.HasData(GetSeedData());
        }

        /// <summary>
        /// Seed data representing a realistic slice of ticket activity for demo/presentation
        /// purposes: a mix of statuses, priorities, categories, and assignees.
        /// Note: AssignedTo/CreatedBy values reference fixed User Ids that must match the
        /// Ids used in UserConfiguration's seed data.
        /// </summary>
        private static List<Ticket> GetSeedData()
        {
            // Fixed user Ids, must stay in sync with UserConfiguration seed data.
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var andiId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var sitiId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var budiId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var dewiId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var rizkyId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            var seedDate = new DateTime(2026, 7, 1, 9, 0, 0, DateTimeKind.Utc);

            return new List<Ticket>
            {
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"),
                    TicketNumber = "TKT-00001",
                    CustomerName = "Budi Hartono",
                    CustomerEmail = "budi.hartono@customer.com",
                    CustomerPhone = "0812-3456-7801",
                    Title = "Login gagal pada aplikasi",
                    Description = "User tidak bisa login setelah update aplikasi versi terbaru. Muncul pesan 'Invalid credentials' meskipun password sudah benar.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.High,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Access,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.InternalPortal,
                    AssignedTo = andiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate,
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"),
                    TicketNumber = "TKT-00002",
                    CustomerName = "Siti Nurhaliza",
                    CustomerEmail = "siti.nurhaliza@customer.com",
                    CustomerPhone = "0812-3456-7802",
                    Title = "Error saat upload dokumen",
                    Description = "Dokumen gagal terupload dengan pesan error 'File size exceeded' padahal ukuran file di bawah batas maksimal.",
                    Status = TicketStatus.InProgress,
                    Priority = TicketPriority.Medium,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Application,
                    Impact = TicketImpact.SomeUsers,
                    Application = TicketApplication.FileServer,
                    AssignedTo = sitiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-1),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"),
                    TicketNumber = "TKT-00003",
                    CustomerName = "Rizky Kurniawan",
                    CustomerEmail = "rizky.kurniawan@customer.com",
                    CustomerPhone = "0812-3456-7803",
                    Title = "Permintaan akses baru",
                    Description = "Membutuhkan akses ke modul laporan keuangan untuk keperluan audit bulanan.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    Type = TicketType.ServiceRequest,
                    Category = TicketCategory.Access,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.ERP,
                    AssignedTo = null,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-2),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"),
                    TicketNumber = "TKT-00004",
                    CustomerName = "Dewi Lestari",
                    CustomerEmail = "dewi.lestari@customer.com",
                    CustomerPhone = "0812-3456-7804",
                    Title = "Data tidak tersimpan",
                    Description = "Saat melakukan penyimpanan data pelanggan baru pada modul CRM, sistem menampilkan pesan error 'Failed to save data' dan data tidak tersimpan di database.",
                    Status = TicketStatus.Closed,
                    Priority = TicketPriority.Low,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Application,
                    Impact = TicketImpact.SomeUsers,
                    Application = TicketApplication.CRM,
                    AssignedTo = budiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-5),
                    ClosedAt = seedDate.AddDays(-3),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"),
                    TicketNumber = "TKT-00005",
                    CustomerName = "Andika Putra",
                    CustomerEmail = "andika.putra@customer.com",
                    CustomerPhone = "0812-3456-7805",
                    Title = "Laporan tidak muncul",
                    Description = "Laporan penjualan bulanan tidak muncul di dashboard, sudah dicoba refresh berkali-kali.",
                    Status = TicketStatus.Closed,
                    Priority = TicketPriority.Low,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Report,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.ERP,
                    AssignedTo = rizkyId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-6),
                    ClosedAt = seedDate.AddDays(-5),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"),
                    TicketNumber = "TKT-00006",
                    CustomerName = "PT. Maju Sejahtera",
                    CustomerEmail = "it@majusejahtera.com",
                    CustomerPhone = "021-5551234",
                    Title = "Integrasi API gagal",
                    Description = "Integrasi API antara sistem internal dengan CRM Elnusa mengembalikan error 500 sejak pagi ini.",
                    Status = TicketStatus.InProgress,
                    Priority = TicketPriority.High,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Application,
                    Impact = TicketImpact.AllUsers,
                    Application = TicketApplication.CRM,
                    AssignedTo = andiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-4),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"),
                    TicketNumber = "TKT-00007",
                    CustomerName = "Joko Susilo",
                    CustomerEmail = "joko.susilo@customer.com",
                    CustomerPhone = "0812-3456-7807",
                    Title = "Aplikasi lambat",
                    Description = "Aplikasi terasa sangat lambat saat membuka halaman dashboard, terutama di jam sibuk.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    Type = TicketType.Problem,
                    Category = TicketCategory.Application,
                    Impact = TicketImpact.AllUsers,
                    Application = TicketApplication.InternalPortal,
                    AssignedTo = sitiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-4),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"),
                    TicketNumber = "TKT-00008",
                    CustomerName = "Rina Wulandari",
                    CustomerEmail = "rina.wulandari@customer.com",
                    CustomerPhone = "0812-3456-7808",
                    Title = "Reset password user",
                    Description = "Lupa password dan tidak menerima email reset password setelah beberapa kali percobaan.",
                    Status = TicketStatus.Closed,
                    Priority = TicketPriority.Low,
                    Type = TicketType.ServiceRequest,
                    Category = TicketCategory.Access,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.Email,
                    AssignedTo = budiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-7),
                    ClosedAt = seedDate.AddDays(-7).AddHours(2),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"),
                    TicketNumber = "TKT-00009",
                    CustomerName = "Dedi Kurniawan",
                    CustomerEmail = "dedi.kurniawan@customer.com",
                    CustomerPhone = "0812-3456-7809",
                    Title = "Fitur tidak berfungsi",
                    Description = "Tombol export ke Excel di halaman laporan tidak merespon saat diklik.",
                    Status = TicketStatus.InProgress,
                    Priority = TicketPriority.Medium,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Report,
                    Impact = TicketImpact.SomeUsers,
                    Application = TicketApplication.ERP,
                    AssignedTo = rizkyId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-3),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-00000000000a"),
                    TicketNumber = "TKT-00010",
                    CustomerName = "PT. Global Inti",
                    CustomerEmail = "admin@globalinti.com",
                    CustomerPhone = "021-5559876",
                    Title = "Permintaan laporan bulanan",
                    Description = "Membutuhkan laporan rekap transaksi bulanan dalam format PDF untuk keperluan internal.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Low,
                    Type = TicketType.ServiceRequest,
                    Category = TicketCategory.Report,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.ERP,
                    AssignedTo = andiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-1),
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-00000000000b"),
                    TicketNumber = "TKT-00011",
                    CustomerName = "Amel Marsha",
                    CustomerEmail = "amel.marsha@customer.com",
                    CustomerPhone = "0812-3456-7811",
                    Title = "Keyboard laptop rusak",
                    Description = "Beberapa tombol pada keyboard laptop tidak berfungsi, khususnya tombol angka.",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    Type = TicketType.Incident,
                    Category = TicketCategory.Hardware,
                    Impact = TicketImpact.SingleUser,
                    Application = TicketApplication.None,
                    AssignedTo = null,
                    CreatedBy = adminId,
                    CreatedAt = seedDate,
                },
                new Ticket
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-00000000000c"),
                    TicketNumber = "TKT-00012",
                    CustomerName = "Nanda Triana",
                    CustomerEmail = "nanda.triana@customer.com",
                    CustomerPhone = "0812-3456-7812",
                    Title = "Perubahan konfigurasi sistem HR",
                    Description = "Permintaan penambahan field baru 'Nomor BPJS' pada modul data karyawan.",
                    Status = TicketStatus.Resolved,
                    Priority = TicketPriority.Low,
                    Type = TicketType.ChangeRequest,
                    Category = TicketCategory.Application,
                    Impact = TicketImpact.SomeUsers,
                    Application = TicketApplication.HRIS,
                    AssignedTo = dewiId,
                    CreatedBy = adminId,
                    CreatedAt = seedDate.AddDays(-8),
                },
            };
        }
    }
}