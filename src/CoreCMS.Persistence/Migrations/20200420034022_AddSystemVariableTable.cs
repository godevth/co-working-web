using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AddSystemVariableTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemVariableCategories",
                columns: table => new
                {
                    SystemVariableCategoryCode = table.Column<string>(maxLength: 50, nullable: false),
                    SystemVariableCategoryName = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemVariableCategories", x => x.SystemVariableCategoryCode);
                });

            migrationBuilder.CreateTable(
                name: "SystemVariables",
                columns: table => new
                {
                    SystemVariableCode = table.Column<string>(maxLength: 50, nullable: false),
                    SystemVariableCategoryCode = table.Column<string>(nullable: false),
                    SystemVariableName = table.Column<string>(maxLength: 450, nullable: false),
                    Ordering = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemVariables", x => x.SystemVariableCode);
                    table.ForeignKey(
                        name: "FK_SystemVariables_SystemVariableCategories_SystemVariableCategoryCode",
                        column: x => x.SystemVariableCategoryCode,
                        principalTable: "SystemVariableCategories",
                        principalColumn: "SystemVariableCategoryCode",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[] { "MEMBER_CLASS", "ระดับบัตรสมาชิก" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "MEMBER_CLASS_NORMAL", 1, "MEMBER_CLASS", "สมาชิกทั่วไป" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "MEMBER_CLASS_GOLD", 2, "MEMBER_CLASS", "สมาชิกบัตรทอง" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemVariables_SystemVariableCategoryCode",
                table: "SystemVariables",
                column: "SystemVariableCategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemVariables");

            migrationBuilder.DropTable(
                name: "SystemVariableCategories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "3cc900e0-fe1b-4f89-98b2-734dd3d21bf3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fb298815-a3b9-48c6-ab78-de4d484a9bee", "AQAAAAEAACcQAAAAENYoUlZKjD06Egogo8FJQyQiJjB91UReMkF3kI2BzteZSw2ChdAnPlxKiMa3lMAu3g==" });
        }
    }
}
