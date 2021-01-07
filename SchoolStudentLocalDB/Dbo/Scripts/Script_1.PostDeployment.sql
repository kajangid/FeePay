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



USE [School_RSSSJ#01012021]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[SP_Add_StudentLogin]
		@AccessFailedCount = 0,
		@Email = N'student@test.com',
		@EmailConfirmed = 0,
		@FirstName = N'student',
		@IsActive = 1,
		@LockoutEnabled = 0,
		@NormalizedEmail = N'STUDENT@TEST.COM',
		@NormalizedUserName = N'STUDENT@TEST.COM',
		@Password = N'karan123',
		@PasswordHash = N'AQAAAAEAACcQAAAAEJACOSUOzh76NNxgarCpNJwSJeoDjH/OY7HQvOCsBOM5H5zaQVzzUgGMfvUYlu+sQw==',
		@PhoneNumberConfirmed = 0,
		@TwoFactorEnabled = 0,
		@UserName = N'student@test.com'

SELECT	@return_value as 'Return Value'

GO




*/
