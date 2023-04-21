using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterHistoryView : Migration
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
                 WHERE (BookingId = bd.BookingId)) AS BookingEndDate, b.Qty, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode, b.IsOwner, b.CustomerEmail,(select Id from AspNetUsers where Email = b.CustomerEmail) as CustomerId, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, 
             u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
FROM   dbo.Booking AS b LEFT OUTER JOIN
             dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
             dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
             dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id'");
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "99bf95ff-be7a-4bf0-8075-611badf7c3cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "7ca46ed6-019b-4092-9b5c-e0435815b199");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "52d3c3c6-fc7d-4dc4-b972-c22dd18c57ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "239b4242-c245-4570-81f1-21e42c851cce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "b188e0a2-916a-4c3d-af15-217212988e31");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "786ec3ed-5e91-4831-aafc-3174fb4e1246");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "30aca2e9-89a8-47ab-a242-13c1595f0cce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "ac650ddc-2ae8-4fe2-bce6-d2c0718fe227");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "77da857e-fbb4-4064-a28a-7ef988f1d02e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ac79c393-c136-4785-a736-159b0c899f34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "4a2f8120-0a26-4f79-abcd-0dc55d5cb355");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "9387e95d-bafd-4c73-b1ee-c88a3d9ed04f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "6cbe3e24-bc7b-49ef-bc81-465dbe136a0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "beeefac0-afb4-429e-b196-3c338c126956");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "27f98d8c-fec4-442d-bfc3-30751de8dca8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "90484208-0b84-41e7-ac4a-1a88d6432ef7");
        }
    }
}
