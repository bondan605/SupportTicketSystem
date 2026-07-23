using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bCryptInDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketNumber",
                table: "Tickets");

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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Open",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("13e1575d-40d2-48de-8546-d6c82e1a139a"), "Assigned", new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 23, 15, 1, 47, 748, DateTimeKind.Local).AddTicks(7077), null, "InProgress", null, new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 22, 22, 1, 47, 748, DateTimeKind.Utc).AddTicks(7092), null, null },
                    { new Guid("e869b1fd-98ec-4491-91a4-0e2ea26774a3"), "Created", new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 23, 15, 1, 47, 748, DateTimeKind.Local).AddTicks(7041), null, "Open", null, new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 22, 8, 1, 47, 748, DateTimeKind.Utc).AddTicks(7073), null, null },
                    { new Guid("f6fad12f-a598-4c26-85a8-138a1c5a13ed"), "StatusChanged", new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 15, 1, 47, 748, DateTimeKind.Local).AddTicks(7094), null, "Closed", "InProgress", new Guid("e3333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 23, 6, 1, 47, 748, DateTimeKind.Utc).AddTicks(7099), null, null }
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 7, 22, 8, 1, 47, 747, DateTimeKind.Utc).AddTicks(9397), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 7, 22, 20, 1, 47, 747, DateTimeKind.Utc).AddTicks(9423), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e3333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 7, 20, 8, 1, 47, 747, DateTimeKind.Utc).AddTicks(9427), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CreatedDate", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("09fdbcb2-3f03-4c45-9bfc-c54de8145340"), new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 23, 3, 1, 47, 747, DateTimeKind.Utc).AddTicks(9467), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily@client.com", "Emily Blunt", "Credit card rejected.", "InProgress", "TKT-00004", "Payment Issue", null, null, null },
                    { new Guid("f79549fc-b5bb-4542-a636-958b3645ecec"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 21, 8, 1, 47, 747, DateTimeKind.Utc).AddTicks(9472), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kevin@client.com", "Kevin Hart", "CSV export is empty.", "Resolved", "TKT-00005", "Export Failure", null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 646, DateTimeKind.Local).AddTicks(7055), "$2a$11$Cgncj7D9UZ.mla3vluy01uJ19XSgMI4bXnRpmtKwN.xhDGukDIQQK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 792, DateTimeKind.Local).AddTicks(9796), "$2a$11$PVSX39rMDRrX2yOSrW0NM.9j5sFi3AOgPgm4eWaxGcN3tORoH0Hu6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 951, DateTimeKind.Local).AddTicks(6288), "$2a$11$.rGA6a/gfUy7BdoZRf/sWuRCju5Tc73SADDVPxS/DSJ.AUtZDE2Ue" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 49, 104, DateTimeKind.Local).AddTicks(6026), "$2a$11$lYJsxB4vZXqCOQVI3UBcxuCdeR1XqF5Jqzive5TSLPxlAfSWdoxAK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 47, 749, DateTimeKind.Local).AddTicks(336), "$2a$11$Ep20NleMyZlU1PIkINzuIOlH63jAP7KfcYEc4d/hbHGIudfzlXBQe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 192, DateTimeKind.Local).AddTicks(3979), "$2a$11$82prow65i7.SEcYSyn0i7uMbTY9Ud/Fsm05DornsZG7PtdDc.1DvK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 47, 898, DateTimeKind.Local).AddTicks(1952), "$2a$11$lGT1JtyDXi3Up.pbWJeRc.OuNoRwHBtNLAxOcrCKums.Eyul7DYTq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 44, DateTimeKind.Local).AddTicks(3490), "$2a$11$1.f2a.gYHSsDUL.G47tky.lG/kbwh35SDFofSkWzvP4em/lFR.koq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 349, DateTimeKind.Local).AddTicks(9803), "$2a$11$gblKbK/eaOLhp6EPtZujNeVYjl4GUp.Gk7Hqcp.UawTYOOSpT.8Yq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 15, 1, 48, 497, DateTimeKind.Local).AddTicks(2880), "$2a$11$yX9wSQsSoO1wVsw6jL1kW.V8yoWvMOc8iUk/9NMB15a5wFMs52bvm" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedDate_Status_AssignedTo",
                table: "Tickets",
                columns: new[] { "CreatedDate", "Status", "AssignedTo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_CreatedDate_Status_AssignedTo",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("13e1575d-40d2-48de-8546-d6c82e1a139a"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("e869b1fd-98ec-4491-91a4-0e2ea26774a3"));

            migrationBuilder.DeleteData(
                table: "TicketHistories",
                keyColumn: "Id",
                keyValue: new Guid("f6fad12f-a598-4c26-85a8-138a1c5a13ed"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("09fdbcb2-3f03-4c45-9bfc-c54de8145340"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("f79549fc-b5bb-4542-a636-958b3645ecec"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Open");

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0e669024-e40b-4a0f-a80d-a2874fff6f57"), "Assigned", new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5381), null, "InProgress", null, new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 22, 19, 50, 53, 594, DateTimeKind.Utc).AddTicks(5383), null, null },
                    { new Guid("e512de76-4d2e-4845-a3f5-f1fa7d06650c"), "StatusChanged", new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5385), null, "Closed", "InProgress", new Guid("e3333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 23, 3, 50, 53, 594, DateTimeKind.Utc).AddTicks(5387), null, null },
                    { new Guid("e5bd5fc0-5a77-486e-bc93-c7bfef492444"), "Created", new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(5360), null, "Open", null, new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 22, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(5379), null, null }
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 22, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 22, 17, 50, 53, 594, DateTimeKind.Utc).AddTicks(106));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e3333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 20, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(110));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1d9c3923-e771-4b54-8a3c-c8955f18b35f"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 21, 5, 50, 53, 594, DateTimeKind.Utc).AddTicks(141), null, "kevin@client.com", "Kevin Hart", "CSV export is empty.", "Resolved", "TKT-00005", "Export Failure", null, null },
                    { new Guid("939be3ad-6997-4e92-9724-e06191b9978c"), new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 23, 0, 50, 53, 594, DateTimeKind.Utc).AddTicks(138), null, "emily@client.com", "Emily Blunt", "Credit card rejected.", "InProgress", "TKT-00004", "Payment Issue", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7640), "$2a$11$upbEQNZ6RCir2lzOKhnXLulDhkR9ZMx0a53Ehp2HEBTwRy8p5M.iW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7642), "$2a$11$dW7edZOCIgj2fcQVGsGgJuzj/c4sSItkDYSG/Jl8uAJaPicKvcP.K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7645), "$2a$11$wUQ3sMSfGSVzMPTsped24.NsNv2rwfRdRUcCMQaYbbVuSBxdFPw16" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7647), "$2a$11$tKbz7OTycPGH5S2l0R9M2uK3ozqoAMvXzTkImrVcw8wnA/EoAUVbq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7621), "$2a$11$Xh1W2gm9.FsVNiFp517c.ez0aqhvjJ9759i3HakowM7nImmlb8G06" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7634), "$2a$11$xKCx8nWqIS9gIzQkqAsl8.43zJ/Wv0GgzFZQIhh0HKMGuLGktCUQy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7630), "$2a$11$tCgzV480lTGFQ/EjtmbWJO1lHaTVk/yIvG/F2.CHqhwoP7bEP4dZC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7631), "$2a$11$lrOmdDn48fXO8SLXxPCFtunlbCjrpYv/q0eC6QytAZMQak9L5g27i" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7636), "$2a$11$cQMg4KWiFwzDToLk.e5pIO0rsSKTo91liEjKYYYawtx9u8B8D536a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 23, 12, 50, 53, 594, DateTimeKind.Local).AddTicks(7637), "$2a$11$ru/mKr2mSNIlzBvkn9Izgew2vOmNC6zv5j2.wBj4MsPlaHFwRxkC6" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketNumber",
                table: "Tickets",
                column: "TicketNumber",
                unique: true);
        }
    }
}
