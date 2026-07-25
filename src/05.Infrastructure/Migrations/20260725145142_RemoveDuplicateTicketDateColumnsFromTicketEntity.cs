using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateTicketDateColumnsFromTicketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "TicketHistories",
                columns: new[] { "Id", "Action", "ChangedBy", "CreatedAt", "CreatedBy", "NewStatus", "PreviousStatus", "TicketId", "Timestamp", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2dda9a3d-0e45-4677-9b7a-ae8fc32643bb"), "StatusChanged", new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8118), null, "Closed", "InProgress", new Guid("e3333333-3333-3333-3333-333333333333"), new DateTime(2026, 7, 25, 12, 51, 40, 146, DateTimeKind.Utc).AddTicks(8122), null, null },
                    { new Guid("56a16890-63fe-4072-9b0b-8cce81e68090"), "Assigned", new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8104), null, "InProgress", null, new Guid("e2222222-2222-2222-2222-222222222222"), new DateTime(2026, 7, 25, 4, 51, 40, 146, DateTimeKind.Utc).AddTicks(8117), null, null },
                    { new Guid("eac5c32e-6975-4447-9873-18e71281566d"), "Created", new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 7, 25, 21, 51, 40, 146, DateTimeKind.Local).AddTicks(8083), null, "Open", null, new Guid("e1111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 24, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(8101), null, null }
                });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 24, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1349));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 25, 2, 51, 40, 146, DateTimeKind.Utc).AddTicks(1370));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("e3333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 7, 22, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1374));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedTo", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "Status", "TicketNumber", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("4aac1410-ea76-4ad0-b9b1-2c85b60eaa4e"), new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 7, 23, 14, 51, 40, 146, DateTimeKind.Utc).AddTicks(1413), null, "kevin@client.com", "Kevin Hart", "CSV export is empty.", "Resolved", "TKT-00005", "Export Failure", null, null },
                    { new Guid("f9eda27a-109d-41e0-a9e2-d3f8f3466a96"), new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"), new DateTime(2026, 7, 25, 9, 51, 40, 146, DateTimeKind.Utc).AddTicks(1410), null, "emily@client.com", "Emily Blunt", "Credit card rejected.", "InProgress", "TKT-00004", "Payment Issue", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 780, DateTimeKind.Local).AddTicks(4781), "$2a$11$o57C7LlmS3tspQLBoW8VpulAziG12FSlA/cnXNfPKup4UEKqw718C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 886, DateTimeKind.Local).AddTicks(4531), "$2a$11$Yn4M3HHdmw8Rblzq3jaWhedKzvp7a.10CVTy5BAIIWv6pbQ0eV92G" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c3d4e5f-6a7b-8c9d-0e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 992, DateTimeKind.Local).AddTicks(1933), "$2a$11$VfIEcel2EoL6XFWBVQJdmOT9x..9kARerLb4/ULXzEtWhiMAMxDom" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d4e5f6a-7b8c-9d0e-1f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 41, 97, DateTimeKind.Local).AddTicks(8028), "$2a$11$ZNAYuwTD2p72o5idHzBPp.BKwvoS9J.yllThCrBe2cpUAPEka6q4q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 147, DateTimeKind.Local).AddTicks(684), "$2a$11$N0aoXbxp/Kxc.8Sy0WpqRuyG5PRgc2yXJ7YqccLZimchZN/37m7Em" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b6c-9d0e-1f2a3b4c5d6e"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 464, DateTimeKind.Local).AddTicks(5986), "$2a$11$6nEynZ9aN6TGCfNfCsfXG.OMALh4wZ05./hdZCp.OeU16yA3CaN6K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-8e1f-2a3b4c5d6e7f"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 253, DateTimeKind.Local).AddTicks(508), "$2a$11$lrYkuSGoVflai2/DyghSyOYrsh3YBrs/n6RHOyxHFK1F2Iz0oLOGa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-9f2a-3b4c5d6e7f8a"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 359, DateTimeKind.Local).AddTicks(1423), "$2a$11$AHsqqbzy0m7F5A8i1D.pgu7xIWiXvkG89vePJlNCAK4gWsjwR.HJu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-b2a3-b4c5d6e7f8a9"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 569, DateTimeKind.Local).AddTicks(427), "$2a$11$8P7OX.I6TlhaqnxO9sgK7OG4R1mnyE2JB1cpeduOsuN/cRcYjAaBe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6a7b8c9-d0e1-4f2a-c3b4-c5d6e7f8a9b0"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 25, 21, 51, 40, 673, DateTimeKind.Local).AddTicks(9135), "$2a$11$4DgyzAPRzJ8s82wQKlITx.Y8DcFzN5HdkyK6MkWlwi.E1eoTnaC1a" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedAt_Status_AssignedTo",
                table: "Tickets",
                columns: new[] { "CreatedAt", "Status", "AssignedTo" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketNumber",
                table: "Tickets",
                column: "TicketNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_CreatedAt_Status_AssignedTo",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketNumber",
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
    }
}
