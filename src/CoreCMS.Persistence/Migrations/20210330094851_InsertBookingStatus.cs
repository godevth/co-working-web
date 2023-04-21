using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class InsertBookingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "2b4c4ca8-6757-42b2-9858-e624a3a385b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "2e7e0060-a742-4504-bddb-8c4ac2e6b142");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "46fead47-c595-4f2c-8d81-16a4707585c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "2181fb8a-0ef0-444a-8eb0-813ce8394d34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "01591861-6a8d-43a8-b16e-4071f19c9931");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "cc64e21c-fdfb-40cf-a6d9-81f0e9ef783e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "e2cb21ef-f4a5-49d5-b8c0-93836874da9b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b00f9485-0f43-46b2-9fc7-f1e0e03ff714");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CANCEL",
                columns: new[] { "Ordering", "SystemVariableName" },
                values: new object[] { 7, "ยกเลิกการจอง" });

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CHECKIN",
                column: "Ordering",
                value: 5);

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_COMPLETE",
                column: "Ordering",
                value: 6);

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_PAID",
                columns: new[] { "Ordering", "SystemVariableName" },
                values: new object[] { 3, "ชำระเงินแล้ว" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[,]
                {
                    { "BOOKING_STATUS_WAITING_FOR_PAYMENT", 2, "BOOKING_STATUS", "รอชำระเงิน" },
                    { "BOOKING_STATUS_WAITING_FOR_CHECKIN", 4, "BOOKING_STATUS", " รอ Check In" },
                    { "BOOKING_STATUS_PLACE_CANCEL", 8, "BOOKING_STATUS", "ถูกยกเลิกการจอง" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_PLACE_CANCEL");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_WAITING_FOR_CHECKIN");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_WAITING_FOR_PAYMENT");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "HistoryView",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Tax",
                table: "HistoryView",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "GrandTotal",
                table: "HistoryView",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "HistoryView",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e4469712-d549-4c3c-8ba9-ecbf63aecde3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "9f027e9e-ea8c-4fdf-836d-514cf2879685");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "9ec2febc-0bb3-4930-b908-d9b6d87fbeb8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "61d33f06-151b-454f-8390-f6f9a5912731");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "3a55d4a5-33c3-48ce-8e63-e4fc76291a94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "c940321e-8445-4728-b6db-779e166aed78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "e44ffc02-ae94-4d50-b683-32c845f64a03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "a004c7a4-ec5c-49bf-912b-8dcd41421b9d");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CANCEL",
                columns: new[] { "Ordering", "SystemVariableName" },
                values: new object[] { 3, "ยกเลิก" });

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CHECKIN",
                column: "Ordering",
                value: 4);

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_COMPLETE",
                column: "Ordering",
                value: 5);

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_PAID",
                columns: new[] { "Ordering", "SystemVariableName" },
                values: new object[] { 2, "จ่ายแล้ว" });
        }
    }
}
