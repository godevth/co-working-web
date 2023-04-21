using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreCMS.Persistence.Migrations
{
    public partial class CreateGenerateMemberCardsSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GenerateMemberCards
	@MemberClassCode NVARCHAR(50),
	@ExpireDate DateTime,
	@GenerateRefCode NVARCHAR(50),
	@BatchCode NVARCHAR(50),
	@Quantity INT,
	@CreateUserId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION GEN_MC_TR

	IF @Quantity < 0 
	BEGIN
		SET @Quantity = 0 
	END

	DECLARE @i INT = 0;

	WHILE @i < @Quantity
	BEGIN
		INSERT INTO MemberCards (MemberClassCode, RunningNumber, SecureCode, [ExpireDate], GenerateRefCode, BatchCode, CardNumber, IsDeleted, InActiveStatus, IsRegistered, CreatedUserId, CreatedDate)
			SELECT @MemberClassCode, ISNULL(MAX(mc.RunningNumber) + 1, 1), FLOOR(RAND(CHECKSUM(NEWID()))*(9999-1000+1)+1000), @ExpireDate, @GenerateRefCode, @BatchCode, '', 0, 0, 0, @CreateUserId, GETDATE()
			FROM MemberCards AS mc

		DECLARE @id INT;
		SELECT @id = @@IDENTITY

		DECLARE @runningNumber INT;
		SELECT @runningNumber = RunningNumber FROM MemberCards WHERE Id = @id

		DECLARE @cardNumber NVARCHAR(50)
		SET @cardNumber = N'ST';

		IF @MemberClassCode = 'MEMBER_CLASS_GOLD'
		BEGIN
			SET @cardNumber = @cardNumber + N'G'
		END
		ELSE
		BEGIN
			SET @cardNumber = @cardNumber + N'S'
		END

		SET @cardNumber = @cardNumber + FORMAT(@ExpireDate, 'yy')
		SET @cardNumber = @cardNumber + FORMAT(@runningNumber,'00000#')

		UPDATE MemberCards SET CardNumber = @cardNumber WHERE Id = @id

		SET @i = @i + 1;
	END;

	COMMIT TRANSACTION GEN_MC_TR;
END

GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
