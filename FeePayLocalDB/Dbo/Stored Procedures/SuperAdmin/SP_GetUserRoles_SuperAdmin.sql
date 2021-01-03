CREATE PROCEDURE [dbo].[SP_GetUserRoles_SuperAdmin]
	@UserId int 
AS
BEGIN
	SELECT [r].[Id], [r].[Name], [r].[NormalizedName] FROM [dbo].[SuperAdmin_Role] r 
	INNER JOIN [dbo].[SuperAdmin_UserRole] ur ON 
	ur.[RoleId] = r.Id
    WHERE 
	ur.UserId = @UserId
END
RETURN 0
