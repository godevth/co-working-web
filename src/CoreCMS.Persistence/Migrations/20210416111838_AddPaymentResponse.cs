using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddPaymentResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantID = table.Column<string>(maxLength: 25, nullable: false),
                    BookingNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(12,5)", nullable: true),
                    McpAmount = table.Column<decimal>(type: "decimal(12,5)", nullable: true),
                    McpFxRate = table.Column<decimal>(type: "decimal(12,7)", nullable: true),
                    McpCurrencyCode = table.Column<string>(maxLength: 3, nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 3, nullable: false),
                    TransactionDateTime = table.Column<string>(maxLength: 14, nullable: false),
                    AgentCode = table.Column<string>(maxLength: 30, nullable: false),
                    ChannelCode = table.Column<string>(maxLength: 30, nullable: false),
                    ApprovalCode = table.Column<string>(maxLength: 6, nullable: true),
                    ReferenceNo = table.Column<string>(maxLength: 15, nullable: true),
                    Pan = table.Column<string>(maxLength: 19, nullable: true),
                    CardToken = table.Column<string>(maxLength: 20, nullable: true),
                    IssuerCountry = table.Column<string>(maxLength: 2, nullable: true),
                    Eci = table.Column<string>(maxLength: 2, nullable: true),
                    InstallmentPeriod = table.Column<string>(maxLength: 2, nullable: true),
                    InterestType = table.Column<string>(maxLength: 1, nullable: true),
                    InterestRate = table.Column<decimal>(type: "decimal(12,5)", nullable: true),
                    InstallmentMerchantAbsorbRate = table.Column<decimal>(type: "decimal(12,5)", nullable: true),
                    RecurringUniqueID = table.Column<string>(maxLength: 20, nullable: true),
                    FxAmount = table.Column<decimal>(type: "decimal(12,5)", nullable: true),
                    FxRate = table.Column<decimal>(type: "decimal(12,7)", nullable: true),
                    FxCurrencyCode = table.Column<string>(maxLength: 3, nullable: true),
                    UserDefined1 = table.Column<string>(maxLength: 255, nullable: true),
                    UserDefined2 = table.Column<string>(maxLength: 255, nullable: true),
                    UserDefined3 = table.Column<string>(maxLength: 255, nullable: true),
                    UserDefined4 = table.Column<string>(maxLength: 255, nullable: true),
                    UserDefined5 = table.Column<string>(maxLength: 255, nullable: true),
                    RespCode = table.Column<string>(maxLength: 4, nullable: true),
                    RespDesc = table.Column<string>(maxLength: 255, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    BookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentResponse_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e3e9f1e0-a368-4b89-8d34-b3d4d16cde32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "2a57102e-42c8-4b79-815b-27baa7ffe7ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "84bb8bed-edf7-4471-bec7-bba3ee9a65b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "00ea900f-b1a4-4669-b6ad-43faded34192");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "592f7339-0e77-4ebb-8ee7-b383f9b4b6f8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "66da0c5a-19c5-402b-bfe3-66ed2736cd37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "9212cc69-f6f6-457a-a64c-35b2c6765287");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "3bf07014-64b3-4d0b-9619-c85c05594480");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_BookingId",
                table: "PaymentResponse",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_BookingNumber",
                table: "PaymentResponse",
                column: "BookingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_ChannelCode",
                table: "PaymentResponse",
                column: "ChannelCode");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_Id",
                table: "PaymentResponse",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_MerchantID",
                table: "PaymentResponse",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_RespCode",
                table: "PaymentResponse",
                column: "RespCode");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponse_RespDesc",
                table: "PaymentResponse",
                column: "RespDesc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentResponse");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "45b14873-a2b3-4076-bfd5-d8958c707429");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "80f00fb0-a618-48af-9314-4b31442329b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "284c4dfc-b9c2-48ef-932c-0f080cbf5036");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "e4e6e088-e2f2-4a59-81b1-38e57a63a092");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "04c8013f-d27d-4882-8648-e0f65b55625f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "9be450a7-e46e-43d8-8898-5eb52502eafc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "c35b27f6-da4e-4a1b-bc42-62c451a3bc0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "341446c4-f724-4247-b081-e07aed75124e");
        }
    }
}
