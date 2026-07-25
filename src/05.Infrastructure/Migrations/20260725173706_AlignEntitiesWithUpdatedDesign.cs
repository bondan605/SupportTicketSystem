using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignEntitiesWithUpdatedDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_CreatedAt_Status_AssignedTo",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("2dda9a3d-0e45-4677-9b7a-ae8fc32643bb"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("56a16890-63fe-4072-9b0b-8cce81e68090"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("eac5c32e-6975-4447-9873-18e71281566d"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("4aac1410-ea76-4ad0-b9b1-2c85b60eaa4e"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("f9eda27a-109d-41e0-a9e2-d3f8f3466a96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e3333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "TicketHistories");

            migrationBuilder.DropColumn(
                name: "PreviousStatus",
                table: "TicketHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tickets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TicketNumber",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Open");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Tickets",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Application",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDueDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "TicketHistories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TicketHistories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "TicketHistories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Application", "AssignedTo", "Category", "ClosedAt", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "EstimatedDueDate", "Impact", "Priority", "Status", "TicketNumber", "Title", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000003"), "ERP", null, "Access", null, new DateTime(2026, 6, 29, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "rizky.kurniawan@customer.com", "Rizky Kurniawan", "0812-3456-7803", "Membutuhkan akses ke modul laporan keuangan untuk keperluan audit bulanan.", null, "SingleUser", "Medium", "Open", "TKT-00003", "Permintaan akses baru", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000b"), "None", null, "Hardware", null, new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "amel.marsha@customer.com", "Amel Marsha", "0812-3456-7811", "Beberapa tombol pada keyboard laptop tidak berfungsi, khususnya tombol angka.", null, "SingleUser", "Medium", "Open", "TKT-00011", "Keyboard laptop rusak", "Incident", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BirthDate", "CreatedAt", "CreatedBy", "Email", "IsActive", "JobTitle", "LastLoginAt", "Name", "PasswordHash", "PhoneNumber", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Jl. Jenderal Sudirman No. 10, Jakarta Pusat, DKI Jakarta", null, null, new DateTime(2026, 6, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, "admin@company.com", true, "System Administrator", null, "Admin User", "$2a$11$P/lO7CA.41oXAOsFw16J7Ox2IRJlctUf2/RxorywWvWOMxzrBDgFu", "0812-3456-7890", "Manager", null, null, "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, null, null, new DateTime(2026, 6, 12, 8, 0, 0, 0, DateTimeKind.Utc), null, "andi.pratama@company.com", true, "Support Agent", null, "Andi Pratama", "$2a$11$kQDRxqXy24ItokVig6Ge0.NSZqIjjCKh9DmLVF5kzYyHN3J4nydb2", "0812-3456-7891", "SupportAgent", null, null, "andi.pratama" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, null, null, new DateTime(2026, 6, 12, 8, 0, 0, 0, DateTimeKind.Utc), null, "siti.aisyah@company.com", true, "Support Agent", null, "Siti Aisyah", "$2a$11$jF/rAxZyePzqQnsHJb4xXe6E0CekI8K7y5bzsDYyfTHMxSmfLaXh6", "0812-3456-7892", "SupportAgent", null, null, "siti.aisyah" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, null, null, new DateTime(2026, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), null, "budi.santoso@company.com", true, "Support Agent", null, "Budi Santoso", "$2a$11$J9ffXjeUV/PACSWRRDxTn.jYKYypdk.dKXOYZVNISLz/j5401GaDe", "0812-3456-7893", "SupportAgent", null, null, "budi.santoso" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, null, null, new DateTime(2026, 6, 14, 8, 0, 0, 0, DateTimeKind.Utc), null, "dewi.lestari@company.com", true, "Support Agent", null, "Dewi Lestari", "$2a$11$UHJd5eSVGaLFa3bpBXBV5ue.Jqxz8kFF2TAFzAxFWcYmYUquCoGpu", "0812-3456-7894", "SupportAgent", null, null, "dewi.lestari" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, null, null, new DateTime(2026, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), null, "rizky.hidayat@company.com", true, "Support Agent", null, "Rizky Hidayat", "$2a$11$FSnmWqH5vkJDg1IFtPPQvOk/SgMSr6ARbwA8b8ADUrg2aNCepB6S2", "0812-3456-7895", "SupportAgent", null, null, "rizky.hidayat" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, null, null, new DateTime(2026, 6, 16, 8, 0, 0, 0, DateTimeKind.Utc), null, "nanda.triana@company.com", true, "Support Manager", null, "Nanda Triana", "$2a$11$Yo9SofrKhyU4vVaejPQ16OYucZMI04I8RR30YNbeVgsv8coiNQay.", "0812-3456-7896", "Manager", null, null, "nanda.triana" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BirthDate", "CreatedAt", "CreatedBy", "Email", "JobTitle", "LastLoginAt", "Name", "PasswordHash", "PhoneNumber", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("88888888-8888-8888-8888-888888888888"), null, null, null, new DateTime(2026, 6, 15, 8, 0, 0, 0, DateTimeKind.Utc), null, "support@company.com", null, null, "Support Team", "$2a$11$xtNKlJJXlNTvjmesK63KVeQAy6DWfrLB/9LfaYoesUkY/yfEtWr0u", null, "SupportAgent", null, null, "support.team" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Application", "AssignedTo", "Category", "ClosedAt", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "EstimatedDueDate", "Impact", "Priority", "Status", "TicketNumber", "Title", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), "InternalPortal", new Guid("22222222-2222-2222-2222-222222222222"), "Access", null, new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "budi.hartono@customer.com", "Budi Hartono", "0812-3456-7801", "User tidak bisa login setelah update aplikasi versi terbaru. Muncul pesan 'Invalid credentials' meskipun password sudah benar.", null, "SingleUser", "High", "Open", "TKT-00001", "Login gagal pada aplikasi", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), "FileServer", new Guid("33333333-3333-3333-3333-333333333333"), "Application", null, new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "siti.nurhaliza@customer.com", "Siti Nurhaliza", "0812-3456-7802", "Dokumen gagal terupload dengan pesan error 'File size exceeded' padahal ukuran file di bawah batas maksimal.", null, "SomeUsers", "Medium", "InProgress", "TKT-00002", "Error saat upload dokumen", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), "CRM", new Guid("44444444-4444-4444-4444-444444444444"), "Application", new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "dewi.lestari@customer.com", "Dewi Lestari", "0812-3456-7804", "Saat melakukan penyimpanan data pelanggan baru pada modul CRM, sistem menampilkan pesan error 'Failed to save data' dan data tidak tersimpan di database.", null, "SomeUsers", "Low", "Closed", "TKT-00004", "Data tidak tersimpan", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), "ERP", new Guid("66666666-6666-6666-6666-666666666666"), "Report", new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 25, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "andika.putra@customer.com", "Andika Putra", "0812-3456-7805", "Laporan penjualan bulanan tidak muncul di dashboard, sudah dicoba refresh berkali-kali.", null, "SingleUser", "Low", "Closed", "TKT-00005", "Laporan tidak muncul", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), "CRM", new Guid("22222222-2222-2222-2222-222222222222"), "Application", null, new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "it@majusejahtera.com", "PT. Maju Sejahtera", "021-5551234", "Integrasi API antara sistem internal dengan CRM Elnusa mengembalikan error 500 sejak pagi ini.", null, "AllUsers", "High", "InProgress", "TKT-00006", "Integrasi API gagal", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), "InternalPortal", new Guid("33333333-3333-3333-3333-333333333333"), "Application", null, new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "joko.susilo@customer.com", "Joko Susilo", "0812-3456-7807", "Aplikasi terasa sangat lambat saat membuka halaman dashboard, terutama di jam sibuk.", null, "AllUsers", "Medium", "Open", "TKT-00007", "Aplikasi lambat", "Problem", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), "Email", new Guid("44444444-4444-4444-4444-444444444444"), "Access", new DateTime(2026, 6, 24, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 24, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "rina.wulandari@customer.com", "Rina Wulandari", "0812-3456-7808", "Lupa password dan tidak menerima email reset password setelah beberapa kali percobaan.", null, "SingleUser", "Low", "Closed", "TKT-00008", "Reset password user", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), "ERP", new Guid("66666666-6666-6666-6666-666666666666"), "Report", null, new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "dedi.kurniawan@customer.com", "Dedi Kurniawan", "0812-3456-7809", "Tombol export ke Excel di halaman laporan tidak merespon saat diklik.", null, "SomeUsers", "Medium", "InProgress", "TKT-00009", "Fitur tidak berfungsi", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000a"), "ERP", new Guid("22222222-2222-2222-2222-222222222222"), "Report", null, new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "admin@globalinti.com", "PT. Global Inti", "021-5559876", "Membutuhkan laporan rekap transaksi bulanan dalam format PDF untuk keperluan internal.", null, "SingleUser", "Low", "Open", "TKT-00010", "Permintaan laporan bulanan", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000c"), "HRIS", new Guid("55555555-5555-5555-5555-555555555555"), "Application", null, new DateTime(2026, 6, 23, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "nanda.triana@customer.com", "Nanda Triana", "0812-3456-7812", "Permintaan penambahan field baru 'Nomor BPJS' pada modul data karyawan.", null, "SomeUsers", "Low", "Resolved", "TKT-00012", "Perubahan konfigurasi sistem HR", "ChangeRequest", null, null }
                });

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewValue", "Note", "OldValue", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000000001"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000001"), new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000002"), "AssigneeChanged", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 1, 9, 15, 0, 0, DateTimeKind.Utc), null, "Andi Pratama", null, "Unassigned", new Guid("a0000000-0000-0000-0000-000000000001"), new DateTime(2026, 7, 1, 9, 15, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000003"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000004"), "PriorityChanged", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 6, 30, 10, 0, 0, 0, DateTimeKind.Utc), null, "Medium", null, "Low", new Guid("a0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 30, 10, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000005"), "StatusChanged", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 6, 30, 11, 0, 0, 0, DateTimeKind.Utc), null, "InProgress", null, "Open", new Guid("a0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 30, 11, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000006"), "CommentAdded", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 6, 30, 12, 0, 0, 0, DateTimeKind.Utc), null, null, "User sudah mencoba solusi tetapi masih gagal.", null, new Guid("a0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 30, 12, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000007"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000008"), "StatusChanged", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), null, "InProgress", null, "Open", new Guid("a0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000009"), "StatusChanged", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 6, 28, 7, 0, 0, 0, DateTimeKind.Utc), null, "Resolved", null, "InProgress", new Guid("a0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 28, 7, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000a"), "StatusChanged", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), null, "Closed", null, "Resolved", new Guid("a0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000b"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 25, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 25, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000c"), "StatusChanged", new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), null, "Closed", null, "Open", new Guid("a0000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000d"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000e"), "AssigneeChanged", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 27, 9, 10, 0, 0, DateTimeKind.Utc), null, "Andi Pratama", null, "Unassigned", new Guid("a0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 27, 9, 10, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-00000000000f"), "StatusChanged", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 6, 27, 10, 0, 0, 0, DateTimeKind.Utc), null, "InProgress", null, "Open", new Guid("a0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 27, 10, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000010"), "TicketCreated", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Ticket created by Admin User.", null, new Guid("a0000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 24, 9, 0, 0, 0, DateTimeKind.Utc), null, null },
                    { new Guid("b0000000-0000-0000-0000-000000000011"), "StatusChanged", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 6, 24, 11, 0, 0, 0, DateTimeKind.Utc), null, "Closed", null, "Open", new Guid("a0000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 24, 11, 0, 0, 0, DateTimeKind.Utc), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Application",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EstimatedDueDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "TicketHistories");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TicketHistories");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "TicketHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tickets",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TicketNumber",
                table: "Tickets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Open",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickets",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Tickets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "NewStatus",
                table: "TicketHistories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousStatus",
                table: "TicketHistories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("e1111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 7, 24, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1349), null, "john@client.com", "John Doe", "Urgent: Server is not responding.", "Open", "TKT-00001", "System Down", null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "Name", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 25, 21, 51, 40, 780, DateTimeKind.Local).AddTicks(4781), null, "emily.d@support.com", "Emily Davis", "$2a$11$o57C7LlmS3tspQLBoW8VpulAziG12FSlA/cnXNfPKup4UEKqw718C", "SupportAgent", null, null },
                    { new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 25, 21, 51, 40, 886, DateTimeKind.Local).AddTicks(4531), null, "michael.b@support.com", "Michael Brown", "$2a$11$Yn4M3HHdmw8Rblzq3jaWhedKzvp7a.10CVTy5BAIIWv6pbQ0eV92G", "SupportAgent", null, null },
                    { new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 25, 21, 51, 40, 992, DateTimeKind.Local).AddTicks(1933), null, "jessica.w@support.com", "Jessica Wilson", "$2a$11$VfIEcel2EoL6XFWBVQJdmOT9x..9kARerLb4/ULXzEtWhiMAMxDom", "SupportAgent", null, null },
                    { new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"), new DateTime(2026, 7, 25, 21, 51, 41, 97, DateTimeKind.Local).AddTicks(8028), null, "kevin.l@support.com", "Kevin Lee", "$2a$11$ZNAYuwTD2p72o5idHzBPp.BKwvoS9J.yllThCrBe2cpUAPEka6q4q", "SupportAgent", null, null },
                    { new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 25, 21, 51, 40, 147, DateTimeKind.Local).AddTicks(684), null, "azwar@support.com", "Azwar Manager", "$2a$11$N0aoXbxp/Kxc.8Sy0WpqRuyG5PRgc2yXJ7YqccLZimchZN/37m7Em", "Manager", null, null },
                    { new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 25, 21, 51, 40, 464, DateTimeKind.Local).AddTicks(5986), null, "budi@support.com", "Budi Agent", "$2a$11$6nEynZ9aN6TGCfNfCsfXG.OMALh4wZ05./hdZCp.OeU16yA3CaN6K", "SupportAgent", null, null },
                    { new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 25, 21, 51, 40, 253, DateTimeKind.Local).AddTicks(508), null, "sarah.m@support.com", "Sarah Miller", "$2a$11$lrYkuSGoVflai2/DyghSyOYrsh3YBrs/n6RHOyxHFK1F2Iz0oLOGa", "Manager", null, null },
                    { new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"), new DateTime(2026, 7, 25, 21, 51, 40, 359, DateTimeKind.Local).AddTicks(1423), null, "david.c@support.com", "David Chen", "$2a$11$AHsqqbzy0m7F5A8i1D.pgu7xIWiXvkG89vePJlNCAK4gWsjwR.HJu", "Manager", null, null },
                    { new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 25, 21, 51, 40, 569, DateTimeKind.Local).AddTicks(427), null, "alice.j@support.com", "Alice Johnson", "$2a$11$8P7OX.I6TlhaqnxO9sgK7OG4R1mnyE2JB1cpeduOsuN/cRcYjAaBe", "SupportAgent", null, null },
                    { new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"), new DateTime(2026, 7, 25, 21, 51, 40, 673, DateTimeKind.Local).AddTicks(9135), null, "robert.s@support.com", "Robert Smith", "$2a$11$4DgyzAPRzJ8s82wQKlITx.Y8DcFzN5HdkyK6MkWlwi.E1eoTnaC1a", "SupportAgent", null, null }
                });

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("eac5c32e-6975-4447-9873-18e71281566d"), "Created", new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8083), null, "Open", null, new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 24, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(8101), null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("4aac1410-ea76-4ad0-b9b1-2c85b60eaa4e"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1413), null, "kevin@client.com", "Kevin Hart", "CSV export is empty.", "Resolved", "TKT-00005", "Export Failure", null, null },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 25, 2, 51, 40, 146, DateTimeKind.Utc).AddTicks(1370), null, "jane@client.com", "Jane Smith", "Button color is wrong on dark mode.", "InProgress", "TKT-00002", "UI Bug", null, null },
                    { new Guid("e3333333-3333-3333-3333-333333333333"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 22, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1374), null, "mark@client.com", "Mark Lee", "User forgot password.", "Closed", "TKT-00003", "Password Reset", null, null },
                    { new Guid("f9eda27a-109d-41e0-a9e2-d3f8f3466a96"), new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 25, 9, 51, 40, 146, DateTimeKind.Utc).AddTicks(1410), null, "emily@client.com", "Emily Blunt", "Credit card rejected.", "InProgress", "TKT-00004", "Payment Issue", null, null }
                });

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2dda9a3d-0e45-4677-9b7a-ae8fc32643bb"), "StatusChanged", new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8118), null, "Closed", "InProgress", new Guid("e3333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 25, 12, 51, 40, 146, DateTimeKind.Utc).AddTicks(8122), null, null },
                    { new Guid("56a16890-63fe-4072-9b0b-8cce81e68090"), "Assigned", new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8104), null, "InProgress", null, new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 25, 4, 51, 40, 146, DateTimeKind.Utc).AddTicks(8117), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedAt_Status_AssignedTo",
                table: "Tickets",
                columns: new[] { "CreatedAt", "Status", "AssignedTo" });
        }
    }
}
