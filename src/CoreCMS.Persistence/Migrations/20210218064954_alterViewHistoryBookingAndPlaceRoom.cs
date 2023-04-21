using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class alterViewHistoryBookingAndPlaceRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[PlaceRoomView]
                AS
SELECT        r.RoomId, r.RoomName_TH AS RoomNameTH, r.RoomName_EN AS RoomNameEN, r.Capacity AS RoomCapacity, r.RoomTypeId, rt.RoomTypeName,rt.RoomTypeNameEN, p.PlaceId, p.PlaceName_TH AS PlaceNameTH, 
                         p.PlaceName_EN AS PlaceNameeN, p.Details AS PlaceDetail, p.NearBy AS PlaceNearBy, p.Address AS PlaceAddress, p.TAMBON_ID AS PlaceTambonId, p.AMPER_ID AS PlaceAmperId, p.PROVINCE_ID AS PlaceProvinceId, 
                         p.ZIP_CODE AS PlaceZipCode, p.Latitude AS PlaceLatitude, p.Longitude AS PlaceLongitude, p.PlaceTypeID, pt.PlaceTypeName,pt.PlaceTypeNameEN
FROM            dbo.Place AS p LEFT OUTER JOIN
                         dbo.PlaceType AS pt ON p.PlaceTypeID = pt.PlaceTypeID LEFT OUTER JOIN
                         dbo.Room AS r ON r.PlaceId = p.PlaceId LEFT OUTER JOIN
                         dbo.RoomType AS rt ON r.RoomTypeId = rt.RoomTypeID'");

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
                             (SELECT        Id
                               FROM            dbo.AspNetUsers
                               WHERE        (Email = b.CustomerEmail)) AS CustomerId, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, 
                         p.SystemVariableName AS PaymentMethodName, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, 
                         u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
FROM            dbo.Booking AS b LEFT OUTER JOIN
                         dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
                         dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
                         dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id INNER JOIN
                         dbo.SystemVariables AS s ON s.SystemVariableCode = b.BookingStatusCode INNER JOIN
                         dbo.SystemVariables AS p ON p.SystemVariableCode = b.PaymentMethodCode'");

            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[BookingView]
                AS
SELECT        prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, prv.RoomTypeId, prv.RoomTypeName,prv.RoomTypeNameEN, prv.PlaceId, prv.PlaceNameTH, prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, 
                         prv.PlaceTambonId, prv.PlaceAmperId, prv.PlaceProvinceId, prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, prv.PlaceTypeId, prv.PlaceTypeName,prv.PlaceTypeNameEN, bd.OpenDay, bd.OpenTime, bd.CloseTime, b.PriceTimeType, 
                         b.PriceQty, b.Price, bd.BookingDateId, bd.StartDate AS BookingStartDate, bd.EndDate AS BookingEndDate, b.Qty, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode, b.IsOwner, b.CustomerEmail, 
                         b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber, b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, 
                         u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus, b.Remaining
FROM            dbo.Booking AS b LEFT OUTER JOIN
                         dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
                         dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
                         dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "c7d00c04-e169-4d83-94fd-4896d2213be5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "2b375d55-4c8c-492f-b8c0-50beb255bb9d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "032d2120-027d-4bea-b598-11455f5f8e47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "bce5f98f-bc1f-4d07-8e18-86dfb220341c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "de0aaf0d-0d23-4c1c-9658-5f1a7f2094b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "2ea2d860-8cff-4e0e-a61f-5d7e3f499697");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "4d71ed19-44ac-4089-8915-e9ae3aa44a96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "8345f06e-c0d2-42f1-a113-83562f9737f8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e1567c4b-2109-449f-a758-66ba9bce9377");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "4844149d-c3e6-40d3-83d0-d68c17ce06cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "146d2080-1fed-4461-ab62-d75a87f9b203");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "91c7af6c-5da7-4a92-8d47-8a5b404d87f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "d3b7a104-d221-468e-8065-fef25696efed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "24e361a2-a2a7-4882-821b-0f735cf842c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "7d4b2984-9586-4edd-9eae-fff9ee857405");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "a4c7706b-5cca-4d03-8a7d-c86945e603ee");
        }
    }
}
