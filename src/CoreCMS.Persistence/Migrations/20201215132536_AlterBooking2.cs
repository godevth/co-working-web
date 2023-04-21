using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBooking2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_RoomPrice_RoomPriceId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingFacility_BookingDate_BookingDateId",
                table: "BookingFacility");

            migrationBuilder.DropIndex(
                name: "IX_BookingFacility_BookingDateId",
                table: "BookingFacility");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RoomPriceId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingDateId",
                table: "BookingFacility");

            migrationBuilder.DropColumn(
                name: "RoomPriceId",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingFacility",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookingFacility",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CloseTime",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenDay",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenTime",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Booking",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PriceQty",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceTimeType",
                table: "Booking",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "0411a69f-9d7c-4a08-92f2-a87a8e050d58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d88f50b4-263a-45d7-a0fd-a2570e0b8f8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b6759f56-0458-4577-9fba-fce562df9e21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "3d83aa47-8ebf-48cf-955f-529ce5423a57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "ec5324ff-c8e7-4bf7-8daf-91ec43847d9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "e701f7c8-b711-4803-b410-83a80ca55c88");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "21962018-cdd6-46c0-8a2b-5f84cf1565a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b5abc943-8fe8-45d7-9058-06d77021800a");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFacility_BookingId",
                table: "BookingFacility",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingFacility_Booking_BookingId",
                table: "BookingFacility",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingFacility_Booking_BookingId",
                table: "BookingFacility");

            migrationBuilder.DropIndex(
                name: "IX_BookingFacility_BookingId",
                table: "BookingFacility");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingFacility");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingFacility");

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "OpenDay",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PriceQty",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PriceTimeType",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "BookingDateId",
                table: "BookingFacility",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomPriceId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e924c3ce-bc46-4241-8ee4-a2fb0e941e4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "0bd12813-cebb-4c0f-8844-9f9402869405");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b0b3d043-0f95-45a8-bcd6-0317c8809bf3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "34727dbb-7e4f-46c3-9563-b51eaae5f2a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "5342b5d3-47f1-4118-88ba-7db4a0119372");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "78afd528-24c8-4352-96fb-2015e94ebe9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "a5ee6047-0b53-41fb-aa08-6fde6007416d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "dd61832e-77e0-4134-b26c-998452132967");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFacility_BookingDateId",
                table: "BookingFacility",
                column: "BookingDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomPriceId",
                table: "Booking",
                column: "RoomPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_RoomPrice_RoomPriceId",
                table: "Booking",
                column: "RoomPriceId",
                principalTable: "RoomPrice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingFacility_BookingDate_BookingDateId",
                table: "BookingFacility",
                column: "BookingDateId",
                principalTable: "BookingDate",
                principalColumn: "BookingDateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
