using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterUserSearchView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserSearchView] as 
                SELECT  AspNetUsers.Id , AspNetUsers.UserName ,AspNetUsers.NormalizedUserName , AspNetUsers.Email , AspNetUsers.NormalizedEmail , AspNetUsers.EmailConfirmed , AspNetUsers.PhoneNumber ,
                AspNetUsers.PhoneNumberConfirmed , AspNetUsers.TwoFactorEnabled , AspNetUsers.LockoutEnd , AspNetUsers.LockoutEnabled , AspNetUsers.AccessFailedCount , AspNetUsers.CreateUserDateTime ,
                AspNetUsers.FirstName , AspNetUsers.LastName , AspNetUsers.DisplayName , AspNetUsers.LastLogOnDateTime ,AspNetUsers.OpenID , AspNetUsers.[Position] , 
                CASE WHEN AspNetRoles.Id IS NOT NULL THEN AspNetRoles.Description +' [' + AspNetRoles.Name  + ']' ELSE NULL END AS Roles ,
                AspNetUsers.InActiveStatus , AspNetUsers.IsDeleted , AspNetRoles.UserTypeId , UserType.Description AS UserType
                FROM AspNetUsers LEFT OUTER JOIN
	                AspNetUserRoles ON AspNetUserRoles.UserId = AspNetUsers.Id LEFT OUTER JOIN 
	                AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id LEFT OUTER JOIN 
	                UserType ON UserType.UserTypeId = AspNetRoles.UserTypeId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "7d5d38bb-2931-4e22-bd6a-1c2335a9cabc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "bf512326-04f6-41aa-ba28-e3a6352951b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "0f8aca1a-cd1a-4e38-8a73-add7452717c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "2773e147-5b81-4b1d-9ad2-51a22bc2bb26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "573e7817-6e56-4d63-ad74-29313b0b1720");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "0f5d6b5b-9067-4483-9589-1f1f11524567");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "ead7fcf6-7ece-426d-bd63-205768cddd8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "646db4c6-a16d-4d21-a300-a5362163b5f0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21b579c7-038b-4bbb-8d31-f9865ca7861a",
                column: "ConcurrencyStamp",
                value: "86e13b75-9476-4d10-b80f-a9e381ede05d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2419e2fd-c635-412d-b689-338ea3f13b32",
                column: "ConcurrencyStamp",
                value: "94b44185-2e1c-46cd-b15c-93626226514d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45542588-db7b-4efc-97a7-91f11989d26f",
                column: "ConcurrencyStamp",
                value: "7990d418-cd26-4cfc-9488-68ffb8214f6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5744dd30-16b8-40ac-9ace-00aca29976ce",
                column: "ConcurrencyStamp",
                value: "20bdca22-3c0b-4da0-89e1-9ee4da583a00");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                column: "ConcurrencyStamp",
                value: "a126915a-6bc6-44c3-ba03-c5d30a58783d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ce0dc-f9fa-4938-9d33-852106fb4993",
                column: "ConcurrencyStamp",
                value: "01fcc3b3-1b0c-41bc-9c3b-a6816e5f7934");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabc5ea-07e3-4021-8026-258c371558ed",
                column: "ConcurrencyStamp",
                value: "f5ab7781-a4b8-4c22-be8b-79adead315a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e79a9d4e-4aa6-4046-a065-bb822c9f8258",
                column: "ConcurrencyStamp",
                value: "d498e415-f402-4645-8194-313207c8a15d");
        }
    }
}
