using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "Name", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7067), null, "emily.d@support.com", "Emily Davis", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7068), null, "michael.b@support.com", "Michael Brown", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7070), null, "jessica.w@support.com", "Jessica Wilson", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7071), null, "kevin.l@support.com", "Kevin Lee", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7029), null, "azwar@support.com", "Azwar Manager", "hashed_pass", "Manager", null, null },
                    { new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7062), null, "budi@support.com", "Budi Agent", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7057), null, "sarah.m@support.com", "Sarah Miller", "hashed_pass", "Manager", null, null },
                    { new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7060), null, "david.c@support.com", "David Chen", "hashed_pass", "Manager", null, null },
                    { new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7064), null, "alice.j@support.com", "Alice Johnson", "hashed_pass", "SupportAgent", null, null },
                    { new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"), new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7065), null, "robert.s@support.com", "Robert Smith", "hashed_pass", "SupportAgent", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"));
        }
    }
}
