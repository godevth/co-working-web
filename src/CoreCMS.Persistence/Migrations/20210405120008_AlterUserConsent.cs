using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterUserConsent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermAndConditionTermId",
                table: "UserConsent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "UserConsent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "7e80aba9-3d89-4d84-a710-c64c7eb5adc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "82ce9d34-f153-4be5-8e04-0ce137aef170");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "442016cb-333e-44d4-aeec-c227f69f107c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "fa6445a0-c98a-4002-8b7a-54a36efecf4b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "54838842-fd6d-4c15-b0dc-5f6a3ea9ad0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "c89dbac6-6f6c-4817-afdd-b7cd308c4182");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "f242ca13-41df-4997-9673-06e7eded2163");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "ac7ba3cb-68c5-4188-8fea-3297d95bbba3");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_TermAndConditionTermId",
                table: "UserConsent",
                column: "TermAndConditionTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConsent_TermAndCondition_TermAndConditionTermId",
                table: "UserConsent",
                column: "TermAndConditionTermId",
                principalTable: "TermAndCondition",
                principalColumn: "TermId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConsent_TermAndCondition_TermAndConditionTermId",
                table: "UserConsent");

            migrationBuilder.DropIndex(
                name: "IX_UserConsent_TermAndConditionTermId",
                table: "UserConsent");

            migrationBuilder.DropColumn(
                name: "TermAndConditionTermId",
                table: "UserConsent");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "UserConsent");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "d95f6c36-7a2c-461e-8cdc-b10d5708e49b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "5ed5fd1f-4761-4f91-8223-0f98df99b8e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "e6c74563-bc8b-4c51-917a-cc4cf26a499e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b177a71c-43db-4ed4-a631-b118fcae3304");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "cfcbc11d-a842-47c7-93a8-f1d1da28474f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "37d92e69-06ed-481f-ae9d-a7c832bf484d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "7d09b1a2-ded2-42a3-a806-c86e519e7d96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "e904df41-7a33-49ee-8848-402aa5053e6b");
        }
    }
}
