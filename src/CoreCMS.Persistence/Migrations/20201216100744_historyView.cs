using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class historyView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'CREATE VIEW [dbo].[HistoryView]
                AS
                SELECT DISTINCT 
             prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, prv.RoomTypeId, prv.RoomTypeName, prv.PlaceId, prv.PlaceNameTH, prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, prv.PlaceTambonId, prv.PlaceAmperId, prv.PlaceProvinceId, 
             prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, prv.PlaceTypeId, prv.PlaceTypeName, b.PriceTimeType, b.PriceQty, b.Price,
                 (SELECT MIN(StartDate) AS Expr1
                 FROM    dbo.BookingDate
                 WHERE (BookingId = bd.BookingId)) AS BookingStartDate,
                 (SELECT MAX(StartDate) AS Expr1
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryView");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "4e8e861d-8977-4cc1-9764-ec264205f007");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "a6fbdc44-1e1d-4500-bfe5-bb6ecb102d1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "ba13358c-c67f-46f2-9a56-7a854876c348");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "d35b2649-9345-4262-93bf-1f3a47b31a11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "abd88dab-6713-4a6d-aa8e-341d7051afef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "8f951f2b-e788-4e6b-94ed-b6f425f9917c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "14fa23a0-29aa-4914-9f35-2ebafab7e15e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "56d76cf0-ff96-4338-8d20-f3750435da72");
        }
    }
}
