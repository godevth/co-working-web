using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterHistoryView3 : Migration
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
                 WHERE (BookingId = bd.BookingId)) AS BookingEndDate, b.Qty, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode,s.SystemVariableName, b.IsOwner, b.CustomerEmail,(select Id from AspNetUsers where Email = b.CustomerEmail) as CustomerId, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, 
             u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
FROM   dbo.Booking AS b LEFT OUTER JOIN
             dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
             dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
             dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id INNER JOIN
			 dbo.SystemVariables AS s ON s.SystemVariableCode = b.BookingStatusCode '");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "83f35db0-014d-4710-9f17-d9c66b18beed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "f00624e8-ab00-4bd2-9b43-81ea42ece6b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "de2fccdb-4bda-435b-a8e8-ab50127ee625");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b8977ccc-3e94-412f-addc-18d51e857b68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "9918ca71-808e-46e3-b6a1-78362aac2d86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "86d2f6e6-97dd-4be6-a1f9-fe12d25a3254");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "dee332b2-88ab-4845-ba32-4727e5485501");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "cb66da8f-a795-49a4-acd2-ed104031985d");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CHECKIN",
                column: "SystemVariableName",
                value: "เข้าใช้งาน");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "HistoryView");

            migrationBuilder.DropColumn(
                name: "SystemVariableName",
                table: "HistoryView");

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

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CHECKIN",
                column: "SystemVariableName",
                value: "เช็คอิน");
        }
    }
}
