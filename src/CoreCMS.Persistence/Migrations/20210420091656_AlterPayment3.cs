using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPayment3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payment",
                keyColumn: "PaymentMethodCode",
                keyValue: null,
                column: "PaymentMethodCode",
                value: "PAYMENT_METHOD_COD");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodCode",
                table: "Payment",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
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
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment",
                column: "PaymentMethodCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodCode",
                table: "Payment",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "3c26975e-7749-4d47-89b9-6c2b994b7f52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ba1326d8-fca3-4565-a84c-a5c966f05b0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "eb32fde8-d236-4b50-851f-99e8e5d9b799");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "171f7c05-7ae0-4399-bdef-0b6c5159956e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "33ad8401-cf15-408b-9e06-5e5aa6364070");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "aac007f4-89fe-4f5a-b425-cf19605e1812");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "64087eb0-bc12-4785-9344-9284f3458757");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "f2f2fa8d-b1ca-4887-803d-ba4982d051a6");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment",
                column: "PaymentMethodCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
