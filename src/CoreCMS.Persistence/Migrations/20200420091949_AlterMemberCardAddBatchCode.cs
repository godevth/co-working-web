using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterMemberCardAddBatchCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchCode",
                table: "MemberCards",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "f81591e5-18eb-4054-8d1d-b19dc8bd8e63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "06f86940-5a98-4695-b205-eb2d4237c3f1", "AQAAAAEAACcQAAAAEDZN8OBaHpVSHZObsdDR7EKTj+DrcUjVRa/YE23oH5hSx2gMc72tupbnBhpeTfmbPg==" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_BatchCode",
                table: "MemberCards",
                column: "BatchCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_CardNumber",
                table: "MemberCards",
                column: "CardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_GenerateRefCode",
                table: "MemberCards",
                column: "GenerateRefCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_RunningNumber",
                table: "MemberCards",
                column: "RunningNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MemberCards_BatchCode",
                table: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_MemberCards_CardNumber",
                table: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_MemberCards_GenerateRefCode",
                table: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_MemberCards_RunningNumber",
                table: "MemberCards");

            migrationBuilder.DropColumn(
                name: "BatchCode",
                table: "MemberCards");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "cf91291a-d789-42f7-bbbd-3249664932cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "485bf866-dc9a-4399-afb6-ad3558ef5d2b", "AQAAAAEAACcQAAAAEJ7kGhXs1KwntY0qFK0hYVCI26xbcTIaog0F9VuHyq0ueLrKxatcgG6idgUzF6DBIQ==" });
        }
    }
}
