using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterApplicationUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmEmailDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RandomCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "1799dce8-8e5c-47a9-8d2a-41b90a70aec2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63c3dbbc-7783-48ba-9911-0f8c1997aa0a", "AQAAAAEAACcQAAAAEKON6rfK3ahQXtnZhNpXxSRRuBvy6ovGh0ZjIqCPmkneVDVcJguPKIw9s7pxkJ+0sQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmEmailDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RandomCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "3c39f65b-f2ab-4027-898e-02074d230c15");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ffa2876-c906-471d-9d9a-61a1d8175b10", "AQAAAAEAACcQAAAAEOsy7SkcENcYvNTYVzheE/cJ/lqOAtZAcWBLu886uDDk+J/AxCyJoLyHzdmvggtFsQ==" });
        }
    }
}
