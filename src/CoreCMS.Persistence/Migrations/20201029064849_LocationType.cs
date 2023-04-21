using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class LocationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationType",
                columns: table => new
                {
                    LocationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    LocationTypeName = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationType", x => x.LocationTypeID);
                    table.ForeignKey(
                        name: "FK_LocationType_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationType_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationType_AspNetUsers_UpdatedUserId",
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
                value: "f22de949-313f-45b1-944f-0131e82078c3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3b633d8e-f2f8-4b84-b676-2b1fe8ade361", "AQAAAAEAACcQAAAAEObdpNfQQejEUkBPGjIZa4RzOVKuOBOx2V2u/d5UrJ3ARm1pL3Lv0nhThQBbTRBRrg==" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_CreatedUserId",
                table: "LocationType",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_DeletedUserId",
                table: "LocationType",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_UpdatedUserId",
                table: "LocationType",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationType");

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
    }
}
