using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Remaining",
                table: "Booking",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    ReceiveRunning = table.Column<int>(nullable: false),
                    ReceiveNumber = table.Column<string>(nullable: false),
                    BookingId = table.Column<int>(nullable: false),
                    CounterPaymentCode = table.Column<string>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Receive = table.Column<decimal>(nullable: false),
                    Change = table.Column<decimal>(nullable: false),
                    Remaining = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    ReceiveUserId = table.Column<string>(nullable: true),
                    BankCode = table.Column<string>(nullable: true),
                    CreditCardTypeCode = table.Column<string>(nullable: true),
                    MerchantId = table.Column<string>(nullable: true),
                    RefCode = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_SystemVariables_BankCode",
                        column: x => x.BankCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_SystemVariables_CounterPaymentCode",
                        column: x => x.CounterPaymentCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_SystemVariables_CreditCardTypeCode",
                        column: x => x.CreditCardTypeCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_ReceiveUserId",
                        column: x => x.ReceiveUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "da7e5f9e-7182-476c-afda-4cc731cd03f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "00826cc6-86ec-465d-8f40-0a49554c823c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "fea76700-afcf-4b07-b955-71bf5c0ae0f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "0cf4084f-f95b-404a-88c0-b02799b06eaf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "36a2d1bb-e21b-44bf-aef6-2be1bf52eb8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "3c0548e3-c68c-446e-8ae2-6b46e4d01ca8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "3e209ee8-371d-4fa9-bdcb-e5fab2b2f03f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "44444c9d-523d-44d9-9925-2b5122072aa9");

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[,]
                {
                    { "BANK", "ธนาคาร" },
                    { "COUNTER_PAYMENT", "วิธีชำระเงินหน้าเค้าท์เตอร์" },
                    { "CREDIT_CARD_TYPE", "ประเภทบัตรเครดิต" }
                });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "PAYMENT_METHOD_ONLINE", 2, "PAYMENT_METHOD", "ช่องทางออนไลน์" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[,]
                {
                    { "BANK_BBL", 1, "BANK", "ธนาคารกรุงเทพ" },
                    { "CREDIT_CARD_TYPE_MASTER_CARD", 2, "CREDIT_CARD_TYPE", "Master Card" },
                    { "CREDIT_CARD_TYPE_VISA", 1, "CREDIT_CARD_TYPE", "VISA" },
                    { "COUNTER_PAYMENT_CREDIT_CARD", 3, "COUNTER_PAYMENT", "บัตรเครดิต" },
                    { "COUNTER_PAYMENT_TRANSFER", 2, "COUNTER_PAYMENT", "เงินโอน" },
                    { "COUNTER_PAYMENT_CASH", 1, "COUNTER_PAYMENT", "เงินสด" },
                    { "BANK_GSB", 15, "BANK", "ธนาคารออมสิน" },
                    { "BANK_BAAC", 14, "BANK", "ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร" },
                    { "BANK_LHFG", 13, "BANK", "ธนาคารแลนด์แอนด์ เฮ้าส์" },
                    { "BANK_TCD", 12, "BANK", "ธนาคารไทยเครดิตเพื่อรายย่อย" },
                    { "BANK_UOBT", 11, "BANK", "ธนาคารยูโอบี" },
                    { "BANK_TBANK", 10, "BANK", "ธนาคารธนชาต" },
                    { "BANK_TISCO", 9, "BANK", "ธนาคารทิสโก้" },
                    { "BANK_CIMBT", 8, "BANK", "ธนาคารซีไอเอ็มบีไทย" },
                    { "BANK_KKP", 7, "BANK", "ธนาคารเกียรตินาคินภัทร" },
                    { "BANK_BAY", 6, "BANK", "ธนาคารกรุงศรีอยุธยา" },
                    { "BANK_SCB", 5, "BANK", "ธนาคารไทยพาณิชย์" },
                    { "BANK_TMB", 4, "BANK", "ธนาคารทหารไทย" },
                    { "BANK_KTB", 3, "BANK", "ธนาคารกรุงไทย" },
                    { "BANK_KBANK", 2, "BANK", "ธนาคารกสิกรไทย" },
                    { "CREDIT_CARD_TYPE_JCB", 3, "CREDIT_CARD_TYPE", "JCB" },
                    { "CREDIT_CARD_TYPE_OTHER", 4, "CREDIT_CARD_TYPE", "อื่นๆ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BankCode",
                table: "Payment",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CounterPaymentCode",
                table: "Payment",
                column: "CounterPaymentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreatedUserId",
                table: "Payment",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreditCardTypeCode",
                table: "Payment",
                column: "CreditCardTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DeletedUserId",
                table: "Payment",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_InActiveStatus",
                table: "Payment",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IsDeleted",
                table: "Payment",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentId",
                table: "Payment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReceiveUserId",
                table: "Payment",
                column: "ReceiveUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UpdatedUserId",
                table: "Payment",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_BAAC");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_BAY");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_BBL");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_CIMBT");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_GSB");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_KBANK");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_KKP");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_KTB");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_LHFG");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_SCB");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_TBANK");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_TCD");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_TISCO");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_TMB");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BANK_UOBT");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "COUNTER_PAYMENT_CASH");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "COUNTER_PAYMENT_CREDIT_CARD");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "COUNTER_PAYMENT_TRANSFER");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "CREDIT_CARD_TYPE_JCB");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "CREDIT_CARD_TYPE_MASTER_CARD");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "CREDIT_CARD_TYPE_OTHER");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "CREDIT_CARD_TYPE_VISA");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_ONLINE");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "BANK");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "COUNTER_PAYMENT");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "CREDIT_CARD_TYPE");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "6257ac18-9dde-4bea-afb1-3df8995bc0c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ee9bec4b-dc2c-43b4-8550-e03bc7cd53c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "0418a226-d86c-421d-a653-b3241e380548");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "83ab4ff1-9696-456b-94ed-2eefca782553");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "09901990-f523-4e65-be97-f7302d9bb4d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "02256e20-2ed7-4b89-836b-6b20442385b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d0332598-d4ad-4084-ab6f-23a3e71b7a0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "e8deaa30-ec0c-4e44-91ad-b674f2273a4b");
        }
    }
}
