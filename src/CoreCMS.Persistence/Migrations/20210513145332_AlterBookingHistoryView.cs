using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBookingHistoryView : Migration
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
                         u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus, b.Remaining, b.VoidCode, b.RefundedBy, b.RefundedDate
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
                         u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName, u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus, b.Remaining, b.VoidCode, b.RefundedBy, b.RefundedDate
                FROM            dbo.Booking AS b LEFT OUTER JOIN
                         dbo.PlaceRoomView AS prv ON b.RoomId = prv.RoomId LEFT OUTER JOIN
                         dbo.BookingDate AS bd ON b.BookingId = bd.BookingId LEFT OUTER JOIN
                         dbo.AspNetUsers AS u ON b.CreatedUserId = u.Id'");
                         
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "c866fb88-0288-4534-a7a9-1394d8ac8fa3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "dcefd7d6-fbd4-4dc7-ae67-49c5d2d56bd9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "d5a2a646-8850-48cc-bc77-4c8ad91dc388");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "def976cd-c06a-4dec-a912-fc22d6c1ef4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "57f0f373-f284-4fd5-a9f5-effd4fb40d36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "050c0e75-f5ee-43c8-a2a4-9541a6b77b18");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "4be29c70-b712-41b2-a80a-cc139597d567");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "91098f8a-c201-4524-950b-460d4477eef9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryView_AspNetUsers_RefundedBy",
                table: "HistoryView");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryView_SystemVariables_VoidCode",
                table: "HistoryView");

            migrationBuilder.DropIndex(
                name: "IX_HistoryView_RefundedBy",
                table: "HistoryView");

            migrationBuilder.DropIndex(
                name: "IX_HistoryView_VoidCode",
                table: "HistoryView");

            migrationBuilder.DropColumn(
                name: "RefundedBy",
                table: "HistoryView");

            migrationBuilder.DropColumn(
                name: "RefundedDate",
                table: "HistoryView");

            migrationBuilder.DropColumn(
                name: "VoidCode",
                table: "HistoryView");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "ba0bb87c-7a72-4027-8e54-8d9b7143650f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "83eb8e15-8aeb-4504-aae7-548d2b0c3da7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "7c2e415b-260c-4e93-b0cb-ba8ad55eeab1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "f80a9d84-f5db-436e-9798-04677f73b952");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "1098bc18-3160-4e03-a773-dce84224d982");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "0f8cd82a-cb03-4aa6-9cd9-65726ff8cbd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b6e1c840-ff50-4867-b108-8b1d45226b93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "4b6f9cd8-a3a4-4224-921a-4ed115fae30c");
        }
    }
}
