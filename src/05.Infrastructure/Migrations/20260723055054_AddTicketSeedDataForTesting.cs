using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketSeedDataForTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1d9c3923-e771-4b54-8a3c-c8955f18b35f"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 21, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(141), null, "kevin@client.com", "Kevin Hart", "CSV export is empty.", "Resolved", "TKT-00005", "Export Failure", null, null },
                    { new Guid("939be3ad-6997-4e92-9724-e06191b9978c"), new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 23, 0, 50, 53, 594, DateTimeKind.Utc).AddTicks(138), null, "emily@client.com", "Emily Blunt", "Credit card rejected.", "InProgress", "TKT-00004", "Payment Issue", null, null },
                    { new Guid("e1111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 7, 22, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(87), null, "john@client.com", "John Doe", "Urgent: Server is not responding.", "Open", "TKT-00001", "System Down", null, null },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 22, 17, 50, 53, 594, DateTimeKind.Utc).AddTicks(106), null, "jane@client.com", "Jane Smith", "Button color is wrong on dark mode.", "InProgress", "TKT-00002", "UI Bug", null, null },
                    { new Guid("e3333333-3333-3333-3333-333333333333"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 20, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(110), null, "mark@client.com", "Mark Lee", "User forgot password.", "Closed", "TKT-00003", "Password Reset", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7647));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7630));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7631));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7636));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7637));

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0e669024-e40b-4a0f-a80d-a2874fff6f57"), "Assigned", new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5381), null, "InProgress", null, new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 22, 19, 50, 53, 594, DateTimeKind.Utc).AddTicks(5383), null, null },
                    { new Guid("e512de76-4d2e-4845-a3f5-f1fa7d06650c"), "StatusChanged", new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5385), null, "Closed", "InProgress", new Guid("e3333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 23, 3, 50, 53, 594, DateTimeKind.Utc).AddTicks(5387), null, null },
                    { new Guid("e5bd5fc0-5a77-486e-bc93-c7bfef492444"), "Created", new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5360), null, "Open", null, new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 22, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(5379), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("0e669024-e40b-4a0f-a80d-a2874fff6f57"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("e512de76-4d2e-4845-a3f5-f1fa7d06650c"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("e5bd5fc0-5a77-486e-bc93-c7bfef492444"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("1d9c3923-e771-4b54-8a3c-c8955f18b35f"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("939be3ad-6997-4e92-9724-e06191b9978c"));

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7067));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7068));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7029));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7062));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7060));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7064));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 23, 12, 5, 1, 19, DateTimeKind.Local).AddTicks(7065));
        }
    }
}
