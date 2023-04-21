using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class InsertApplicationRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                columns: new[] { "ConcurrencyStamp", "Description" },
                values: new object[] { "83b80aa6-cc6e-4844-b61a-445a2564e6a9", "ผู้ดูแลระบบสูงสุด" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45542588-db7b-4efc-97a7-91f11989d26f", "9101c595-89d1-4f05-9f37-332f9d57a9a9", "ผู้ดูแลระบบ", "admin", "ADMIN" },
                    { "aa2ce0dc-f9fa-4938-9d33-852106fb4993", "68c99579-e222-4c23-bfeb-e2be587800a1", "เจ้าของกิจการ", "business_owner", "BUSINESS_OWNER" },
                    { "21b579c7-038b-4bbb-8d31-f9865ca7861a", "679de1f7-fd2e-4898-b4ad-8af7c1601c55", "พนักงานดูแลระบบ", "system", "SYSTEM" },
                    { "afabc5ea-07e3-4021-8026-258c371558ed", "27d6fc8c-8184-43d1-9655-b322e4ccd798", "สมาชิกระดับ Bronze", "bronze", "BRONZE" },
                    { "5744dd30-16b8-40ac-9ace-00aca29976ce", "29baebc9-6b97-4777-81f6-f3c0481df13e", "สมาชิกระดับ Silver", "silver", "SILVER" },
                    { "2419e2fd-c635-412d-b689-338ea3f13b32", "0be1be4c-3719-476a-bec5-38c26d8a5d29", "สมาชิกระดับ Gold", "gold", "GOLD" },
                    { "e79a9d4e-4aa6-4046-a065-bb822c9f8258", "0a930b95-d5bc-4690-bc6e-784cb5b61433", "สมาชิกระดับ Platinum", "platinum", "PLATINUM" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
