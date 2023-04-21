using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    PlaceName = table.Column<string>(maxLength: 450, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    TAMBON_ID = table.Column<int>(maxLength: 10, nullable: true),
                    AMPER_ID = table.Column<int>(maxLength: 10, nullable: true),
                    PROVINCE_ID = table.Column<int>(maxLength: 10, nullable: true),
                    ZIP_CODE = table.Column<string>(maxLength: 5, nullable: true),
                    PlaceTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_Place_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Place_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Place_PlaceType_PlaceTypeID",
                        column: x => x.PlaceTypeID,
                        principalTable: "PlaceType",
                        principalColumn: "PlaceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Place_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImplementationDate",
                columns: table => new
                {
                    ImplementationDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUserId = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    InActiveStatus = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    StartDay = table.Column<string>(nullable: true),
                    EndDay = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    PlaceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImplementationDate", x => x.ImplementationDateId);
                    table.ForeignKey(
                        name: "FK_ImplementationDate_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImplementationDate_AspNetUsers_DeletedUserId",
                        column: x => x.DeletedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImplementationDate_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImplementationDate_AspNetUsers_UpdatedUserId",
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
                value: "6674b67c-f24c-42cb-a3dc-10cda31d1734");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "39f71b09-85fc-4e70-9e1a-6c3d7978810a", "AQAAAAEAACcQAAAAELqJzeqdY8QSZjiTVnYjNpWaJ3K8uDWP78eWgXMlza29tJURs6BsClQd8vRpN4IAYQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_ImplementationDate_CreatedUserId",
                table: "ImplementationDate",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementationDate_DeletedUserId",
                table: "ImplementationDate",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementationDate_PlaceId",
                table: "ImplementationDate",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementationDate_UpdatedUserId",
                table: "ImplementationDate",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_CreatedUserId",
                table: "Place",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_DeletedUserId",
                table: "Place",
                column: "DeletedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_PlaceTypeID",
                table: "Place",
                column: "PlaceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Place_UpdatedUserId",
                table: "Place",
                column: "UpdatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImplementationDate");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "a83d1f6e-34af-4b8d-a340-a42008c02be2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "674aaaec-55f9-4038-9dcb-1d187f2b48ea", "AQAAAAEAACcQAAAAEEu5aiY65oarQowsQgS6ORJ3IDb1zp6sKNixUt7nYKCvllwYzoohCazieQzegaNuEg==" });
        }
    }
}
