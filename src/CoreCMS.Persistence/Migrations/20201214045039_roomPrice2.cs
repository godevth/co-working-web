using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class roomPrice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUserId",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InActiveStatus",
                table: "RoomPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "RoomPrice",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "5686dae8-bca0-49fa-97bd-695ff0df0684");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "0e6879d4-a369-4272-b75e-4451d0198bb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "312f081a-0e64-4a1b-aecf-cc66a9b3a85a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "587337c8-7f25-4dbf-b4d2-91d2d95bbc7e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "c1931e32-44a1-44f8-ac38-e3cb24bc95d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "67803dd8-558d-4e85-961d-21a4ce24897f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "4a7ec65d-75e9-4f4b-ba40-2a1485324996");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "6449e942-a195-45c9-94c6-2dcfbfdcb81a");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPrice_CreatedUserId",
                table: "RoomPrice",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPrice_DeletedUserId",
                table: "RoomPrice",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPrice_UpdatedUserId",
                table: "RoomPrice",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPrice_AspNetUsers_CreatedUserId",
                table: "RoomPrice",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPrice_AspNetUsers_DeletedUserId",
                table: "RoomPrice",
                column: "DeletedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPrice_AspNetUsers_UpdatedUserId",
                table: "RoomPrice",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomPrice_AspNetUsers_CreatedUserId",
                table: "RoomPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPrice_AspNetUsers_DeletedUserId",
                table: "RoomPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPrice_AspNetUsers_UpdatedUserId",
                table: "RoomPrice");

            migrationBuilder.DropIndex(
                name: "IX_RoomPrice_CreatedUserId",
                table: "RoomPrice");

            migrationBuilder.DropIndex(
                name: "IX_RoomPrice_DeletedUserId",
                table: "RoomPrice");

            migrationBuilder.DropIndex(
                name: "IX_RoomPrice_UpdatedUserId",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "InActiveStatus",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "RoomPrice");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "RoomPrice");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "38055738-a641-447a-af04-bca598071e44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "014a1341-11c5-46ae-94b8-b9390cf12edc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "dbce2e72-ae22-424a-b4b2-2dc5aa1dca18");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "f901fdf6-0d4b-4948-af67-633e48c8662c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "d549ad05-2776-4bbd-acbb-34e69c061b66");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "c0706a6c-da0b-4bb7-9279-6c55834fe7e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "533a3f13-e8b1-455d-bcdc-1349930f4304");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "726b33fb-c52e-4bab-867b-f523affaf4cc");
        }
    }
}
