using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddTermAndConditionUserConsent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TermAndCondition",
                columns: table => new
                {
                    TermId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TermTH = table.Column<string>(nullable: true),
                    TermEN = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermAndCondition", x => x.TermId);
                    table.ForeignKey(
                        name: "FK_TermAndCondition_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermAndCondition_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermAndCondition_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TermAndCondition_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserConsent",
                columns: table => new
                {
                    UserConsentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    PersistedGrantsKey = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConsent", x => x.UserConsentId);
                    table.ForeignKey(
                        name: "FK_UserConsent_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserConsent_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserConsent_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConsent_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserConsent_AspNetUsers_UserId",
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
                value: "40f33cf4-ac0d-4faa-a9de-93932ed98aca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "cc7f9941-d29e-4161-8766-651d9b4e1892");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "a5ea3a20-82b8-4875-b64d-86efc765f6a8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "f3a8fe63-1380-45fa-a3ac-35ef21f65c2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "0026cdee-fe0e-4890-aca7-002520904e72");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "ef498927-6d81-44ad-86ba-914236277835");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "27dd648c-5cfd-4be3-9780-5d1d5135ab6e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "a8d71ffb-a506-416f-b9fa-0afa381cba00");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_CreatedUserId",
                table: "TermAndCondition",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_DeletedUserId",
                table: "TermAndCondition",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_InActiveStatus",
                table: "TermAndCondition",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_IsDeleted",
                table: "TermAndCondition",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_PlaceId",
                table: "TermAndCondition",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_TermId",
                table: "TermAndCondition",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_TermAndCondition_UpdatedUserId",
                table: "TermAndCondition",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_CreatedUserId",
                table: "UserConsent",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_DeletedUserId",
                table: "UserConsent",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_InActiveStatus",
                table: "UserConsent",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_IsDeleted",
                table: "UserConsent",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_PersistedGrantsKey",
                table: "UserConsent",
                column: "PersistedGrantsKey");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_PlaceId",
                table: "UserConsent",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_UpdatedUserId",
                table: "UserConsent",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_UserConsentId",
                table: "UserConsent",
                column: "UserConsentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsent_UserId",
                table: "UserConsent",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermAndCondition");

            migrationBuilder.DropTable(
                name: "UserConsent");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "2b4c4ca8-6757-42b2-9858-e624a3a385b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "2e7e0060-a742-4504-bddb-8c4ac2e6b142");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "46fead47-c595-4f2c-8d81-16a4707585c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "2181fb8a-0ef0-444a-8eb0-813ce8394d34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "01591861-6a8d-43a8-b16e-4071f19c9931");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "cc64e21c-fdfb-40cf-a6d9-81f0e9ef783e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "e2cb21ef-f4a5-49d5-b8c0-93836874da9b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "b00f9485-0f43-46b2-9fc7-f1e0e03ff714");
        }
    }
}
