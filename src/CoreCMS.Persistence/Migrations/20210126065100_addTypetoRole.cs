using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class addTypetoRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserType_AspNetRoles_UserRoleId",
                table: "UserType");

            migrationBuilder.DropIndex(
                name: "IX_UserType_UserRoleId",
                table: "UserType");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "UserType");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "2d0c3dd7-d0b8-4a54-a97a-4bcf72a84402");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "9bf96cc1-a54b-420a-9559-afa6068b677f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "0a33dd97-bfc4-42f6-a41a-e608da0e3158");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "2abfde73-77f0-4fa1-bd60-ffb884935b59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "89f860d9-87cf-4580-a920-acb265fe3deb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "4c930462-afe3-439d-b5c3-126e45ce0d3b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "c50ed67d-0a29-4afd-80d2-76f6c06e618e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "931d3e9a-8de6-4959-aebe-89b96037f0b1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserTypeId",
                table: "AspNetRoles",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_UserType_UserTypeId",
                table: "AspNetRoles",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_UserType_UserTypeId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserTypeId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "UserRoleId",
                table: "UserType",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "85926d31-8614-49fa-a3b8-de9d7b628806");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "31296473-7239-4b61-99f0-e0135f9f79b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "f275683d-9072-4f2f-b301-8c50e760cb3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "e9d898be-7e50-4a52-bc79-a8fafafa2c7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "63c1eadc-f427-45ff-b359-07056b0d09d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "dd6e6004-e906-4402-bd9a-bbfbdc907a9d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "f5ca4c6d-14c1-4b04-b21e-af70cf9c4361");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "fa874784-c4af-41a9-b425-e217abc1bb76");

            migrationBuilder.CreateIndex(
                name: "IX_UserType_UserRoleId",
                table: "UserType",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserType_AspNetRoles_UserRoleId",
                table: "UserType",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
