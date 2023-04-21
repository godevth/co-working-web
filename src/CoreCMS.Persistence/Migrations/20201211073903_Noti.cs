using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class Noti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserOwnerId = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    OS = table.Column<string>(nullable: true),
                    ResponeDeviceId = table.Column<string>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_AspNetUsers_UserOwnerId",
                        column: x => x.UserOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    NotiCode = table.Column<string>(nullable: false),
                    NotiTypeCode = table.Column<string>(nullable: false),
                    UserOwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_SystemVariables_NotiTypeCode",
                        column: x => x.NotiTypeCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserOwnerId",
                        column: x => x.UserOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badges_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponeJobId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    SetTimeSend = table.Column<long>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
                    NotificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogNotification_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogNotification_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "3b8c2441-84c6-4adb-b39d-fbd9b2e2d5e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "95c6cf90-4a55-4f38-b7f3-fa8602cd0978");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b108980f-a3c0-42c2-b83e-f08bd58cd692");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "bccc57b1-40b4-4182-96fc-452136800a3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "77e39d4f-2cc6-44b6-8a78-470b2c2c053f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "1d8eb8b3-281c-4e67-993f-96d8b8a07817");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "febff22b-49b6-402f-882d-7a4274bfe3a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "d2eaf83b-0b6a-497a-8425-c298cb841767");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_DeviceId",
                table: "Badges",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserOwnerId",
                table: "Devices",
                column: "UserOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotification_DeviceId",
                table: "LogNotification",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotification_NotificationId",
                table: "LogNotification",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedUserId",
                table: "Notifications",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DeletedUserId",
                table: "Notifications",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotiTypeCode",
                table: "Notifications",
                column: "NotiTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UpdatedUserId",
                table: "Notifications",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserOwnerId",
                table: "Notifications",
                column: "UserOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "LogNotification");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "05a39647-16f9-4ff9-b938-303229fd7a76");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "b1a5c4a2-c877-4f4f-936b-232209ea9d26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "f923fcb3-58bd-4dfd-8550-594d0d3dfe1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "e2f15de0-cb70-4bce-a3cc-09dec1200b61");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "7194f04f-b23e-474b-841f-918d75bae40c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "08652de9-7c75-497f-9548-731ce38f154f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "f1c31b7f-a926-46fd-bba5-0dfd402b1b04");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "361871c0-ea1a-4d0e-812a-3a4609289819");
        }
    }
}
