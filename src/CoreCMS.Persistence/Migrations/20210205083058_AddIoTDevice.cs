using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddIoTDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IoTDeviceGroup",
                columns: table => new
                {
                    IoTDeviceGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    GroupName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsOpenPerDevice = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IoTDeviceGroup", x => x.IoTDeviceGroupId);
                    table.ForeignKey(
                        name: "FK_IoTDeviceGroup_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDeviceGroup_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDeviceGroup_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IoTDevice",
                columns: table => new
                {
                    IoTDeviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    DeviceName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IoTDeviceGroupId = table.Column<int>(nullable: false),
                    MongoDeviceId = table.Column<string>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    StatusCode = table.Column<string>(nullable: true),
                    DeviceTypeCode = table.Column<string>(nullable: true),
                    Dimmer = table.Column<int>(nullable: true),
                    IoTDeviceStatusSystemVariableCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IoTDevice", x => x.IoTDeviceId);
                    table.ForeignKey(
                        name: "FK_IoTDevice_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDevice_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDevice_SystemVariables_DeviceTypeCode",
                        column: x => x.DeviceTypeCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDevice_IoTDeviceGroup_IoTDeviceGroupId",
                        column: x => x.IoTDeviceGroupId,
                        principalTable: "IoTDeviceGroup",
                        principalColumn: "IoTDeviceGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IoTDevice_SystemVariables_IoTDeviceStatusSystemVariableCode",
                        column: x => x.IoTDeviceStatusSystemVariableCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTDevice_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IoTDevice_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IoTTransaction",
                columns: table => new
                {
                    IoTTransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    IoTDeviceGroupId = table.Column<int>(nullable: false),
                    IoTDeviceId = table.Column<int>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true),
                    Dimmer = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IoTTransaction", x => x.IoTTransactionId);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_IoTDeviceGroup_IoTDeviceGroupId",
                        column: x => x.IoTDeviceGroupId,
                        principalTable: "IoTDeviceGroup",
                        principalColumn: "IoTDeviceGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_IoTDevice_IoTDeviceId",
                        column: x => x.IoTDeviceId,
                        principalTable: "IoTDevice",
                        principalColumn: "IoTDeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_SystemVariables_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IoTTransaction_AspNetUsers_UpdatedUserId",
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

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[,]
                {
                    { "IOT_DEVICE_STATUS", "สถานะของอุปกรณ์" },
                    { "IOT_DEVICE_TYPE", "ประเภทของอุปกรณ์" }
                });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[,]
                {
                    { "IOT_DEVICE_STATUS_ON", 1, "IOT_DEVICE_STATUS", "เปิด" },
                    { "IOT_DEVICE_STATUS_OFF", 2, "IOT_DEVICE_STATUS", "ปิด" },
                    { "IOT_DEVICE_TYPE_LIGHT", 1, "IOT_DEVICE_TYPE", "ไฟ" },
                    { "IOT_DEVICE_TYPE_PLUG", 2, "IOT_DEVICE_TYPE", "ปลั๊ก" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_CreatedUserId",
                table: "IoTDevice",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_DeletedUserId",
                table: "IoTDevice",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_DeviceTypeCode",
                table: "IoTDevice",
                column: "DeviceTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_InActiveStatus",
                table: "IoTDevice",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_IoTDeviceGroupId",
                table: "IoTDevice",
                column: "IoTDeviceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_IoTDeviceId",
                table: "IoTDevice",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_IoTDeviceStatusSystemVariableCode",
                table: "IoTDevice",
                column: "IoTDeviceStatusSystemVariableCode");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_IsDeleted",
                table: "IoTDevice",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_RoomId",
                table: "IoTDevice",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDevice_UpdatedUserId",
                table: "IoTDevice",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_CreatedUserId",
                table: "IoTDeviceGroup",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_DeletedUserId",
                table: "IoTDeviceGroup",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_InActiveStatus",
                table: "IoTDeviceGroup",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_IoTDeviceGroupId",
                table: "IoTDeviceGroup",
                column: "IoTDeviceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_IsDeleted",
                table: "IoTDeviceGroup",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDeviceGroup_UpdatedUserId",
                table: "IoTDeviceGroup",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_CreatedUserId",
                table: "IoTTransaction",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_DeletedUserId",
                table: "IoTTransaction",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_InActiveStatus",
                table: "IoTTransaction",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_IoTDeviceGroupId",
                table: "IoTTransaction",
                column: "IoTDeviceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_IoTDeviceId",
                table: "IoTTransaction",
                column: "IoTDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_IoTTransactionId",
                table: "IoTTransaction",
                column: "IoTTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_IsDeleted",
                table: "IoTTransaction",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_StatusCode",
                table: "IoTTransaction",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_IoTTransaction_UpdatedUserId",
                table: "IoTTransaction",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IoTTransaction");

            migrationBuilder.DropTable(
                name: "IoTDevice");

            migrationBuilder.DropTable(
                name: "IoTDeviceGroup");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "IOT_DEVICE_STATUS_OFF");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "IOT_DEVICE_STATUS_ON");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "IOT_DEVICE_TYPE_LIGHT");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "IOT_DEVICE_TYPE_PLUG");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "IOT_DEVICE_STATUS");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "IOT_DEVICE_TYPE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e0fa1b3b-1c62-464b-926d-cf75f13fd1f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "d2ec1454-f848-4894-851e-f9de30626c52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "a50939c1-4425-4df3-b24d-1cb209e516ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "a52f0294-8977-4db5-8e57-4eed361c5ad5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "08b278ba-1a5d-495d-adc4-3c60a90a8a46");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "37f44a2a-73d1-4025-8171-ea1b667e365f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "b7c96690-1504-46af-beae-8d99f573b95f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "d15eeed9-b281-473f-b592-4c9674b11f77");
        }
    }
}
