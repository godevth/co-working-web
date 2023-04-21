using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class addTypeRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "36582e20-76bc-4c60-9ee1-1c647232ac4a", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "f86206aa-6567-473e-9459-df907d53af07", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "17ecd97e-7aed-436f-b564-1834276aeb0b", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "94e02c60-8339-469a-be14-86ad76b93b84", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "b6f1bb44-c8a7-4da6-938d-50b566bf5133", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "dda450a5-243a-4b2f-87a5-0c570014685d", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "db99efb5-da99-45e8-8d88-1062c715fa92", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "ebbc3aca-cfee-474f-a024-baad642cce14", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "2d0c3dd7-d0b8-4a54-a97a-4bcf72a84402", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "9bf96cc1-a54b-420a-9559-afa6068b677f", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "0a33dd97-bfc4-42f6-a41a-e608da0e3158", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "2abfde73-77f0-4fa1-bd60-ffb884935b59", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "89f860d9-87cf-4580-a920-acb265fe3deb", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "4c930462-afe3-439d-b5c3-126e45ce0d3b", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "c50ed67d-0a29-4afd-80d2-76f6c06e618e", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                columns: new[] { "ConcurrencyStamp", "UserTypeId" },
                values: new object[] { "931d3e9a-8de6-4959-aebe-89b96037f0b1", null });
        }
    }
}
