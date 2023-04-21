using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class FacilityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityType",
                columns: table => new
                {
                    FacilityTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    FacilityTypeName_TH = table.Column<string>(nullable: true),
                    FacilityTypeName_EN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityType", x => x.FacilityTypeID);
                    table.ForeignKey(
                        name: "FK_FacilityType_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacilityType_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacilityType_AspNetUsers_UpdatedUserId",
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
                value: "8ea9e383-2ef6-4fef-a2a2-a449d7448927");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f36022b5-3e8c-4c64-84dd-36f16d52d929", "AQAAAAEAACcQAAAAEG7ilDMDT02m86FwN6jILIOjvRcmSjJUyWJ97tFwTlwjIHGt1udr7qDWngp7d2wrxA==" });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityType_CreatedUserId",
                table: "FacilityType",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityType_DeletedUserId",
                table: "FacilityType",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityType_UpdatedUserId",
                table: "FacilityType",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityType");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "08842d05-3296-40af-a633-95516987442a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "748f988f-4f2d-4115-aeb1-b5d3b3e1bd5e", "AQAAAAEAACcQAAAAELTxE7uQAta4QFKJoLzCCphGH+7yWi8yHkZ/QGHb6INaTUs/6oO/g87tvG412lWsdA==" });
        }
    }
}
