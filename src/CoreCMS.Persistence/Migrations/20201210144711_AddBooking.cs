using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    BookingRunning = table.Column<int>(nullable: false),
                    BookingNumber = table.Column<string>(nullable: false),
                    PaymentMethodCode = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsOwner = table.Column<bool>(nullable: false),
                    CustomerFirstname = table.Column<string>(nullable: true),
                    CustomerLastname = table.Column<string>(nullable: true),
                    CustomerEmail = table.Column<string>(nullable: true),
                    CustomerPhoneNumber = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    GrandTotal = table.Column<double>(nullable: false),
                    BookingStatusCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Booking_SystemVariables_BookingStatusCode",
                        column: x => x.BookingStatusCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_SystemVariables_PaymentMethodCode",
                        column: x => x.PaymentMethodCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingDate",
                columns: table => new
                {
                    BookingDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDate", x => x.BookingDateId);
                    table.ForeignKey(
                        name: "FK_BookingDate_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFacility",
                columns: table => new
                {
                    BookingFacilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDateId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFacility", x => x.BookingFacilityId);
                    table.ForeignKey(
                        name: "FK_BookingFacility_BookingDate_BookingDateId",
                        column: x => x.BookingDateId,
                        principalTable: "BookingDate",
                        principalColumn: "BookingDateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFacility_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "2af7ea36-a380-4397-8a16-7add64b0f6f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "e61ac615-6de0-4c55-b749-b1264bfe5a69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "1f7ce76a-68fa-4052-96bf-83300664e5e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "a398963d-6c39-46bb-9902-c896cf189dd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "270b6582-9c2f-485c-8a5a-b9026943889f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "5d09a009-351b-4cdc-a16f-ff8a771991e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "3d644a2d-704b-4cec-b70f-597ad5c5ca81");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "14e5c09b-060f-479f-abcc-480f9d9e9cdd");

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[,]
                {
                    { "PAYMENT_METHOD", "วิธีการชำระเงิน" },
                    { "BOOKING_STATUS", "สถานะการจอง" }
                });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[,]
                {
                    { "PAYMENT_METHOD_COD", 1, "PAYMENT_METHOD", "ชำระเงินปลายทาง" },
                    { "BOOKING_STATUS_RESERVE", 1, "BOOKING_STATUS", "จอง" },
                    { "BOOKING_STATUS_PAID", 2, "BOOKING_STATUS", "จ่ายแล้ว" },
                    { "BOOKING_STATUS_CANCEL", 3, "BOOKING_STATUS", "ยกเลิก" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingId",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingNumber",
                table: "Booking",
                column: "BookingNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingStatusCode",
                table: "Booking",
                column: "BookingStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedUserId",
                table: "Booking",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_DeletedUserId",
                table: "Booking",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_InActiveStatus",
                table: "Booking",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IsDeleted",
                table: "Booking",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PaymentMethodCode",
                table: "Booking",
                column: "PaymentMethodCode");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UpdatedUserId",
                table: "Booking",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDate_BookingDateId",
                table: "BookingDate",
                column: "BookingDateId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDate_BookingId",
                table: "BookingDate",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFacility_BookingDateId",
                table: "BookingFacility",
                column: "BookingDateId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFacility_BookingFacilityId",
                table: "BookingFacility",
                column: "BookingFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFacility_FacilityId",
                table: "BookingFacility",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingFacility");

            migrationBuilder.DropTable(
                name: "BookingDate");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_CANCEL");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_PAID");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "BOOKING_STATUS_RESERVE");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PAYMENT_METHOD_COD");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "BOOKING_STATUS");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "PAYMENT_METHOD");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "43ac0c9d-0c21-4178-a580-381c08b5cacc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "ac0ce757-02b7-4ed8-89aa-8e7e994d672b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "3aec8910-3a49-4d6b-a201-dd07de565932");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "04af972a-66b3-4637-b9b0-b2db747deb86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "2119394d-9945-4be8-bd1b-1294b7ea5de0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "47a1b9fe-0b31-4892-9675-50e6bc6e8504");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "aca2fc3e-acbd-4001-b60c-537f5e14b899");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "f7bd68b6-889c-42bd-ab5f-638a34f91636");
        }
    }
}
