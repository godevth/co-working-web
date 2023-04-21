using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class updateFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Facility");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Facility");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "FacilityServices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "FacilityServices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "2124ec2d-e12d-4e85-a549-c888f8e5a7fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f7db070-8439-4b51-8946-2b62f37c8247", "AQAAAAEAACcQAAAAEALauWicdUQNHjk8uEL+G6f3LviFfEmhGCR6bxwG+Wk/pv+5gqMylHG1DX18yAHMUw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FacilityServices");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "FacilityServices");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Facility",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Facility",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
