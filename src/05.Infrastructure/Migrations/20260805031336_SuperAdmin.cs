using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$8wvhMRhDF0Cr07JsJvw2p.Vtm73a/8niPYtuY.eN.7jr66AdjWj6G");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$Rc/YPi/V.YZRPdDVOoFZl.4mx.y0wBconDL2pAiK9.WGm1vjuFvN6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$A9Bxmn/Nht1xdLfS7Zc2/OCF/HjK2LHQ1W4g1zlHvPEP/MEl51T/G");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "PasswordHash",
                value: "$2a$11$Q1KbGZvcNzP6qg0E.Ttwe.Bje6aJ6jOTf2u1fN2mZ/1jaItpB5VZm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "PasswordHash",
                value: "$2a$11$I2ay5uWFvYJjaALqa96LAesoDAXQdNlDRtt44giUN0GDjzbrRqAbO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "PasswordHash",
                value: "$2a$11$8PcVLLhZVRChldP.f/Y4sO/4GqfEyBUrzTyCGJC.c4QKZG1nGKJYm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "PasswordHash",
                value: "$2a$11$7YPLNE7IM0vVVddTSNt1AuHXA157mNmzF2CLDlaqX5hetwPLgYZLO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "PasswordHash",
                value: "$2a$11$/KRCzU3Xw9KsvYR.OGR7aOEsQMnEg6thz5Fu4tkZ1.YJLCuAotZ3.");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarUrl", "BirthDate", "CreatedAt", "CreatedBy", "Email", "IsActive", "JobTitle", "LastLoginAt", "Name", "PasswordHash", "PhoneNumber", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), "Jl. Jenderal Sudirman No. 10, Jakarta Pusat, DKI Jakarta", null, null, new DateTime(2026, 6, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, "superadmin@company.com", true, "Administrator", null, "Super Admin", "$2a$11$fi7TEATv4vtZHPxzFKEI7ueFFFrb29zLM45cr6bPhgOztUmn0oS5W", "0812-3456-7890", "SuperAdmin", null, null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

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
    }
}
