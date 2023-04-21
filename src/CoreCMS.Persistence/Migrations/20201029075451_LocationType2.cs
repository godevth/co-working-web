using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class LocationType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LocationType");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "b4c45d38-eefe-465a-acb9-d3fb06427803");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "67870dd7-21fc-48a4-8a95-c5547eaa7fc6", "AQAAAAEAACcQAAAAEMiN9Ng/lltrXYeniS919zQKD2l4IResaHxutmLGBR53QX/mN4Oa3FFJ3z5H+DWpBw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LocationType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "f22de949-313f-45b1-944f-0131e82078c3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3b633d8e-f2f8-4b84-b676-2b1fe8ade361", "AQAAAAEAACcQAAAAEObdpNfQQejEUkBPGjIZa4RzOVKuOBOx2V2u/d5UrJ3ARm1pL3Lv0nhThQBbTRBRrg==" });
        }
    }
}
