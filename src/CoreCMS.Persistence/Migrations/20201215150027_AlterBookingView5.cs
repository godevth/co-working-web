using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBookingView5 : Migration
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
                LEFT JOIN RoomType rt ON r.RoomTypeId = rt.RoomTypeID 
                WHERE r.IsDeleted = 0 AND r.InActiveStatus = 0'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "032a3d2d-6f77-4a22-b34f-e994454e649c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d75bbe80-aeb8-43cf-ad12-bbd54ef2766f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b88a2cec-eabb-4716-9c62-643f40c31127");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "7a5768e1-b958-4f4b-90fb-0f3937b5bf1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "25a383e1-a56b-4da1-ad54-35db8a0e60f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "153dc6ce-f48b-4851-a03b-e421dbdb4920");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b31f470e-49a1-4b4e-8ace-11d635db7090");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "2340e6c7-c5c3-4911-aea9-69d7cab5df67");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "BookingView");

            migrationBuilder.AddColumn<string>(
                name: "CloseTime",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImplementationDateId",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OpenDay",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenTime",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PlaceRoomView",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PriceQty",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceTimeType",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomPriceId",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CloseTime",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImplementationDateId",
                table: "BookingView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OpenDay",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenTime",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookingView",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PriceQty",
                table: "BookingView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceTimeType",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomPriceId",
                table: "BookingView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "8ee097ec-70a1-4066-9914-4a3a2b74a15c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "efe2c971-7e98-4605-8326-6ffbef9855ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "bb8c7d75-efac-42fd-903a-8aeb9eeb7a14");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "43639b8d-6e2b-4a79-97dc-452b13433605");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "82beee69-ee03-40d6-964f-1cf4d853a7ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "f5521166-03fd-438c-9319-6e7c36cd3cb8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "7b40f831-7a69-4b13-887a-9770ae52aff2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b64cdbd7-21ef-4f47-81fa-c0217fa88242");
        }
    }
}
