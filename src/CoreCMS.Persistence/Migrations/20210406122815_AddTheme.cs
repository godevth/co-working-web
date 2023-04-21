using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceTheme",
                columns: table => new
                {
                    ThemeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceTheme", x => x.ThemeId);
                    table.ForeignKey(
                        name: "FK_PlaceTheme_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceTheme_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceTheme_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceTheme_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTheme",
                columns: table => new
                {
                    UserThemeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ThemeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTheme", x => x.UserThemeId);
                    table.ForeignKey(
                        name: "FK_UserTheme_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTheme_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTheme_PlaceTheme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "PlaceTheme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTheme_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTheme_AspNetUsers_UserId",
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

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[] { "THEME_TYPE", "ประเภทของธีม" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[,]
                {
                    { "THEME_TYPE_LOGO_LIGHT", 1, "THEME_TYPE", "รูปโลโก้แบบสว่าง" },
                    { "THEME_TYPE_LOGO_DARK", 2, "THEME_TYPE", "รูปโลโก้แบบมืด" },
                    { "THEME_TYPE_BG_LIGHT", 3, "THEME_TYPE", "รูปพื้นหลังแบบสว่าง" },
                    { "THEME_TYPE_BG_DARK", 4, "THEME_TYPE", "รูปพื้นหลังแบบมืด" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_CreatedUserId",
                table: "PlaceTheme",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_DeletedUserId",
                table: "PlaceTheme",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_InActiveStatus",
                table: "PlaceTheme",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_IsDeleted",
                table: "PlaceTheme",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_PlaceId",
                table: "PlaceTheme",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_ThemeId",
                table: "PlaceTheme",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceTheme_UpdatedUserId",
                table: "PlaceTheme",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_CreatedUserId",
                table: "UserTheme",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_DeletedUserId",
                table: "UserTheme",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_InActiveStatus",
                table: "UserTheme",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_IsDeleted",
                table: "UserTheme",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_ThemeId",
                table: "UserTheme",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_UpdatedUserId",
                table: "UserTheme",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_UserId",
                table: "UserTheme",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_UserThemeId",
                table: "UserTheme",
                column: "UserThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConsentPersistedGrantsView_TermAndCondition_TermId",
                table: "UserConsentPersistedGrantsView");

            migrationBuilder.DropTable(
                name: "UserTheme");

            migrationBuilder.DropTable(
                name: "PlaceTheme");

            migrationBuilder.DropIndex(
                name: "IX_UserConsentPersistedGrantsView_TermId",
                table: "UserConsentPersistedGrantsView");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "THEME_TYPE_BG_DARK");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "THEME_TYPE_BG_LIGHT");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "THEME_TYPE_LOGO_DARK");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "THEME_TYPE_LOGO_LIGHT");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "THEME_TYPE");

            migrationBuilder.AddColumn<int>(
                name: "TermAndConditionTermId",
                table: "UserConsentPersistedGrantsView",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "297d02bc-e5c4-49c9-89d3-944345fe106a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "99fe39b9-2319-41fd-a9f1-4dcdaf2e4988");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "b134ae96-3085-4333-a040-b5e287e893e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "881c27ab-33ee-4d04-b032-b006f27fa191");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "058f4c22-8e58-4594-9f38-7fad4c9b91c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "ab4b81a6-58af-4ce8-9a4d-7c0d0d10e16b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "d837d0d3-0bb7-4967-9173-779fabdfe6cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "972ec142-1b75-42d7-9b6b-0b8ecfb9875d");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsentPersistedGrantsView_TermAndConditionTermId",
                table: "UserConsentPersistedGrantsView",
                column: "TermAndConditionTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConsentPersistedGrantsView_TermAndCondition_TermAndConditionTermId",
                table: "UserConsentPersistedGrantsView",
                column: "TermAndConditionTermId",
                principalTable: "TermAndCondition",
                principalColumn: "TermId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
