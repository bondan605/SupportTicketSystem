using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStaticTicketSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$2kB4X/KTS1.q2/5K4qKRE.fokwVuKXgUq8iExOcxNMQalpoJMpxSy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$sTlIDIxj9QU731T1aAZEveg/T9eYx1moUhjMsgbmNWBzmVZPkYNuG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$DkcBUWxh6SvZA4TiuTHSceVb1Nfz/MeA0T2A4B3AG2bHxBD3ttlL2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "PasswordHash",
                value: "$2a$11$0/Ci7WD6VhpHef8kL82NE.GhvxyVXUbMxHGEHf.JsdaUor28xxCR2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "PasswordHash",
                value: "$2a$11$P9pP5ZR/2LA3sQPPK8WsUOe/KGzm.z5NkZjp6ALtrh9fiP0.XXTG6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "PasswordHash",
                value: "$2a$11$Hay7tHNY8aYF6S9E4P/owOxVetq2GNpHmF0v3rlxxBZyvEGoiOxoa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "PasswordHash",
                value: "$2a$11$upjme009kxPRAvBVePgJpOWvNzIUBbC5Wk.lG0aWBBFqV1fhnVNEO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "PasswordHash",
                value: "$2a$11$qnq7NpCJ8xylc4rkDWqVXOIKvhXOTiWR8geb.wI5Xa9YyWgbNQRPG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Application", "AssignedTo", "Category", "ClosedAt", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "CustomerPhone", "Description", "EstimatedDueDate", "Impact", "Priority", "Status", "TicketNumber", "Title", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), "InternalPortal", new Guid("22222222-2222-2222-2222-222222222222"), "Access", null, new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "budi.hartono@customer.com", "Budi Hartono", "0812-3456-7801", "User tidak bisa login setelah update aplikasi versi terbaru. Muncul pesan 'Invalid credentials' meskipun password sudah benar.", null, "SingleUser", "High", "Open", "TKT-00001", "Login gagal pada aplikasi", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), "FileServer", new Guid("33333333-3333-3333-3333-333333333333"), "Application", null, new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "siti.nurhaliza@customer.com", "Siti Nurhaliza", "0812-3456-7802", "Dokumen gagal terupload dengan pesan error 'File size exceeded' padahal ukuran file di bawah batas maksimal.", null, "SomeUsers", "Medium", "InProgress", "TKT-00002", "Error saat upload dokumen", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), "ERP", null, "Access", null, new DateTime(2026, 6, 29, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "rizky.kurniawan@customer.com", "Rizky Kurniawan", "0812-3456-7803", "Membutuhkan akses ke modul laporan keuangan untuk keperluan audit bulanan.", null, "SingleUser", "Medium", "Open", "TKT-00003", "Permintaan akses baru", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), "CRM", new Guid("44444444-4444-4444-4444-444444444444"), "Application", new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "dewi.lestari@customer.com", "Dewi Lestari", "0812-3456-7804", "Saat melakukan penyimpanan data pelanggan baru pada modul CRM, sistem menampilkan pesan error 'Failed to save data' dan data tidak tersimpan di database.", null, "SomeUsers", "Low", "Closed", "TKT-00004", "Data tidak tersimpan", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), "ERP", new Guid("66666666-6666-6666-6666-666666666666"), "Report", new DateTime(2026, 6, 26, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 25, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "andika.putra@customer.com", "Andika Putra", "0812-3456-7805", "Laporan penjualan bulanan tidak muncul di dashboard, sudah dicoba refresh berkali-kali.", null, "SingleUser", "Low", "Closed", "TKT-00005", "Laporan tidak muncul", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), "CRM", new Guid("22222222-2222-2222-2222-222222222222"), "Application", null, new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "it@majusejahtera.com", "PT. Maju Sejahtera", "021-5551234", "Integrasi API antara sistem internal dengan CRM Elnusa mengembalikan error 500 sejak pagi ini.", null, "AllUsers", "High", "InProgress", "TKT-00006", "Integrasi API gagal", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), "InternalPortal", new Guid("33333333-3333-3333-3333-333333333333"), "Application", null, new DateTime(2026, 6, 27, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "joko.susilo@customer.com", "Joko Susilo", "0812-3456-7807", "Aplikasi terasa sangat lambat saat membuka halaman dashboard, terutama di jam sibuk.", null, "AllUsers", "Medium", "Open", "TKT-00007", "Aplikasi lambat", "Problem", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), "Email", new Guid("44444444-4444-4444-4444-444444444444"), "Access", new DateTime(2026, 6, 24, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 24, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "rina.wulandari@customer.com", "Rina Wulandari", "0812-3456-7808", "Lupa password dan tidak menerima email reset password setelah beberapa kali percobaan.", null, "SingleUser", "Low", "Closed", "TKT-00008", "Reset password user", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), "ERP", new Guid("66666666-6666-6666-6666-666666666666"), "Report", null, new DateTime(2026, 6, 28, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "dedi.kurniawan@customer.com", "Dedi Kurniawan", "0812-3456-7809", "Tombol export ke Excel di halaman laporan tidak merespon saat diklik.", null, "SomeUsers", "Medium", "InProgress", "TKT-00009", "Fitur tidak berfungsi", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000a"), "ERP", new Guid("22222222-2222-2222-2222-222222222222"), "Report", null, new DateTime(2026, 6, 30, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "admin@globalinti.com", "PT. Global Inti", "021-5559876", "Membutuhkan laporan rekap transaksi bulanan dalam format PDF untuk keperluan internal.", null, "SingleUser", "Low", "Open", "TKT-00010", "Permintaan laporan bulanan", "ServiceRequest", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000b"), "None", null, "Hardware", null, new DateTime(2026, 7, 1, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "amel.marsha@customer.com", "Amel Marsha", "0812-3456-7811", "Beberapa tombol pada keyboard laptop tidak berfungsi, khususnya tombol angka.", null, "SingleUser", "Medium", "Open", "TKT-00011", "Keyboard laptop rusak", "Incident", null, null },
                    { new Guid("a0000000-0000-0000-0000-00000000000c"), "HRIS", new Guid("55555555-5555-5555-5555-555555555555"), "Application", null, new DateTime(2026, 6, 23, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), "nanda.triana@customer.com", "Nanda Triana", "0812-3456-7812", "Permintaan penambahan field baru 'Nomor BPJS' pada modul data karyawan.", null, "SomeUsers", "Low", "Resolved", "TKT-00012", "Perubahan konfigurasi sistem HR", "ChangeRequest", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$G0qvvKEf3BahhV6FBijxy.7AxoRkiJBsXflX9JiAwpBXa8b7zR28C");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$Z1EA1UITehVgusUxEQgbTOQrnCY1bP5g1/UeKnlMTq6oD.n/yKSvq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$17/xlQ7p2pT9Ptc16x/vJuN6JirfTnq8l.4NUDeGaDkGDN9Ba8u5e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "PasswordHash",
                value: "$2a$11$aVb8KAoH1V9iNE5tWFqEyOwZrB2x/ZAJyB45e/A3EDE.KaLe8OUKS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "PasswordHash",
                value: "$2a$11$agkfO9pLmsil2hnNz2bspOL97gdkh2P.tantOxXfM7ZzpHrVwinke");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "PasswordHash",
                value: "$2a$11$vk9OjGoOoPLqr2wF7pyb/e7qeEG/hRbTDcM1J2vE9s9mGyZx7.RIK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "PasswordHash",
                value: "$2a$11$NcogiEbZBPFOBCyH/wVWcuxlhuKUCvxlq8eSg3d6UPSt6xu.AiBr6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "PasswordHash",
                value: "$2a$11$BXtlD2ot1GUGEVqW42C2Ke5V0jq7MPHcFih0ka9ZOpihxMCIWCGjK");

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
        }
    }
}
