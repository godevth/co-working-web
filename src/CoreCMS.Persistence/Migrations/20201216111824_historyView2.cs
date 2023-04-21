using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class historyView2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[HistoryView]
                AS
                SELECT DISTINCT 
             prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, prv.RoomTypeId, prv.RoomTypeName, prv.PlaceId, prv.PlaceNameTH, prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, prv.PlaceTambonId, prv.PlaceAmperId, prv.PlaceProvinceId, 
             prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, prv.PlaceTypeId, prv.PlaceTypeName, b.PriceTimeType, b.PriceQty, b.Price,
                 (SELECT MIN(StartDate) AS Expr1
                 FROM    dbo.BookingDate
                 WHERE (BookingId = bd.BookingId)) AS BookingStartDate,
                 (SELECT MAX(EndDate) AS Expr1
                 FROM    dbo.BookingDate AS BookingDate_1
                 WHERE (BookingId = bd.BookingId)) AS BookingEndDate, b.Qty, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode, b.IsOwner, b.CustomerEmail, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, 
             b.PaymentMethodCode, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, 
             b.InActiveStatus
            FROM   dbo.Booking AS b LEFT OUTER JOIN
             dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
             dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
             dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "574d7095-3eac-4775-9500-876d064b3a6a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "3729a82e-b8cf-4903-8c84-73c015c7d402");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "94ef1fc5-7100-4056-a575-477b3d815324");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "52c48d89-5cc2-4a63-b35d-64b1b391b54b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "eed2c7ef-d651-436a-b1df-ab2d5200e832");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "a7730274-a66c-46df-bb17-802490b92496");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d0dc3922-0319-4a47-8214-dae3916a254d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "0f58a266-97d8-4f68-8a71-7d3c946d55e8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "3529409e-9a85-48f0-8272-cfb46a6e1e65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ac1f4929-04a5-448b-a472-3943e0c196bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "e71e4fdf-cc94-4f46-ad4a-f3a28dba4c30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "c2120af7-d35a-4de0-8e05-9b7aeb7d6bdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "80cd880a-0d46-405b-9afa-3b3b88473488");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "5df45666-7026-4a3e-ba54-6e48455643bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "11ea9fad-f466-49dd-85c1-7c055dbde11b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "80ba8b32-01b1-486f-bb41-685197c79247");
        }
    }
}
