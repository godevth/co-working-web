using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterUserConsentPersistedGrantsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_TermId",
                table: "UserConsent",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConsent_TermAndCondition_TermId",
                table: "UserConsent",
                column: "TermId",
                principalTable: "TermAndCondition",
                principalColumn: "TermId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[UserConsentPersistedGrantsView]
                AS
                SELECT uc.UserConsentId, uc.UserId, uc.PlaceId, uc.IsDeleted, uc.InActiveStatus,
                uc.PersistedGrantsKey, uc.TermId, pg.[Type], pg.ClientId, pg.CreationTime, pg.Expiration, pg.[Data] 
                FROM [dbo].UserConsent uc
                LEFT JOIN [dbo].PersistedGrants pg on uc.PersistedGrantsKey = pg.[Key]'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "297d02bc-e5c4-49c9-89d3-944345fe106a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "99fe39b9-2319-41fd-a9f1-4dcdaf2e4988");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b134ae96-3085-4333-a040-b5e287e893e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "881c27ab-33ee-4d04-b032-b006f27fa191");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "058f4c22-8e58-4594-9f38-7fad4c9b91c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "ab4b81a6-58af-4ce8-9a4d-7c0d0d10e16b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d837d0d3-0bb7-4967-9173-779fabdfe6cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "972ec142-1b75-42d7-9b6b-0b8ecfb9875d");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
