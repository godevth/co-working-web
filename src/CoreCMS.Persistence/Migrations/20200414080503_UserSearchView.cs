using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class UserSearchView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                maxLength: 450,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "7fd2efe6-e036-4004-9a9b-0fdd9a089d03", "3cc900e0-fe1b-4f89-98b2-734dd3d21bf3", "ผู้ดูแลระบบ", "super_admin", "SUPER_ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreateUserDateTime", "DisplayName", "Email", "EmailConfirmed", "EmployeeCode", "FamilyName", "FirstName", "FullName", "GivenName", "LastLogOnDateTime", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OpenID", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1312f09c-4010-429d-89cf-c6742832eec9", 0, "fb298815-a3b9-48c6-ab78-de4d484a9bee", null, null, "inno@sbpds.co.th", true, null, null, null, null, null, null, null, false, null, "INNO@SBPDS.CO.TH", "ADMINISTRATOR", false, "AQAAAAEAACcQAAAAENYoUlZKjD06Egogo8FJQyQiJjB91UReMkF3kI2BzteZSw2ChdAnPlxKiMa3lMAu3g==", null, false, null, "", false, "administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1312f09c-4010-429d-89cf-c6742832eec9", "7fd2efe6-e036-4004-9a9b-0fdd9a089d03" });

            migrationBuilder.Sql(@"EXEC sp_executesql N'CREATE VIEW [dbo].[UserSearchView]
                AS
                SELECT	U.[Id]
		                ,U.[UserName]
		                ,U.[NormalizedUserName]
		                ,U.[Email]
		                ,U.[NormalizedEmail]
		                ,U.[EmailConfirmed]
		                ,U.[PhoneNumber]
		                ,U.[PhoneNumberConfirmed]
		                ,U.[TwoFactorEnabled]
		                ,U.[LockoutEnd]
		                ,U.[LockoutEnabled]
		                ,U.[AccessFailedCount]
		                ,U.[CreateUserDateTime]
		                ,U.[FirstName]
		                ,U.[LastName]
		                ,U.[DisplayName]
		                ,U.[LastLogOnDateTime]
                        ,U.[OpenID]
		                ,U.[Position]
		                ,SUBSTRING(
			                (SELECT		'','' + CASE WHEN (AspNetRoles.Name IS NULL OR AspNetRoles.Name = N'''')
							                THEN AspNetRoles.Name ELSE AspNetRoles.Description + '' ['' + AspNetRoles.Name + '']'' END AS [text()]
			                FROM		AspNetUserRoles UR1 INNER JOIN
						                AspNetRoles ON UR1.RoleId = AspNetRoles.Id
			                WHERE		UR1.UserId = U.Id
			                ORDER BY	UR1.UserId
			                FOR			XML PATH ('''')), 2, 1000) Roles
                FROM	[dbo].[AspNetUsers] U'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_executesql N'DROP VIEW [dbo].[UserSearchView]'");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1312f09c-4010-429d-89cf-c6742832eec9", "7fd2efe6-e036-4004-9a9b-0fdd9a089d03" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fd2efe6-e036-4004-9a9b-0fdd9a089d03");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1312f09c-4010-429d-89cf-c6742832eec9");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");
        }
    }
}
