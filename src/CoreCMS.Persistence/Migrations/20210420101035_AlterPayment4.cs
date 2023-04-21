using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPayment4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_SystemVariables_CounterPaymentCode",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "CounterPaymentCode",
                table: "Payment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "67270565-d7ef-4948-97c8-72da047bb457");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "8506715d-9a3a-4167-b947-4cee15e0e23e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "61755d63-ae4e-4139-9bd9-f3e47d7a3a5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b9bb5e89-a39f-414e-90fe-ea0a32a4a5d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "62a727c0-3593-4bfb-9b13-67870ebda764");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "70965e46-b260-4398-87db-3d35acb00837");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "c8e4e7c3-9f49-46fc-b68e-1996e6a7cc10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "abf577eb-4047-46e6-b657-963f9ec2a727");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_SystemVariables_CounterPaymentCode",
                table: "Payment",
                column: "CounterPaymentCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_SystemVariables_CounterPaymentCode",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "CounterPaymentCode",
                table: "Payment",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e05133cf-7a79-4e6b-a7bc-4cc8a06dfe56");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "182b423d-a920-4336-aadf-c5bb7303c4c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "2faa9884-7cb7-46e1-be4e-0b857db621f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "2a3266ad-b2e6-48f3-8bc6-0118caa7a9a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "64d5a380-3953-4b34-be83-3739423dbc53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "a87f520c-8f82-4edb-8c13-e565b0bc82d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "fab5bd62-8b0d-440f-8586-108abb2a04ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "152394ac-fb51-41b3-8e56-b352db059547");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_SystemVariables_CounterPaymentCode",
                table: "Payment",
                column: "CounterPaymentCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
