using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class InsertUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "16aae268-c544-4039-ad88-51e170a6a72a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "b174f2c3-b3e8-4372-8be8-ae06ff338bfe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "3c9c2f1e-2152-4a57-8c7d-565f78273ff5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "c340c2e6-fcb1-467a-9fa5-87d3a10cd9ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "943c4a27-ff96-4399-9b7e-9789ee54a68f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "b360de3e-5247-430f-82f2-25e32c1f1aab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "3d774d0a-404e-4d85-8801-08d156404e71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "4c5a2779-70ad-4885-8523-0309a921b381");

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "ผู้ดูแลระบบ", "admin", "ADMIN" },
                    { 2, "ร้านค้า", "shop", "SHOP" },
                    { 3, "สมาชิกทั่วไป", "general_member", "GENERAL_MEMBER" },
                    { 4, "ผู้ใช้งานทั่วไป (Guest)", "guest", "GUEST" },
                    { 5, "นิติบุคคล, องค์กรเอกชน, องค์กร์ภาครัฐ", "่juristic", "JURISTIC" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
