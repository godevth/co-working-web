using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBookingView3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'ALTER VIEW [dbo].[BookingView]
                AS
                SELECT 
                prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, 
                prv.RoomTypeId, prv.RoomTypeName, prv.PlaceId, prv.PlaceNameTH, 
                prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, prv.PlaceTambonId, 
                prv.PlaceAmperId, prv.PlaceProvinceId, prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, 
                prv.PlaceTypeId, prv.PlaceTypeName, prv.StartDay, prv.StartTime, prv.EndTime, 
                prv.RoomPriceId, prv.TimeType , prv.Qty, prv.RoomPrice,
                bd.BookingDateId, bd.StartDate AS BookingStartDate, bd.EndDate AS BookingEndDate,
                prv.ImplementationDateId, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode,
                b.IsOwner, b.CustomerEmail, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber,
                b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, 
                b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName,
                u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
                FROM Booking b  
                LEFT JOIN PlaceRoomView prv ON b.RoomPriceId = prv.RoomPriceId
                LEFT JOIN BookingDate bd ON b.BookingId  = bd.BookingId 
                LEFT JOIN AspNetUsers u ON b.CreatedUserId = u.Id'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "de3ae66c-d553-4b68-9d75-0bda36f99888");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "154ba32b-436d-4da1-ac0f-2b263e10ba72");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b3d210e2-647e-4f60-88e2-eb59bd9d4c4b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "5c4dcebc-6f16-46b8-8bff-26d0f9ffd2b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "12ae6a95-5fd4-4d7a-bce0-28bf137851cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "ca46ee54-65aa-47c0-b8da-6e8fe1c44d92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "24aa91e2-d5a3-4229-a8e8-23c1e55a34c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "ac6ff2eb-f219-446c-a9cd-0aadb5a99134");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDateId",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "BookingEndDate",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "BookingStartDate",
                table: "BookingView");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "599f2425-4392-4fa7-a887-ebc215eda413");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "cbf8b5fb-fbb6-40b5-ad82-8fde54aa3802");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "45a309c8-df6a-4ae9-9da3-4a491477df2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "8202289d-3014-46a7-ba36-02e5d59169d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "ee9baf6f-f722-4e10-93c0-4f2b6df1a92b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "7f7bfff2-2f51-4514-b8ad-d222800982bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "66a37657-44f2-4c5a-b8a5-49c3fafda28d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "1d3649e0-2cba-4694-a9fc-7b9fdc6e8e63");
        }
    }
}
