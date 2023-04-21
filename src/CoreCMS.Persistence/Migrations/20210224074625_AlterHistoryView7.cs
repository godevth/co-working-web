using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterHistoryView7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[HistoryView]
                AS
SELECT DISTINCT 
                         prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, prv.RoomTypeId, prv.RoomTypeName,prv.RoomTypeNameEN, prv.PlaceId, prv.PlaceNameTH, prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, 
                         prv.PlaceTambonId, prv.PlaceAmperId, prv.PlaceProvinceId, prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, prv.PlaceTypeId, prv.PlaceTypeName,prv.PlaceTypeNameEN, b.PriceTimeType, b.PriceQty, b.Price,
                             (SELECT        MIN(StartDate) AS Expr1
                               FROM            dbo.BookingDate
                               WHERE        (BookingId = bd.BookingId)) AS BookingStartDate,
                             (SELECT        MAX(EndDate) AS Expr1
                               FROM            dbo.BookingDate AS BookingDate_1
                               WHERE        (BookingId = bd.BookingId)) AS BookingEndDate, b.Qty, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode, s.SystemVariableName, b.IsOwner, b.CustomerEmail,
                             (SELECT        top(1) Id from AspNetUsers where Email = b.CustomerEmail and IsDeleted=0 and InActiveStatus=0 order by CreatedDate DESC ) AS CustomerId, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, 
                         p.SystemVariableName AS PaymentMethodName, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, 
                         u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
FROM            dbo.Booking AS b LEFT OUTER JOIN
                         dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
                         dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
                         dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id INNER JOIN
                         dbo.SystemVariables AS s ON s.SystemVariableCode = b.BookingStatusCode INNER JOIN
                         dbo.SystemVariables AS p ON p.SystemVariableCode = b.PaymentMethodCode'");
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "36354f62-d84c-4323-965d-1d0c7a140926");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "0c62e02b-a799-4950-8ab9-28ef34955a3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "43058579-dc91-449f-bcea-c990beeabc01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "185471bc-2cbb-4c01-9e55-d9e433608dd9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "beaac938-4468-4667-858e-c8f8473d3034");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "ec3dd170-a288-4170-bccf-0a90c675f8b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "4210b9ec-fa98-4d27-9507-9b8cc5cf2d2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "73ce2569-3806-4717-b9a9-8c8fe6d84763");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "36a98d2e-679e-41eb-8e4b-426c9d88f697");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d957328c-629b-48b8-b00f-c88397195149");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "fa1a98ec-9404-4916-bb55-30a060757ac9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "06cd05f4-37eb-46e7-8908-c8a4cdb8e439");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "493a3837-311d-41d1-83ef-932d40721c8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "fa58fbf8-36e7-4c9d-b691-054aac132474");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "1d5fb0f2-0e82-448b-bd91-a919abd36c2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "8e73b7fb-92ff-4613-9486-295af57ed6bd");
        }
    }
}
