using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class UpdatePlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceName",
                table: "Place");

            migrationBuilder.AddColumn<string>(
                name: "PlaceName_EN",
                table: "Place",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceName_TH",
                table: "Place",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "8468cb99-20b2-4c5b-95b2-5cbc0a5ef826");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55744527-ac22-4a22-be2d-8e181e160b15", "AQAAAAEAACcQAAAAEOnKmnfK67hpZVMq+hgCxcq3ABz7KLUSHzjKSY16e8m6hCtQhrEm607fXWlXEWo8fQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceName_EN",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "PlaceName_TH",
                table: "Place");

            migrationBuilder.AddColumn<string>(
                name: "PlaceName",
                table: "Place",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "a3ee2921-35a5-4535-b9a3-7a00d0bfa979");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9911682e-82df-4b2a-a57e-2e8b6cc93612", "AQAAAAEAACcQAAAAEES616bBSqL/DwZuLlB6vtHU3tyVK4gtCxyaWWtafa3is39B1zljdrxQyqjWzAF9Xw==" });
        }
    }
}
