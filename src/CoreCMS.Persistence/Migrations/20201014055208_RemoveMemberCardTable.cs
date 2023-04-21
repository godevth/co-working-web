using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class RemoveMemberCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberCards");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "8a6fa817-eacf-43b3-9d73-41321db67a72");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0cf7f585-db69-47fd-b2e6-948d969118f7", "AQAAAAEAACcQAAAAEDmV2eK8ZTjkSj2uspj/38mLiRwmeqhjS/njnOgYYOMqUWCfvop8yYWLlAzqCfzd/g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenerateRefCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: false),
                    MemberClassCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RunningNumber = table.Column<int>(type: "int", nullable: false),
                    SecureCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                value: "60d8d923-f0e5-4381-8dce-c3b066306780");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "57b33bb9-2c54-4207-9b51-cf694222dade", "AQAAAAEAACcQAAAAEM3pX5NhgzchG5n4mHiN3GtrF9SF7daNkQ4tlsofyzBRSgKPz5Gwj/BYRKlrlp4vcg==" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_BatchCode",
                table: "MemberCards",
                column: "BatchCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_CardNumber",
                table: "MemberCards",
                column: "CardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_CreatedUserId",
                table: "MemberCards",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_DeletedUserId",
                table: "MemberCards",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_GenerateRefCode",
                table: "MemberCards",
                column: "GenerateRefCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_MemberClassCode",
                table: "MemberCards",
                column: "MemberClassCode");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_RegisteredUserId",
                table: "MemberCards",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_RunningNumber",
                table: "MemberCards",
                column: "RunningNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_UpdatedUserId",
                table: "MemberCards",
                column: "UpdatedUserId");
        }
    }
}
