using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddBookingView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'CREATE VIEW [dbo].[BookingView]
                AS
                SELECT 
                prv.RoomId, prv.RoomNameTH, prv.RoomNameEN, prv.RoomCapacity, 
                prv.RoomTypeId, prv.RoomTypeName, prv.PlaceId, prv.PlaceNameTH, 
                prv.PlaceNameeN, prv.PlaceDetail, prv.PlaceNearBy, prv.PlaceAddress, prv.PlaceTambonId, 
                prv.PlaceAmperId, prv.PlaceProvinceId, prv.PlaceZipCode, prv.PlaceLatitude, prv.PlaceLongitude, 
                prv.PlaceTypeId, prv.PlaceTypeName, prv.StartDay, prv.StartTime, prv.EndTime, 
                prv.RoomPriceId, prv.TimeType , prv.Qty, prv.RoomPrice,
                prv.ImplementationDateId, b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode,
                b.IsOwner, b.CustomerEmail, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber,
                b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, 
                b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName,
                u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
                FROM Booking b  
                LEFT JOIN PlaceRoomView prv ON b.RoomId = prv.RoomId 
                LEFT JOIN AspNetUsers u ON b.CreatedUserId = u.Id'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "cd9686cd-e81f-4ee0-988b-9eeb158314be");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "3cfb1ea9-909f-4ac3-922e-7915faa85987");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "31aef929-f1a9-4db2-8d81-5c4396a3636a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "24d5a909-4873-4e7e-a2d2-99c6bbfeea18");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "6d9de0cc-ce30-42a8-b232-20b07025bab5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "79f5d939-69f6-49ee-9360-b2334434eb3d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "88c035c2-beb4-4a19-a24d-f88331b43dbe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "34ad11b5-7069-4c68-9e0f-1af791e9d800");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingView");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "0b6ea374-67dc-4751-bab6-1c4267a2beaf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "3b0215b5-6630-463a-89be-6c208da7c905");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "a529b930-085f-4433-8890-2e9df6c5800a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "984982af-fe92-472a-905a-526330883a45");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "123ac99f-2bcd-4523-b2f0-114540c2d512");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "1a31f823-9fb0-4e81-92c7-379bf0912c99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "abe3636d-4e9d-4c89-8e2d-4f9ec3e648b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "fedf5ef9-60b7-4027-9b9f-78a7b0d8b14b");
        }
    }
}
