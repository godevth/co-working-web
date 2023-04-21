using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class CreatePictureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CodeRef = table.Column<string>(maxLength: 50, nullable: true),
                    TypeRef = table.Column<string>(maxLength: 50, nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: false),
                    FileInfoId = table.Column<string>(maxLength: 100, nullable: false),
                    GridFsId = table.Column<string>(maxLength: 100, nullable: false),
                    SysName = table.Column<string>(maxLength: 50, nullable: false),
                    DownloadUrl = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Picture_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Picture_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Picture_AspNetUsers_UpdatedUserId",
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
                value: "64a1f4b8-b14a-4618-93a1-36d79527ff74");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b3794fa-008c-4b43-9eb4-95236c11d793", "AQAAAAEAACcQAAAAEAvAFOjXXO7hyPd3xx0zuzhb+vR7vFyTxOIYHl/iJWcj5iJdPCnQwUiJNPTUq3ZFXQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CodeRef",
                table: "Picture",
                column: "CodeRef");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_CreatedUserId",
                table: "Picture",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_DeletedUserId",
                table: "Picture",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_InActiveStatus",
                table: "Picture",
                column: "InActiveStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_PictureId",
                table: "Picture",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_TypeRef",
                table: "Picture",
                column: "TypeRef");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_UpdatedUserId",
                table: "Picture",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "af794db8-81e7-4f30-b689-8206bde9fdc8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fd0ee41-4e10-40ee-b114-8b8a8796838b", "AQAAAAEAACcQAAAAEKz1r8vc/hvMDnyEUo7Rop3ZpYPI8hlOxKn06Me7ezgRctV2LQAikneMucmzzfw++A==" });
        }
    }
}
