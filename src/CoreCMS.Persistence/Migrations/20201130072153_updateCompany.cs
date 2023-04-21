using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class updateCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InActiveStatus",
                table: "CompanyProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CompanyProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "CompanyProfiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InActiveStatus",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "Company",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "43ac0c9d-0c21-4178-a580-381c08b5cacc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ac0ce757-02b7-4ed8-89aa-8e7e994d672b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "3aec8910-3a49-4d6b-a201-dd07de565932");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "04af972a-66b3-4637-b9b0-b2db747deb86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "2119394d-9945-4be8-bd1b-1294b7ea5de0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "47a1b9fe-0b31-4892-9675-50e6bc6e8504");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "aca2fc3e-acbd-4001-b60c-537f5e14b899");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "f7bd68b6-889c-42bd-ab5f-638a34f91636");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_CreatedUserId",
                table: "CompanyProfiles",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_DeletedUserId",
                table: "CompanyProfiles",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_UpdatedUserId",
                table: "CompanyProfiles",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedUserId",
                table: "Company",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_DeletedUserId",
                table: "Company",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UpdatedUserId",
                table: "Company",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_CreatedUserId",
                table: "Company",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_DeletedUserId",
                table: "Company",
                column: "DeletedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_UpdatedUserId",
                table: "Company",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_CreatedUserId",
                table: "CompanyProfiles",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_DeletedUserId",
                table: "CompanyProfiles",
                column: "DeletedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_UpdatedUserId",
                table: "CompanyProfiles",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_CreatedUserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_DeletedUserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_UpdatedUserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_CreatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_DeletedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyProfiles_AspNetUsers_UpdatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_CreatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_DeletedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_UpdatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Company_CreatedUserId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_DeletedUserId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_UpdatedUserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "InActiveStatus",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "CompanyProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "InActiveStatus",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Company");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "bbd16c9f-7f2f-41a4-9ad3-23e259d3b56a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "9218ebd9-d659-485a-827c-c51b0b631183");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "0ac309bf-715d-4db9-a818-4f2e52a50e9b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "89ff36fc-4475-4ec8-b079-70e35c0ada19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "21aadf2a-4959-4a67-8c86-b18a3e4b67f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "9870d23e-eb81-481c-aeae-abd9a4f81a59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "2c7947c7-d4ef-42f7-b330-8fcfb3f8429f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "1f2571f5-6b3a-40a5-953e-dc7be119715f");
        }
    }
}
