using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicketSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumForActionInTicketHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "TicketHistories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "TicketHistories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$P/lO7CA.41oXAOsFw16J7Ox2IRJlctUf2/RxorywWvWOMxzrBDgFu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$kQDRxqXy24ItokVig6Ge0.NSZqIjjCKh9DmLVF5kzYyHN3J4nydb2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$jF/rAxZyePzqQnsHJb4xXe6E0CekI8K7y5bzsDYyfTHMxSmfLaXh6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "PasswordHash",
                value: "$2a$11$J9ffXjeUV/PACSWRRDxTn.jYKYypdk.dKXOYZVNISLz/j5401GaDe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "PasswordHash",
                value: "$2a$11$UHJd5eSVGaLFa3bpBXBV5ue.Jqxz8kFF2TAFzAxFWcYmYUquCoGpu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "PasswordHash",
                value: "$2a$11$FSnmWqH5vkJDg1IFtPPQvOk/SgMSr6ARbwA8b8ADUrg2aNCepB6S2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "PasswordHash",
                value: "$2a$11$Yo9SofrKhyU4vVaejPQ16OYucZMI04I8RR30YNbeVgsv8coiNQay.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "PasswordHash",
                value: "$2a$11$xtNKlJJXlNTvjmesK63KVeQAy6DWfrLB/9LfaYoesUkY/yfEtWr0u");
        }
    }
}
