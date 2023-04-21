using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddPrivilegePlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlacePaymentMethod",
                columns: table => new
                {
                    PlacePaymentMethodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<Guid>(nullable: false),
                    PaymentMethodCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacePaymentMethod", x => x.PlacePaymentMethodId);
                    table.ForeignKey(
                        name: "FK_PlacePaymentMethod_SystemVariables_PaymentMethodCode",
                        column: x => x.PaymentMethodCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacePaymentMethod_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    PrivilegeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Domain = table.Column<string>(maxLength: 450, nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: true),
                    PrivilegeTypeCode = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.PrivilegeId);
                    table.ForeignKey(
                        name: "FK_Privilege_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Privilege_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Privilege_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Privilege_SystemVariables_PrivilegeTypeCode",
                        column: x => x.PrivilegeTypeCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Privilege_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Privilege_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "6c9674eb-0238-4b30-9c6f-919465a98cf9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "2b697969-4a92-471a-9e62-811d86704a26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "7e9faac0-1fd0-4c1b-8883-ec35715b781e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "1ba9ec7e-65bb-4659-b5df-6f00b23ae361");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "4d35a679-70d2-4198-a10e-e03e747fc7a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "57e29995-c14d-404b-9d85-a171d1614383");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "75b58107-0415-4939-8764-922af739bfc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b7447d1b-182d-4ad8-b6ed-8b579f93ea8f");

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[] { "PRIVILEGE_TYPE", "ประเภทของสิทธิพิเศษ" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "PRIVILEGE_TYPE_DOMAIN", 1, "PRIVILEGE_TYPE", "โดเมน" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "PRIVILEGE_TYPE_PERSON", 2, "PRIVILEGE_TYPE", "ผู้ใช้งาน" });

            migrationBuilder.CreateIndex(
                name: "IX_PlacePaymentMethod_PaymentMethodCode",
                table: "PlacePaymentMethod",
                column: "PaymentMethodCode");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePaymentMethod_PlaceId",
                table: "PlacePaymentMethod",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePaymentMethod_PlacePaymentMethodId",
                table: "PlacePaymentMethod",
                column: "PlacePaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_CreatedUserId",
                table: "Privilege",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_DeletedUserId",
                table: "Privilege",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_InActiveStatus",
                table: "Privilege",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_IsDeleted",
                table: "Privilege",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_PlaceId",
                table: "Privilege",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_PrivilegeId",
                table: "Privilege",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_PrivilegeTypeCode",
                table: "Privilege",
                column: "PrivilegeTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_UpdatedUserId",
                table: "Privilege",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_UserId",
                table: "Privilege",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlacePaymentMethod");

            migrationBuilder.DropTable(
                name: "Privilege");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PRIVILEGE_TYPE_DOMAIN");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "PRIVILEGE_TYPE_PERSON");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "PRIVILEGE_TYPE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "6139294d-27bb-4888-b0eb-7d5456db62c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "a12aa807-bc9a-4387-8683-7b80c36f2b0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "f2548114-2f89-40a2-a970-125b0b7093c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "c0411ff5-ca39-4775-80a0-a2894b6c57f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "6ff98a41-0980-440f-8d70-7ae2519b61d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "59fe65f2-b1a8-4e06-8e7e-cee46801d12b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d0a81608-84ee-4a6c-8265-e0907e9ab769");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "4b7cdfb8-3e84-4bc2-9bee-89ec9682c30b");
        }
    }
}
