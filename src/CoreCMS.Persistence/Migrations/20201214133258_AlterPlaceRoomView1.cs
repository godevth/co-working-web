using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPlaceRoomView1 : Migration
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
                p.PlaceTypeID AS PlaceTypeId, pt.PlaceTypeName AS PlaceTypeName, id.StartDay AS StartDay, id.StartTime AS StartTime, id.EndTime AS EndTime, 
                id.ImplementationDateId AS ImplementationDateId, rp.TimeType AS TimeType, rp.Qty AS Qty, rp.Price AS RoomPrice
                FROM Place p 
                LEFT JOIN PlaceType pt ON p.PlaceTypeID = pt.PlaceTypeID 
                LEFT JOIN ImplementationDate id ON id.PlaceId = p.PlaceId 
                LEFT JOIN Room r ON r.PlaceId = p.PlaceId 
                LEFT JOIN RoomPrice rp ON rp.RoomId = r.RoomId 
                LEFT JOIN RoomType rt ON r.RoomTypeId = rt.RoomTypeID 
                WHERE r.IsDeleted = 0'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "5a8729bd-52f2-49b6-a4ce-e16a5352be58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "78fa24af-915d-48b7-a53e-2d3412257c80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "7f45722b-f299-4614-a7ab-a9669f5058bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "90dadd37-4926-4945-86b1-b0d3f472af99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "ee168818-2a57-4f7a-868f-46b4bd278546");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "a69b86a7-ac6f-4fe3-96ac-f8eeedb0b07d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b8d6240f-eaad-4b8f-9f1a-29a87b42414d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "aaa07183-8826-49bf-829f-c8eb7b0d1234");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "RoomCapacity",
                table: "PlaceRoomView");

            migrationBuilder.DropColumn(
                name: "TimeType",
                table: "PlaceRoomView");

            migrationBuilder.AddColumn<int>(
                name: "RoomQty",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatQty",
                table: "PlaceRoomView",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
