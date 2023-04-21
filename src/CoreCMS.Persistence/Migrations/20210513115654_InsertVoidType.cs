using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class InsertVoidType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "68fae768-8964-4571-9d2c-636deb8f0911");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "7f6ae83a-5de8-4b98-bc4c-fea5b2c8ccd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "d6822b1d-58cb-4a2a-99ac-77ef5921f7f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "b7b930b1-dd68-48a8-8900-2104c63a0674");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "73820e7b-bc20-458b-a63f-7a1d3a16ea7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "13930600-8479-4246-9207-fd19983c2c51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "52a5441c-1712-492f-a1b6-caa72a3f2117");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "1e1c0c87-2062-4b20-ab59-9c03e3735879");

            migrationBuilder.InsertData(
                table: "SystemVariableCategories",
                columns: new[] { "SystemVariableCategoryCode", "SystemVariableCategoryName" },
                values: new object[] { "VOID_TYPE", "ประเภทการคืนเงิน" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "VOID_TYPE_IN_PROGRESS", 1, "VOID_TYPE", "อยู่ระหว่างดำเนินการ" });

            migrationBuilder.InsertData(
                table: "SystemVariables",
                columns: new[] { "SystemVariableCode", "Ordering", "SystemVariableCategoryCode", "SystemVariableName" },
                values: new object[] { "VOID_TYPE_REFUNDED", 2, "VOID_TYPE", "คืนเงิน" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "VOID_TYPE_IN_PROGRESS");

            migrationBuilder.DeleteData(
                table: "SystemVariables",
                keyColumn: "SystemVariableCode",
                keyValue: "VOID_TYPE_REFUNDED");

            migrationBuilder.DeleteData(
                table: "SystemVariableCategories",
                keyColumn: "SystemVariableCategoryCode",
                keyValue: "VOID_TYPE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "0d19ec40-e615-46f1-b1c3-c69f7a401b34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "3b03f170-e834-4899-b439-a9632323284f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "5db63ac5-ae24-4856-9892-296264e1392e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "15eccb98-ceaa-485b-a108-4458427f5804");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "c4830d79-6976-47c3-9964-ada417fd2399");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "8d62cdd4-1f17-4c02-86c8-6e407b7a50a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "cf49a47d-85b7-4cea-8609-0ed1193e3c44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "f0d864b1-15d1-4432-8306-37dbcdb926e9");
        }
    }
}
