CREATE PROCEDURE [dbo].[SP_Get_SchoolAdmin_UserRoles]
	@UserId INT, @IsActive BIT = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		[r].[Id], [r].[Name], [r].[NormalizedName]
	FROM [dbo].[SchoolAdmin_Role] [r] 
		INNER JOIN [dbo].[SchoolAdmin_UserRole] [ur] 
	ON [ur].[RoleId] = [r].[Id]
	WHERE 
		[ur].UserId = @UserId AND 
		[ur].IsActive = @IsActive AND 
		[ur].[IsDelete] = 0
END
RETURN 0
