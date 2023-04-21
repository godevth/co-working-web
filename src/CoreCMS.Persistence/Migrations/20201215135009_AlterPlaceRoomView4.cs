using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPlaceRoomView4 : Migration
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
                p.PlaceTypeID AS PlaceTypeId, pt.PlaceTypeName AS PlaceTypeName, id.StartDay AS OpenDay, id.StartTime AS OpenTime, id.EndTime AS CloseTime, 
                id.ImplementationDateId AS ImplementationDateId, rp.Id AS RoomPriceId, rp.TimeType AS PriceTimeType, rp.Qty AS PriceQty, rp.Price AS Price
                FROM Place p 
                LEFT JOIN PlaceType pt ON p.PlaceTypeID = pt.PlaceTypeID 
                LEFT JOIN ImplementationDate id ON id.PlaceId = p.PlaceId 
                LEFT JOIN Room r ON r.PlaceId = p.PlaceId 
                LEFT JOIN RoomPrice rp ON rp.RoomId = r.RoomId 
                LEFT JOIN RoomType rt ON r.RoomTypeId = rt.RoomTypeID 
                WHERE r.IsDeleted = 0 AND r.InActiveStatus = 0'");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "OpenDay",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "PriceQty",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "PriceTimeType",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "OpenDay",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "PriceQty",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "PriceTimeType",
                table: "BookingView");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomPrice",
                table: "PlaceRoomView",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StartDay",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeType",
                table: "PlaceRoomView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "BookingView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RoomPrice",
                table: "BookingView",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StartDay",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeType",
                table: "BookingView",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "0411a69f-9d7c-4a08-92f2-a87a8e050d58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d88f50b4-263a-45d7-a0fd-a2570e0b8f8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b6759f56-0458-4577-9fba-fce562df9e21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "3d83aa47-8ebf-48cf-955f-529ce5423a57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "ec5324ff-c8e7-4bf7-8daf-91ec43847d9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "e701f7c8-b711-4803-b410-83a80ca55c88");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "21962018-cdd6-46c0-8a2b-5f84cf1565a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b5abc943-8fe8-45d7-9058-06d77021800a");
        }
    }
}
