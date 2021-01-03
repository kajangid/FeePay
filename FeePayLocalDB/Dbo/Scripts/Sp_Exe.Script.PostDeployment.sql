/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*
INSERT [dbo].[SuperAdmin_User] ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDate], [LockoutEnabled], [AccessFailedCount], [SecurityStamp], [FirstName], [LastName], [Password], [FullName], [Photo], [City], [LastLoginIP], [LastLoginDate], [IsActive], [IsDelete], [ModifyDate], [ModifyBy], [AddedDate], [AddedBy])
VALUES (N'admin@test.com', N'ADMIN@TEST.COM', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEJACOSUOzh76NNxgarCpNJwSJeoDjH/OY7HQvOCsBOM5H5zaQVzzUgGMfvUYlu+sQw==', NULL, 0, 0, NULL, 0, 0, NULL, NULL, NULL, N'karan123', NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(N'2021-01-03T16:56:06.5433333+00:00' AS DateTimeOffset), 0, CAST(N'2021-01-03T16:56:06.5433333+00:00' AS DateTimeOffset), 0)


EXEC	[dbo].[SP_Add_SuperAdmin_User]
		@AccessFailedCount = 0,
		@AddedBy = 0,
		@City = NULL,
		@Email = N'testabc123@gmail.com',
		@EmailConfirmed = 0,
		@FirstName = NULL,
		@FullName = NULL,
		@IsActive = 1,
		@IsDelete = 0,
		@LastLoginDate = NULL,
		@LastLoginIP = NULL,
		@LastName = NULL,
		@LockoutEnabled = 0,
		@LockoutEnd = NULL,
		@ModifyBy = 0,
		@NormalizedEmail = N'TESTABC123@GMAIL.COM',
		@NormalizedUserName = N'TESTABC123@GMAIL.COM',
		@Password = N'karan123',
		@PasswordHash = N'AQAAAAEAACcQAAAAEALxqWjmHa+HM417/pxDu9IheGJXILG/aS5w9acxCkOrU3tYQRClW9CcSm3rOUUEPg==',
		@PhoneNumber = NULL,
		@PhoneNumberConfirmed = 0,
		@Photo = NULL,
		@SecurityStamp = NULL,
		@TwoFactorEnabled = 0,
		@UserName = N'testabc123@gmail.com'



*/