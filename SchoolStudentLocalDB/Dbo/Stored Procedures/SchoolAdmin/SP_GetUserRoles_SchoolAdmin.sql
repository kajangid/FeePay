CREATE PROCEDURE [dbo].[SP_GetUserRoles_SchoolAdmin]
	@UserId int 
AS
BEGIN
	SELECT [r].[Id], [r].[Name], [r].[NormalizedName] FROM [dbo].[SchoolAdmin_Role] r 
	INNER JOIN [dbo].[SchoolAdmin_UserRole] ur ON 
	ur.[RoleId] = r.Id
	WHERE 
	ur.UserId = @UserId
END
RETURN 0
