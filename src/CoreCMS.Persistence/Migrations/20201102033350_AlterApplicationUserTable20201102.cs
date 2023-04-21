using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterApplicationUserTable20201102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Companyname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneCountryCode",
                table: "AspNetUsers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Companyname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneCountryCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "64a1f4b8-b14a-4618-93a1-36d79527ff74");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b3794fa-008c-4b43-9eb4-95236c11d793", "AQAAAAEAACcQAAAAEAvAFOjXXO7hyPd3xx0zuzhb+vR7vFyTxOIYHl/iJWcj5iJdPCnQwUiJNPTUq3ZFXQ==" });
        }
    }
}
