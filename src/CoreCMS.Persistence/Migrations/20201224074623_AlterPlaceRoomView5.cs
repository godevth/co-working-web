using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPlaceRoomView5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[PlaceRoomView]
            AS
            SELECT 
            r.RoomId AS RoomId, r.RoomName_TH AS RoomNameTH, r.RoomName_EN AS RoomNameEN, r.Capacity AS RoomCapacity, 
            r.RoomTypeId AS RoomTypeId, rt.RoomTypeName AS RoomTypeName, p.PlaceId AS PlaceId, p.PlaceName_TH AS PlaceNameTH, 
            p.PlaceName_EN AS PlaceNameeN, p.Details AS PlaceDetail, p.NearBy AS PlaceNearBy, p.Address AS PlaceAddress, p.TAMBON_ID AS PlaceTambonId, 
            p.AMPER_ID AS PlaceAmperId, p.PROVINCE_ID AS PlaceProvinceId, p.ZIP_CODE AS PlaceZipCode, p.Latitude AS PlaceLatitude, p.Longitude AS PlaceLongitude, 
            p.PlaceTypeID AS PlaceTypeId, pt.PlaceTypeName AS PlaceTypeName
            FROM Place p 
            LEFT JOIN PlaceType pt ON p.PlaceTypeID = pt.PlaceTypeID 
            LEFT JOIN Room r ON r.PlaceId = p.PlaceId 
            LEFT JOIN RoomType rt ON r.RoomTypeId = rt.RoomTypeID'");

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutUserId",
                table: "CheckIn",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_CheckOutUserId",
                table: "CheckIn",
                column: "CheckOutUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_AspNetUsers_CheckOutUserId",
                table: "CheckIn",
                column: "CheckOutUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_AspNetUsers_CheckOutUserId",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_CheckOutUserId",
                table: "CheckIn");

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutUserId",
                table: "CheckIn",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "50de0caf-3a80-4e3e-897a-2510f75ed0df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "5e36027c-41f3-4a93-8ced-6be4912d53d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b10ec64a-0aa8-4116-b65d-12b29287d459");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "3e587840-5624-46fa-b03c-27ec12695838");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "e9302f7a-5d9b-4ee3-a5c0-64fbc63e954a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "6eab1053-1b57-4dd5-b489-78980adba82b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d26a34d0-59a5-4f42-ab3a-64b177f4c549");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "836d7285-5faa-4d4f-a8cc-4ae0e8377fe6");
        }
    }
}
