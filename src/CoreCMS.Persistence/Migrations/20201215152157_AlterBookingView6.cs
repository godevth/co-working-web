using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBookingView6 : Migration
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
                prv.PlaceTypeId, prv.PlaceTypeName, b.OpenDay, b.OpenTime, b.CloseTime, 
                b.PriceTimeType, b.PriceQty, b.Price,
                bd.BookingDateId, bd.StartDate AS BookingStartDate, bd.EndDate AS BookingEndDate, b.Qty, 
                b.BookingId, b.BookingNumber, b.BookingRunning, b.BookingStatusCode,
                b.IsOwner, b.CustomerEmail, b.CustomerFirstname, b.CustomerLastname, b.CustomerPhoneNumber,
                b.Discount, b.Total, b.Tax, b.GrandTotal, b.PaymentMethodCode, b.Remark, b.CreatedDate, 
                b.CreatedUserId AS OwnerId, u.Email AS OwnerEmail, u.FirstName AS OwnerFirstName, u.LastName AS OwnerLastName,
                u.PhoneNumber AS OwnerPhoneNumber, u.PhoneCountryCode AS OwnerPhoneCountryCode, b.IsDeleted, b.InActiveStatus
                FROM Booking b  
                LEFT JOIN PlaceRoomView prv ON b.RoomId = prv.RoomId 
                LEFT JOIN BookingDate bd ON b.BookingId  = bd.BookingId 
                LEFT JOIN AspNetUsers u ON b.CreatedUserId = u.Id'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "92a711e8-7fea-4042-b801-f7aba580f819");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "b1b3d279-c4dc-4fbc-b665-01b3953b2310");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "e4e46f08-a45c-4bb6-8a7b-65f31dedf7ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b85271a6-6dc3-4c13-a7f1-30757e38c792");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "3d68019b-d6da-4ba6-b2d2-5b38a9360d69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "732555ec-f84b-4ef6-a29c-4ccb449c9d6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "0a1b5f9d-f712-41cb-bcab-20109dcacb99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "79be3c20-4f59-4650-a8a2-6439605348e1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "OpenDay",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "PriceQty",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "PriceTimeType",
                table: "BookingView");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "032a3d2d-6f77-4a22-b34f-e994454e649c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d75bbe80-aeb8-43cf-ad12-bbd54ef2766f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b88a2cec-eabb-4716-9c62-643f40c31127");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "7a5768e1-b958-4f4b-90fb-0f3937b5bf1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "25a383e1-a56b-4da1-ad54-35db8a0e60f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "153dc6ce-f48b-4851-a03b-e421dbdb4920");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b31f470e-49a1-4b4e-8ace-11d635db7090");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "2340e6c7-c5c3-4911-aea9-69d7cab5df67");
        }
    }
}
