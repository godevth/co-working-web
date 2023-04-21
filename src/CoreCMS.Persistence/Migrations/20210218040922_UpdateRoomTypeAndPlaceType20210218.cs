using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class UpdateRoomTypeAndPlaceType20210218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomTypeNameEN",
                table: "RoomType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceTypeNameEN",
                table: "PlaceType",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "e1567c4b-2109-449f-a758-66ba9bce9377");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "4844149d-c3e6-40d3-83d0-d68c17ce06cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "146d2080-1fed-4461-ab62-d75a87f9b203");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "91c7af6c-5da7-4a92-8d47-8a5b404d87f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "d3b7a104-d221-468e-8065-fef25696efed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "24e361a2-a2a7-4882-821b-0f735cf842c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "7d4b2984-9586-4edd-9eae-fff9ee857405");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "a4c7706b-5cca-4d03-8a7d-c86945e603ee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomTypeNameEN",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "PlaceTypeNameEN",
                table: "PlaceType");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "641426bd-32d2-4f7e-8490-d1039be73c6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "60c06a96-b927-420b-adbe-10e445938810");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "4f9e86d9-371b-4813-9fa2-e5ddd2436edc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "4478de0a-7ada-4211-80ed-90fd2f16696e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "9de34a28-79fb-4350-865f-76df99f0ef5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "eb120a6a-3ded-4c52-b51c-7eb4d3fd0655");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "98340b95-c7e0-47a7-9f6b-6a5a1ccff494");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "341f61fa-f6ad-4ed0-a0db-0750f4a0a062");
        }
    }
}
