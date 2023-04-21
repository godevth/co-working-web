using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class AlterUserSearchView20201103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserSearchView] as
            SELECT        U.[Id], U.[UserName], U.[NormalizedUserName], U.[Email], U.[NormalizedEmail], U.[EmailConfirmed], U.[PhoneNumber], U.[PhoneNumberConfirmed], U.[TwoFactorEnabled], U.[LockoutEnd], U.[LockoutEnabled], 
                         U.[AccessFailedCount], U.[CreateUserDateTime], U.[FirstName], U.[LastName], U.[DisplayName], U.[LastLogOnDateTime], U.[OpenID], U.[Position], SUBSTRING
                             ((SELECT        ',' + CASE WHEN (AspNetRoles.Name IS NULL OR
                                                          AspNetRoles.Name = N'') THEN AspNetRoles.Name ELSE AspNetRoles.Description + ' [' + AspNetRoles.Name + ']' END AS [text()]
                                 FROM            AspNetUserRoles UR1 INNER JOIN
                                                          AspNetRoles ON UR1.RoleId = AspNetRoles.Id
                                 WHERE        UR1.UserId = U.Id
                                 ORDER BY UR1.UserId FOR XML PATH('')), 2, 1000) Roles, U.[InActiveStatus], U.[IsDeleted]
            FROM            [dbo].[AspNetUsers] U");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserSearchView] as
            SELECT        U.[Id], U.[UserName], U.[NormalizedUserName], U.[Email], U.[NormalizedEmail], U.[EmailConfirmed], U.[PhoneNumber], U.[PhoneNumberConfirmed], U.[TwoFactorEnabled], U.[LockoutEnd], U.[LockoutEnabled], 
                         U.[AccessFailedCount], U.[CreateUserDateTime], U.[FirstName], U.[LastName], U.[DisplayName], U.[LastLogOnDateTime], U.[OpenID], U.[Position], SUBSTRING
                             ((SELECT        ',' + CASE WHEN (AspNetRoles.Name IS NULL OR
                                                          AspNetRoles.Name = N'') THEN AspNetRoles.Name ELSE AspNetRoles.Description + ' [' + AspNetRoles.Name + ']' END AS [text()]
                                 FROM            AspNetUserRoles UR1 INNER JOIN
                                                          AspNetRoles ON UR1.RoleId = AspNetRoles.Id
                                 WHERE        UR1.UserId = U.Id
                                 ORDER BY UR1.UserId FOR XML PATH('')), 2, 1000) Roles
FROM            [dbo].[AspNetUsers] U");
        }
    }
}
