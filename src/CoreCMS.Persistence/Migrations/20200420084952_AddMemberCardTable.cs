using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddMemberCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberCards",
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
                    CardNumber = table.Column<string>(maxLength: 50, nullable: false),
                    MemberClassCode = table.Column<string>(nullable: false),
                    RunningNumber = table.Column<int>(nullable: false),
                    SecureCode = table.Column<string>(maxLength: 10, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    GenerateRefCode = table.Column<string>(maxLength: 50, nullable: false),
                    IsRegistered = table.Column<bool>(nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: true),
                    RegisteredUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCards_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCards_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCards_SystemVariables_MemberClassCode",
                        column: x => x.MemberClassCode,
                        principalTable: "SystemVariables",
                        principalColumn: "SystemVariableCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberCards_AspNetUsers_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCards_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "cf91291a-d789-42f7-bbbd-3249664932cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "485bf866-dc9a-4399-afb6-ad3558ef5d2b", "AQAAAAEAACcQAAAAEJ7kGhXs1KwntY0qFK0hYVCI26xbcTIaog0F9VuHyq0ueLrKxatcgG6idgUzF6DBIQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_CreatedUserId",
                table: "MemberCards",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_DeletedUserId",
                table: "MemberCards",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_MemberClassCode",
                table: "MemberCards",
                column: "MemberClassCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_RegisteredUserId",
                table: "MemberCards",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_UpdatedUserId",
                table: "MemberCards",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberCards");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "82969506-8555-49bc-ad06-ebe0f4497d11");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "02975ff9-754b-4785-a221-55f9e37f7491", "AQAAAAEAACcQAAAAEGBKVxmdbwiY0/jzomjYH0SqC8XhGpdV+pJRHif4D5b2Wy3lde8MJCuRBAppKDZ2RA==" });
        }
    }
}
