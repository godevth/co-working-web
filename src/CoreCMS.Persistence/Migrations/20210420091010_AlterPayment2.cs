using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterPayment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethodCode",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentResponseId",
                table: "Payment",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_COD",
                column: "SystemVariableName",
                value: "Pay at the front desk");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_ONLINE",
                column: "SystemVariableName",
                value: "Payment Online");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodCode",
                table: "Payment",
                column: "PaymentMethodCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentResponseId",
                table: "Payment",
                column: "PaymentResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment",
                column: "PaymentMethodCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentResponse_PaymentResponseId",
                table: "Payment",
                column: "PaymentResponseId",
                principalTable: "PaymentResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_SystemVariables_PaymentMethodCode",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentResponse_PaymentResponseId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_PaymentMethodCode",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_PaymentResponseId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentMethodCode",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "PaymentResponseId",
                table: "Payment");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "ca737fe0-1219-46ee-b680-8d24fcc906d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "63947874-fff0-465e-b9c8-91a5cec96580");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "e8f68342-ff20-42c5-99ec-cf5877d06536");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "eb7f5406-aae2-4dc5-ab2d-cb8103f78a4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "b051c78d-ee9e-4536-b330-b26f88dc9ce7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "331f5840-57ba-43ca-a647-fe5ff36de006");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "cc466979-926f-4e23-9b47-9f7b9f942f78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "96a1447c-c3ec-4aa4-9532-04f11e35d873");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_COD",
                column: "SystemVariableName",
                value: "ชำระเงินปลายทาง");

            migrationBuilder.UpdateData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_ONLINE",
                column: "SystemVariableName",
                value: "ช่องทางออนไลน์");
        }
    }
}
