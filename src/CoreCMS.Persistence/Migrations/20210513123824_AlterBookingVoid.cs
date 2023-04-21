using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterBookingVoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefundedBy",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefundedDate",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoidCode",
                table: "Booking",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "ba0bb87c-7a72-4027-8e54-8d9b7143650f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "83eb8e15-8aeb-4504-aae7-548d2b0c3da7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "7c2e415b-260c-4e93-b0cb-ba8ad55eeab1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "f80a9d84-f5db-436e-9798-04677f73b952");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "1098bc18-3160-4e03-a773-dce84224d982");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "0f8cd82a-cb03-4aa6-9cd9-65726ff8cbd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b6e1c840-ff50-4867-b108-8b1d45226b93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "4b6f9cd8-a3a4-4224-921a-4ed115fae30c");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RefundedBy",
                table: "Booking",
                column: "RefundedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_VoidCode",
                table: "Booking",
                column: "VoidCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_RefundedBy",
                table: "Booking",
                column: "RefundedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_SystemVariables_VoidCode",
                table: "Booking",
                column: "VoidCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_RefundedBy",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_SystemVariables_VoidCode",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingView_AspNetUsers_RefundedBy",
                table: "BookingView");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingView_SystemVariables_VoidCode",
                table: "BookingView");

            migrationBuilder.DropIndex(
                name: "IX_BookingView_RefundedBy",
                table: "BookingView");

            migrationBuilder.DropIndex(
                name: "IX_BookingView_VoidCode",
                table: "BookingView");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RefundedBy",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_VoidCode",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RefundedBy",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "RefundedDate",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "VoidCode",
                table: "BookingView");

            migrationBuilder.DropColumn(
                name: "RefundedBy",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RefundedDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "VoidCode",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "68fae768-8964-4571-9d2c-636deb8f0911");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "7f6ae83a-5de8-4b98-bc4c-fea5b2c8ccd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "d6822b1d-58cb-4a2a-99ac-77ef5921f7f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b7b930b1-dd68-48a8-8900-2104c63a0674");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "73820e7b-bc20-458b-a63f-7a1d3a16ea7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "13930600-8479-4246-9207-fd19983c2c51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "52a5441c-1712-492f-a1b6-caa72a3f2117");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "1e1c0c87-2062-4b20-ab59-9c03e3735879");
        }
    }
}
