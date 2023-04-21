using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterIoTDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IoTDevice_SystemVariables_IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_IoTDevice_Room_RoomId",
                table: "IoTDevice");

            migrationBuilder.DropIndex(
                name: "IX_IoTDevice_IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice");

            migrationBuilder.DropIndex(
                name: "IX_IoTDevice_RoomId",
                table: "IoTDevice");

            migrationBuilder.DropColumn(
                name: "IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "IoTDevice");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "IoTDeviceGroup",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "StatusCode",
                table: "IoTDevice",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "641426bd-32d2-4f7e-8490-d1039be73c6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "60c06a96-b927-420b-adbe-10e445938810");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "4f9e86d9-371b-4813-9fa2-e5ddd2436edc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "4478de0a-7ada-4211-80ed-90fd2f16696e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "9de34a28-79fb-4350-865f-76df99f0ef5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "eb120a6a-3ded-4c52-b51c-7eb4d3fd0655");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "98340b95-c7e0-47a7-9f6b-6a5a1ccff494");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "341f61fa-f6ad-4ed0-a0db-0750f4a0a062");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_RoomId",
                table: "IoTDeviceGroup",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_StatusCode",
                table: "IoTDevice",
                column: "StatusCode");

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDevice_SystemVariables_StatusCode",
                table: "IoTDevice",
                column: "StatusCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDeviceGroup_Room_RoomId",
                table: "IoTDeviceGroup",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IoTDevice_SystemVariables_StatusCode",
                table: "IoTDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_IoTDeviceGroup_Room_RoomId",
                table: "IoTDeviceGroup");

            migrationBuilder.DropIndex(
                name: "IX_IoTDeviceGroup_RoomId",
                table: "IoTDeviceGroup");

            migrationBuilder.DropIndex(
                name: "IX_IoTDevice_StatusCode",
                table: "IoTDevice");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "IoTDeviceGroup");

            migrationBuilder.AlterColumn<string>(
                name: "StatusCode",
                table: "IoTDevice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "IoTDevice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "ac4f3a10-5121-40fa-830e-77ec40eb898f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "416a1848-020f-4ceb-81d2-db79fc04772b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "c3dabac6-1240-4be4-86a2-5b51dc687430");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "69bc4ff2-3c33-474c-945e-3936d42ebc08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "62df9955-f10d-448f-bc46-0ab6129d34cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "82b20e7f-5c0d-48f8-ab28-e8fdc9c08aff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "807af904-ccb7-4b58-99b5-f64c063c4331");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "006c15e7-a9bc-4e7a-a3ae-f798a82f9852");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice",
                column: "IoTDeviceStatusSystemVariableCode");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_RoomId",
                table: "IoTDevice",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDevice_SystemVariables_IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice",
                column: "IoTDeviceStatusSystemVariableCode",
                principalTable: "SystemVariables",
                principalColumn: "SystemVariableCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IoTDevice_Room_RoomId",
                table: "IoTDevice",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
