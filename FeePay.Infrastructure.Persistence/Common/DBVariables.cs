namespace FeePay.Infrastructure.Persistence.Common
{
	using FeePay.Core.Application.Interface.Common;
	internal class DBVariables : IDBVariables
	{
		// TODO : For Quality Change 'RAW QUERY' to 'STORED PROCEDURES'

		#region SUPER ADMIN IDENTITY
		public string SP_Add_SuperAdmin_User { get; } = "SP_Add_SuperAdmin_User";
		public string SP_Delete_SuperAdmin_User { get; } = "SP_Delete_SuperAdmin_User";
		public string SP_Get_SuperAdmin_User { get; } = "SP_Get_SuperAdmin_User";
		public string SP_GetAll_SuperAdmin_User { get; } = "SP_GetAll_SuperAdmin_User";
		public string SP_Update_SuperAdmin_User { get; } = "SP_Update_SuperAdmin_User";
		public string SP_Add_SuperAdmin_Role { get; } = "SP_Add_SuperAdmin_Role";
		public string SP_Delete_SuperAdmin_Role { get; } = "SP_Delete_SuperAdmin_Role";
		public string SP_Get_SuperAdmin_Role { get; } = "SP_Get_SuperAdmin_Role";
		public string SP_GetAll_SuperAdmin_Role { get; } = "SP_GetAll_SuperAdmin_Role";
		public string SP_Update_SuperAdmin_Role { get; } = "SP_Update_SuperAdmin_Role";
		public string SP_Add_SuperAdmin_UserRole { get; } = "SP_Add_SuperAdmin_UserRole";
		public string SP_Delete_SuperAdmin_UserRole { get; } = "SP_Delete_SuperAdmin_UserRole";
		public string SP_Get_SuperAdmin_UserRole { get; } = "SP_Get_SuperAdmin_UserRole";
		public string SP_Update_SuperAdmin_UserRole { get; } = "SP_Update_SuperAdmin_UserRole";
		public string SP_GetUserRoles_SuperAdmin { get; } = "SP_GetUserRoles_SuperAdmin";
		public string SP_Get_SuperAdmin_UsersInRole { get; } = "SP_Get_SuperAdmin_UsersInRole";
		public string SP_GetAll_SuperAdmin_UserRole { get; } = "SP_GetAll_SuperAdmin_UserRole";
		public string SP_AddLoginInfo_SuperAdmin { get; } = "SP_AddLoginInfo_SuperAdmin";
		public string QUERY_Password_SuperAdmin { get; } = @"SELECT 
		[Id],
		[UserName],
		[Password]
		FROM [dbo].[SuperAdmin_User]
		WHERE 
		[IsDelete] = 0 AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END";
		#endregion


		#region SCHOOL ADMIN IDENTITY
		public string SP_Add_SchoolAdmin_User { get; } = "SP_Add_SchoolAdmin_User";
		public string SP_Delete_SchoolAdmin_User { get; } = "SP_Delete_SchoolAdmin_User";
		public string SP_Get_SchoolAdmin_User { get; } = "SP_Get_SchoolAdmin_User";
		public string SP_GetAll_SchoolAdmin_User { get; } = "SP_GetAll_SchoolAdmin_User";
		public string SP_Get_SchoolAdmin_User_With_AddEditUser { get; } = "SP_Get_SchoolAdmin_User_AddEditUser";
		public string SP_GetAll_SchoolAdmin_User_With_AddEditUser { get; } = "SP_GetAll_SchoolAdmin_User_AddEditUser";
		public string SP_Update_SchoolAdmin_User { get; } = "SP_Update_SchoolAdmin_User";
		public string SP_Add_SchoolAdmin_Role { get; } = "SP_Add_SchoolAdmin_Role";
		public string SP_Delete_SchoolAdmin_Role { get; } = "SP_Delete_SchoolAdmin_Role";
		public string SP_Get_SchoolAdmin_Role { get; } = "SP_Get_SchoolAdmin_Role";
		public string SP_GetAll_SchoolAdmin_Role { get; } = "SP_GetAll_SchoolAdmin_Role";
		public string SP_Get_SchoolAdmin_Role_With_AddEditUser { get; } = "SP_Get_SchoolAdmin_Role_AddEditUser";
		public string SP_GetAll_SchoolAdmin_Role_With_AddEditUser { get; } = "SP_GetAll_SchoolAdmin_Role_AddEditUser";
		public string SP_Update_SchoolAdmin_Role { get; } = "SP_Update_SchoolAdmin_Role";
		public string SP_Add_SchoolAdmin_UserRole { get; } = "SP_Add_SchoolAdmin_UserRole";
		public string SP_Delete_SchoolAdmin_UserRole { get; } = "SP_Delete_SchoolAdmin_UserRole";
		public string SP_Get_SchoolAdmin_UserRole { get; } = "SP_Get_SchoolAdmin_UserRole";
		public string SP_Update_SchoolAdmin_UserRole { get; } = "SP_Update_SchoolAdmin_UserRole";
		public string SP_Get_SchoolAdmin_UserRoles { get; } = "SP_Get_SchoolAdmin_UserRoles";
		public string SP_Get_SchoolAdmin_UsersInRole { get; } = "SP_Get_SchoolAdmin_UsersInRole";
		public string SP_GetAll_SchoolAdmin_UserRole { get; } = "SP_GetAll_SchoolAdmin_UserRole";
		public string SP_Add_SchoolAdmin_User_LoginInfo { get; } = "SP_Add_SchoolAdmin_User_LoginInfo";
		public string QUERY_FindUserPassword_SchoolAdminUser { get; } = @"SELECT 
		[Password],
		[UserName] 
		FROM [dbo].[SchoolAdmin_User] 
		WHERE [Id] = @UserId AND 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE  [IsActive] END";
		public string QUERY_GetAllUserPassword_SchoolAdminUser { get; } = @"SELECT 
		[Password],
		[UserName] 
		FROM [dbo].[SchoolAdmin_User]
		WHERE 
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE  [IsActive] END";
		#endregion


		#region STUDENT LOGIN IDENTITY
		public string SP_Add_StudentLogin { get; } = "SP_Add_StudentLogin";
		public string SP_Delete_StudentLogin { get; } = "SP_Delete_StudentLogin";
		public string SP_Get_StudentLogin { get; } = "SP_Get_StudentLogin";
		public string SP_GetAll_StudentLogin { get; } = "SP_GetAll_StudentLogin";
		public string SP_Update_StudentLogin { get; } = "SP_Update_StudentLogin";
		public string SP_Add_StudentLogin_LoginInfo { get; } = "SP_Add_StudentLogin_LoginInfo";
		public string QUERY_FindUserPassword_StudentLogin { get; } = @"SELECT 
		[sa].[Id],
		[sl].[Password],
		[sl].[UserName] 
		FROM [dbo].[StudentLogin] [sl]
		INNER JOIN [dbo].[StudentAdmission] [sa] ON 
		[sa].[StudentLoginId] = [sl].[Id] AND [sa].[IsDelete] = 0
		WHERE 
		[sa].[Id] = @UserId AND
		[sl].[IsDelete] = 0 AND 
		[sl].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sl].[IsActive] END";
		public string QUERY_GetAllUserPassword_StudentLogin { get; } = @"SELECT 
		[sa].[Id],
		[sl].[Password],
		[sl].[UserName] 
		FROM [dbo].[StudentLogin] [sl]
		INNER JOIN [dbo].[StudentAdmission] [sa] ON 
		[sa].[StudentLoginId] = [sl].[Id] AND [sa].[IsDelete] = 0
		WHERE 
		[sl].[IsDelete] = 0 AND
		[sl].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sl].[IsActive] END";
		#endregion


		#region REGISTER SCHOOL 
		public string SP_Add_RegisteredSchool { get; } = "SP_Add_RegisteredSchool";
		public string SP_Update_RegisteredSchool { get; } = "SP_Update_RegisteredSchool";
		public string SP_Delete_RegisteredSchool { get; } = "SP_Delete_RegisteredSchool";
		public string SP_Get_RegisteredSchool { get; } = "SP_Get_RegisteredSchool";
		public string SP_GetAll_RegisteredSchool { get; } = "SP_GetAll_RegisteredSchool";
		#endregion


		#region FEE TYPE 
		public string SP_Add_FeeType { get; } = "SP_Add_FeeType";
		public string SP_Update_FeeType { get; } = "SP_Update_FeeType";
		public string SP_Delete_FeeType { get; } = "SP_Delete_FeeType";
		public string SP_Get_FeeType { get; } = "SP_Get_FeeType";
		public string SP_GetAll_FeeType { get; } = "SP_GetAll_FeeType";
		public string SP_Get_FeeType_AddEditUser { get; } = "SP_Get_FeeType_AddEditUser";
		public string SP_GetAll_FeeType_AddEditUser { get; } = "SP_GetAll_FeeType_AddEditUser";
		#endregion


		#region FEE GROUP
		public string SP_Add_FeeGroup { get; } = "SP_Add_FeeGroup";
		public string SP_Update_FeeGroup { get; } = "SP_Update_FeeGroup";
		public string SP_Delete_FeeGroup { get; } = "SP_Delete_FeeGroup";
		public string SP_Get_FeeGroup { get; } = "SP_Get_FeeGroup";
		public string SP_GetAll_FeeGroup { get; } = "SP_GetAll_FeeGroup";
		public string SP_Get_FeeGroup_AddEditUser { get; } = "SP_Get_FeeGroup_AddEditUser";
		public string SP_GetAll_FeeGroup_AddEditUser { get; } = "SP_GetAll_FeeGroup_AddEditUser";
		public string SP_GetAll_FeeGroup_MasterAndType { get; } = "SP_GetAll_FeeGroup_MasterAndType";
		#endregion


		#region FEE MASTER
		public string SP_Add_FeeMaster { get; } = "SP_Add_FeeMaster";
		public string SP_Update_FeeMaster { get; } = "SP_Update_FeeMaster";
		public string SP_Delete_FeeMaster { get; } = "SP_Delete_FeeMaster";
		public string SP_Get_FeeMaster { get; } = "SP_Get_FeeMaster";
		public string SP_GetAll_FeeMaster { get; } = "SP_GetAll_FeeMaster";
		public string SP_Get_FeeMaster_AddEditUser { get; } = "SP_Get_FeeMaster_AddEditUser";
		public string SP_GetAll_FeeMaster_AddEditUser { get; } = "SP_GetAll_FeeMaster_AddEditUser";
		#endregion


		#region CLASS
		public string SP_Add_Class { get; } = "SP_Add_Class";
		public string SP_Update_Class { get; } = "SP_Update_Class";
		public string SP_Delete_Class { get; } = "SP_Delete_Class";
		public string SP_Get_Class { get; } = "SP_Get_Class";
		public string SP_Get_Class_AddEditUser { get; } = "SP_Get_Class_AddEditUser";
		#endregion


		#region SECTION
		public string SP_Add_Section { get; } = "SP_Add_Section";
		public string SP_Update_Section { get; } = "SP_Update_Section";
		public string SP_Delete_Section { get; } = "SP_Delete_Section";
		public string SP_Get_Section { get; } = "SP_Get_Section";
		public string SP_Get_Section_AddEditUser { get; } = "SP_Get_Section_AddEditUser";
		#endregion


		#region CLASS_SECTION
		public string SP_Add_ClassSection { get; } = "SP_Add_ClassSection";
		public string SP_Remove_ClassSection { get; } = "SP_Remove_ClassSection";
		public string SP_Get_ClassSection { get; } = "SP_Get_ClassSection";
		public string SP_GetAll_ClassesSections_AddEditUser { get; } = "SP_GetAll_ClassesSections_AddEditUser";
		#endregion


		#region SESSION
		public string SP_Add_Session { get; } = "SP_Add_Session";
		public string SP_Update_Session { get; } = "SP_Update_Session";
		public string SP_Delete_Session { get; } = "SP_Delete_Session";
		public string SP_Get_Session { get; } = "SP_Get_Session";
		public string SP_SetDefault_Session { get; } = "SP_SetDefault_Session";
		public string SP_Get_Session_AddEditUser { get; } = "SP_Get_Session_AddEditUser";
		#endregion


		#region STUDENT_ADMISSION
		public string SP_Add_StudentAdmission { get; } = "SP_Add_StudentAdmission";
		public string SP_Update_StudentAdmission { get; } = "SP_Update_StudentAdmission";
		public string SP_Delete_StudentAdmission { get; } = "SP_Delete_StudentAdmission";
		public string SP_Get_StudentAdmission { get; } = "SP_Get_StudentAdmission";
		public string SP_Search_Students { get; } = "SP_Search_Students";
		public string SP_Get_StudentAdmission_AddEditUser { get; } = "SP_Get_StudentAdmission_AddEditUser";
		public string QUERY_BulkUpdate_StudentAdmission { get; } = @"
				CREATE TABLE  #routineUpdatedRecords
				([Id] INT,[AcademicSessionId] INT,[ClassId] INT,[SectionId] INT,
				[IsActive] BIT,[ModifyBy] INT,[EnrollNo] NVARCHAR(49));
				INSERT INTO #routineUpdatedRecords 
				VALUES(@Id, @AcademicSessionId, @ClassId, @SectionId, @IsActive, @ModifyBy, @EnrollNo)
		
				UPDATE [sa] SET 
				[sa].[AcademicSessionId]	=	CASE WHEN [ur].[AcademicSessionId] IS NOT NULL THEN [ur].[AcademicSessionId] ELSE [ur].[AcademicSessionId] END, 
				[sa].[ClassId]		=	CASE WHEN [ur].[ClassId] IS NOT NULL THEN [ur].[ClassId] ELSE [sa].[ClassId] END, 
				[sa].[SectionId]	=	CASE WHEN [ur].[SectionId] IS NOT NULL THEN [ur].[SectionId] ELSE [sa].[SectionId] END, 
				[sa].[IsActive]		=	CASE WHEN [ur].[IsActive] IS NOT NULL THEN [ur].[IsActive] ELSE [sa].[IsActive] END, 
				[sa].[ModifyBy]		=	CASE WHEN [ur].[ModifyBy] IS NOT NULL THEN [ur].[ModifyBy] ELSE [sa].[ModifyBy] END, 
				[sa].[EnrollNo]		=	CASE WHEN [ur].[EnrollNo] IS NOT NULL THEN [ur].[EnrollNo] ELSE [sa].[EnrollNo] END
				FROM [StudentAdmission] [sa]
				INNER JOIN #routineUpdatedRecords [ur] 
				ON [sa].[Id] = [ur].[Id] AND [sa].[IsActive] = 1 AND [sa].[IsDelete] = 0";
		public string QUERY_StudentSearch_Select_AddEditUser { get; } = @"SELECT 
		[sa].[Id], 
		[sa].[FormNo], 
		[sa].[ClassId], 
		[sa].[SectionId], 
		[sa].[StudentLoginId], 
		[sa].[AcademicSessionId],
		[sa].[AdmissionDate],
		[sa].[FirstName], 
		[sa].[LastName], 
		[sa].[FatherName], 
		[sa].[MobileNo], 
		[sa].[Gender], 
		[sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], 
		[sa].[EnrollNo], 
		[sa].[MACHINEID], 
		[sa].[PreviousClass], 
		[sa].[PreviousInstituteName], 
		[sa].[YearOfPassing],
		[sa].[PreviousRollNo], 
		[sa].[PreviousPercent], 
		[sa].[MotherName], 
		[sa].[DateOfBirth], 
		[sa].[Category], 
		[sa].[AlternateMobileNo],
		[sa].[StudentEmail], 
		[sa].[GuardianEmail], 
		[sa].[StudentType], 
		[sa].[Medium], 
		[sa].[Address], 
		[sa].[CityId], 
		[sa].[Village],
		[sa].[StateId], 
		[sa].[Religion], 
		[sa].[Image], 
		[sa].[Remarks], 
		[sa].[IsActive], 
		[sa].[IsDelete], 
		[sa].[ModifyDate], 
		[sa].[AddedDate]
	
		, [c].[Id], 
		[c].[Name], 
		[c].[NormalizedName]	
		, [s].[Id], 
		[s].[Name], 
		[s].[NormalizedName]
	
		, [t4].[Id], 
		[t4].[FullName], 
		[t4].[Email]
		, [t5].[Id], 
		[t5].[FullName], 
		[t5].[Email]
		FROM
			[dbo].[StudentAdmission] [sa]
		INNER JOIN [dbo].[Class] [c] ON
		[c].[Id] = [sa].[ClassId] AND [c].[IsActive] = 1 AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] ON
		[s].[Id] = [sa].[SectionId]
		OUTER APPLY (
					SELECT [ab].[Id],[ab].[FullName],[ab].[Email] 
					FROM [dbo].[SchoolAdmin_User] [ab]
					WHERE [ab].[Id] = [sa].[AddedBy] AND [ab].[IsDelete] = 0 AND [ab].[IsActive] = 1) [t4]

		OUTER APPLY (
					SELECT [mb].[Id],[mb].[FullName],[mb].[Email] 
					FROM [dbo].[SchoolAdmin_User] [mb]
					WHERE [mb].[Id] = [sa].[ModifyBy] AND [mb].[IsDelete] = 0 AND [mb].[IsActive] = 1) [t5]

		WHERE  
			[sa].[IsDelete] = 0 ";
		public string QUERY_StudentSearch_Select { get; } = @"SELECT 
		[sa].[Id], 
		[sa].[FormNo],
		[sa].[ClassId],
		[sa].[SectionId],
		[sa].[AcademicSessionId],
		[sa].[StudentLoginId],
		[sa].[AdmissionDate],
		[sa].[FirstName],
		[sa].[LastName], 
		[sa].[FatherName], 
		[sa].[MobileNo], 
		[sa].[Gender], 
		[sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], 
		[sa].[EnrollNo],
		[sa].[MACHINEID],
		[sa].[PreviousClass],
		[sa].[PreviousInstituteName],
		[sa].[YearOfPassing],
		[sa].[PreviousRollNo],
		[sa].[PreviousPercent],
		[sa].[MotherName],
		[sa].[DateOfBirth], 
		[sa].[Category], 
		[sa].[AlternateMobileNo],
		[sa].[StudentEmail],
		[sa].[GuardianEmail], 
		[sa].[StudentType], 
		[sa].[Medium],
		[sa].[Address],
		[sa].[CityId],
		[sa].[Village],
		[sa].[StateId], 
		[sa].[Religion], 
		[sa].[Image], 
		[sa].[Remarks], 
		[sa].[IsActive], 
		[sa].[IsDelete],
		[sa].[ModifyDate],
		[sa].[AddedDate]
	
		, [c].[Id], 
		[c].[Name], 
		[c].[NormalizedName]	
		, [s].[Id], 
		[s].[Name], 
		[s].[NormalizedName]
		FROM
			[dbo].[StudentAdmission] [sa]
		INNER JOIN [dbo].[Class] [c] ON
		[c].[Id] = [sa].[ClassId] AND [c].[IsActive] = 1 AND [c].[IsDelete] = 0
		INNER JOIN [dbo].[Section] [s] ON
		[s].[Id] = [sa].[SectionId]
		WHERE 
			[sa].[IsDelete] = 0 ";
		public string QUERY_StudentSearch_Where_SearchIn { get; } = @" AND (
			[sa].[FirstName] LIKE '%'+@SearchParam+'%' OR
			[sa].[LastName] LIKE '%'+@SearchParam+'%' OR
			[sa].[FatherName] LIKE '%'+@SearchParam+'%' OR
			[sa].[MobileNo] LIKE '%'+@SearchParam+'%' OR
			[sa].[Sr_RegNo] LIKE '%'+@SearchParam+'%' OR
			[c].[Name] LIKE '%'+@SearchParam+'%'
			) ";
		public string QUERY_StudentSearch_Where_IsActive { get; } = " AND [sa].[IsActive] = @IsActive ";
		public string QUERY_StudentSearch_Where_ClassId { get; } = " AND [sa].[ClassId] = @ClassId ";
		public string QUERY_StudentSearch_Where_SectionId { get; } = " AND [sa].[SectionId] = @SectionId ";
		public string QUERY_StudentSearch_Where_Gender { get; } = " AND [sa].[Gender] = @Gender ";
		public string QUERY_StudentSearch_Where_StudentId { get; } = " AND [sa].[Id] = @StudentId ";
		public string QUERY_StudentSearch_Where_AcademicSessionId { get; } = " AND [sa].[AcademicSessionId] = @AcademicSessionId ";
		#endregion


		#region STATE & CITY

		public string QUERY_GetAll_Active_Cities { get; } = "SELECT *FROM [dbo].[Cities] WHERE IsActive = @IsActive";
		public string QUERY_GetAll_Active_States { get; } = "SELECT *FROM [dbo].[States] WHERE IsActive = @IsActive";
		public string QUERY_Find_Active_Cities_ByStateId { get; } = "SELECT *FROM [dbo].[Cities] WHERE [StateId] = @StateId AND IsActive = @IsActive";
		public string QUERY_Find_Active_CityByIdAsync { get; } = "SELECT *FROM [dbo].[Cities] WHERE Id = @Id AND IsActive = @IsActive";
		public string QUERY_Find_Active_StateByIdAsync { get; } = "SELECT *FROM [dbo].[States] WHERE Id = @Id AND IsActive = @IsActive";
		#endregion


		#region STUDENT_FEES
		public string SP_Add_StudentFees { get; } = "SP_Add_StudentFees";
		public string SP_Get_StudentFees { get; } = "SP_Get_StudentFees";
		public string SP_Remove_StudentFees { get; } = "SP_Remove_StudentFees";
		public string QUERY_IsFeeAssignToStudent { get; } = @"SELECT COUNT(*) 
													FROM StudentFees 
													WHERE 
													StudentAdmissionId = @StudentAdmissionId AND 
													FeeMasterId = @FeeMasterId";
		public string QUERY_StudentFeeList { get; } = @"SELECT
		[sf].[Id],
		[sf].[StudentAdmissionId],
		[sf].[FeeMasterId], 
		[sf].[Status],		
		[sf].[FeeGroupId],
		[sf].[PaymentId],
		[sf].[Mode],
		[sf].[PaymentDate],
		[sf].[IsPaid],
		[sf].[AcademicSessionId],
		[fg].[Name] AS [FeeGroupName],
		[fm].[Amount] AS [FeeAmount],
		[fm].[DueDate],				
		[ft].[Id] AS [FeeTypeId], 
		[ft].[Name] AS [FeeTypeName], 
		[ft].[Code] AS [FeeTypeCode]
		FROM 
		[dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[FeeMaster] AS [fm] 
		ON [fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
		INNER JOIN [dbo].[FeeType] AS [ft] 
		ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
		INNER JOIN [dbo].[FeeGroup] AS [fg]  
		ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		WHERE 
		[sf].[StudentAdmissionId] = @StudentAdmissionId AND 
		[sf].[AcademicSessionId] = @AcademicSessionId";
		public string QUERY_Find_StudentFee { get; } = @"SELECT 
		[sf].[Id],
		[sf].[StudentAdmissionId],
		[sf].[FeeMasterId],
		[sf].[Status],
		[sf].[FeeGroupId],
		[sf].[PaymentId],
		[sf].[Mode],
		[sf].[PaymentDate],
		[sf].[IsPaid],
		[sf].[AcademicSessionId],
		[fg].[Name] AS [FeeGroupName],
		[fm].[Amount] AS [FeeAmount],		
		[fm].[DueDate],
		[ft].[Id] AS [FeeTypeId],
		[ft].[Name] AS [FeeTypeName],
		[ft].[Code] AS [FeeTypeCode]
		FROM 
		[dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[FeeMaster] AS [fm] 
		ON [fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
		INNER JOIN [dbo].[FeeType] AS [ft] 
		ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
		INNER JOIN [dbo].[FeeGroup] AS [fg]  
		ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		WHERE [sf].[Id] = @Id AND 
		[sf].[IsActive] = 1 AND 
		[sf].[IsDelete] = 0";
		public string QUERY_StudentFeeList_ByTranscationId { get; } = @"SELECT
		[sf].[Id],
		[sf].[StudentAdmissionId],
		[sf].[FeeMasterId], 
		[sf].[Status],		
		[sf].[FeeGroupId],
		[sf].[PaymentId],
		[sf].[Mode],
		[sf].[PaymentDate],
		[sf].[IsPaid],
		[sf].[AcademicSessionId],
		[fg].[Name] AS [FeeGroupName],
		[fm].[Amount] AS [FeeAmount],
		[fm].[DueDate],				
		[ft].[Id] AS [FeeTypeId], 
		[ft].[Name] AS [FeeTypeName], 
		[ft].[Code] AS [FeeTypeCode]
		FROM 
		[dbo].[StudentFees] [sf] 
		INNER JOIN [dbo].[FeeMaster] AS [fm] 
		ON [fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
		INNER JOIN [dbo].[FeeType] AS [ft] 
		ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
		INNER JOIN [dbo].[FeeGroup] AS [fg]  
		ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
		WHERE 
		[sf].[PaymentId] = @PaymentId AND 
		[sf].[AcademicSessionId] = @AcademicSessionId";
		public string QUERY_GetClasses_Fees { get; } = @"SELECT 
		[s].[ClassId],
		[fm].[Amount] ,
		[fm].[DueDate] ,
		[sf].[IsPaid] 
		FROM  [dbo].[StudentAdmission] [s] 
		INNER JOIN [dbo].[StudentFees] [sf] ON
		[sf].[StudentAdmissionId] = [s].[Id] AND [sf].[IsActive] = 1
		INNER JOIN [dbo].[FeeMaster] [fm] ON
		[fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsActive] = 1
		WHERE 
		[s].[IsActive] = 1 AND 
		[sf].[AcademicSessionId] = @AcademicSessionId
		ORDER BY ClassId ";
		public string QUERY_GetClassStudents_Fees { get; } = @"SELECT 
		[s].[Id] AS [StudentAdmissionId],
		[s].[FirstName] AS [Name],
		[fm].[Amount] ,
		[fm].[DueDate] ,
		[sf].[IsPaid] 
		FROM  [dbo].[StudentAdmission] [s] 
		INNER JOIN [dbo].[StudentFees] [sf] ON
		[sf].[StudentAdmissionId] = [s].[Id] AND [sf].[IsActive] = 1
		INNER JOIN [dbo].[FeeMaster] [fm] ON
		[fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsActive] = 1
		INNER JOIN [dbo].[Class] [c] ON
		[c].[Id] = [s].[ClassId] AND [c].[IsActive] = 1
		INNER JOIN [dbo].[Section] [se] ON
		[se].[Id] = [s].[SectionId] AND [se].[IsActive] = 1
		WHERE 
		[s].[IsActive] = 1 AND 
		[s].[ClassId] = @ClassId AND 
		[sf].[AcademicSessionId] = @AcademicSessionId
		ORDER BY [StudentAdmissionId] ";
		public string QUERY_STUDENTFEES_GetAll { get; } = @"SELECT
		[sf].[Id],
		[sf].[StudentAdmissionId],
		[sf].[FeeMasterId],
		[sf].[FeeGroupId],
		[sf].[Status],
		[sf].[PaymentId],
		[sf].[Mode],
		[sf].[PaymentDate],
		[sf].[IsPaid],
		[sf].[IsActive],
		[sf].[IsDelete],

		[fm].[Amount] AS [FeeAmount],
		[fm].[DueDate],	
									
		[sa].[Id],
		[sa].[FormNo],
		[sa].[AdmissionDate],
		[sa].[FirstName],
		[sa].[LastName],
		[sa].[FatherName],
		[sa].[MobileNo],
		[sa].[Sr_RegNo],
		[sa].[MotherName],
		[sa].[ClassId],
		[sa].[SectionId]
		FROM [dbo].[StudentFees] [sf]
		INNER JOIN [dbo].[StudentAdmission] [sa] ON
		[sa].[Id] = [sf].[StudentAdmissionId] AND [sf].[IsDelete] = 0 AND [sf].[IsActive] = 1
		INNER JOIN [dbo].[FeeMaster] AS [fm]  ON
		[fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
		WHERE 
		[sf].[IsActive] = 1 ";
		public string QUERY_STUDENTFEES_GetAll_Where_FromDate { get; } = @" AND DATEADD(dd, DATEDIFF(dd, 0, [sf].[PaymentDate]), 0) >= DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0) ";
		public string QUERY_STUDENTFEES_GetAll_Where_ToDate { get; } = @" AND DATEADD(dd, DATEDIFF(dd, 0, [sf].[PaymentDate]), 0) <= DATEADD(dd, DATEDIFF(dd, 0, @ToDate), 0) ";
		public string QUERY_STUDENTFEES_GetAll_Where_ClassId { get; } = @" AND [sa].[ClassId] = @ClassId ";
		public string QUERY_STUDENTFEES_GetAll_Where_SectionId { get; } = @" AND [sa].[SectionId] = @SectionId ";
		public string QUERY_STUDENTFEES_GetAll_Where_StudentAdmissionId { get; } = @" AND [sf].[StudentAdmissionId] = @StudentAdmissionId ";
		public string QUERY_STUDENTFEES_GetAll_Where_IsPaid { get; } = @" AND [sf].[IsPaid] = @IsPaid ";
		public string QUERY_STUDENTFEES_GetAll_Where_AcademicSessionId { get; } = @"  AND [sf].[AcademicSessionId] = @AcademicSessionId";
		public string QUERY_STUDENTFEES_GetAll_Where_SearchParam { get; } = @"  AND (
		[sa].[FirstName] LIKE '%' + @SearchParam + '%' OR
		[sa].[LastName] LIKE '%' + @SearchParam + '%' OR
		[sa].[FatherName] LIKE '%' + @SearchParam + '%' OR
		[sa].[MobileNo] LIKE '%' + @SearchParam + '%' OR
		[sa].[Sr_RegNo] LIKE '%' + @SearchParam + '%'
		)  ";

		#endregion


		#region SEARCH DYNAMIC QUERIES

		#endregion


		#region STUDENT ACADEMIC SESSIONS
		public string SP_Add_Student_Academic_Sessions { get; } = "SP_Add_Student_Academic_Sessions";
		public string SP_Update_Student_Academic_Sessions { get; } = "SP_Update_Student_Academic_Sessions";
		public string SP_Delete_Student_Academic_Sessions { get; } = "SP_Delete_Student_Academic_Sessions";
		public string SP_Get_Student_Academic_Sessions { get; } = "SP_Get_Student_Academic_Sessions";
		public string QUERY_BulkUpdate_Student_Academic_Sessions { get; } = @"
				CREATE TABLE  #routineUpdatedRecords
				([Id] INT,[StudentAdmissionId] INT,[SessionId] INT,[ClassId] INT,[SectionId] INT, [IsActive] BIT,[ModifyBy] INT);
				INSERT INTO #routineUpdatedRecords 
				VALUES(@Id, @StudentAdmissionId, @SessionId, @ClassId, @SectionId, @IsActive, @ModifyBy)
		
				UPDATE [sa] SET 
				[sa].[StudentAdmissionId]	=	CASE WHEN [ur].[StudentAdmissionId] IS NOT NULL THEN [ur].[StudentAdmissionId] ELSE [ur].[StudentAdmissionId] END, 
				[sa].[ClassId]		=	CASE WHEN [ur].[ClassId] IS NOT NULL THEN [ur].[ClassId] ELSE [sa].[ClassId] END, 
				[sa].[SectionId]	=	CASE WHEN [ur].[SectionId] IS NOT NULL THEN [ur].[SectionId] ELSE [sa].[SectionId] END, 
				[sa].[IsActive]		=	CASE WHEN [ur].[IsActive] IS NOT NULL THEN [ur].[IsActive] ELSE [sa].[IsActive] END, 
				[sa].[ModifyBy]		=	CASE WHEN [ur].[ModifyBy] IS NOT NULL THEN [ur].[ModifyBy] ELSE [sa].[ModifyBy] END, 
				[sa].[SessionId]	=	CASE WHEN [ur].[SessionId] IS NOT NULL THEN [ur].[SessionId] ELSE [sa].[SessionId] END
				FROM [Student_Academic_Sessions] [sa]
				INNER JOIN #routineUpdatedRecords [ur] 
				ON [sa].[Id] = [ur].[Id] AND [sa].[IsDelete] = 0";
		#endregion


		#region FEESTRANSACTION
		public string QUERY_Add_FeesTransaction { get; } = @"INSERT INTO 
		[dbo].[FeesTranscation]([UserId],[TransactionId],[TransactionMode],[Amount],[IsComplete],[State],[Date])
		VALUES(@UserId, @TransactionId, @TransactionMode, @Amount, @IsComplete, @State, @Date)
		SELECT CAST(SCOPE_IDENTITY() AS INT)";
		public string QUERY_Update_FeesTransaction { get; } = @"UPDATE [dbo].[FeesTranscation]  SET
		[TransactionId]		=	CASE WHEN @TransactionId IS NOT NULL THEN @TransactionId ELSE [TransactionId] END,
		[TransactionMode]	=	CASE WHEN @TransactionMode IS NOT NULL THEN @TransactionMode ELSE [TransactionMode] END,
		[Amount]			=	CASE WHEN @Amount IS NOT NULL THEN @Amount ELSE [Amount] END,
		[IsComplete]		=	CASE WHEN @IsComplete IS NOT NULL THEN @IsComplete ELSE [IsComplete] END,
		[State]				=	CASE WHEN @State IS NOT NULL THEN @State ELSE [State] END,
		[Date]				=	CASE WHEN @Date IS NOT NULL THEN @Date ELSE [Date] END
		Where [Id] = @Id
		SELECT CAST(SCOPE_IDENTITY() AS INT)";
		public string QUERY_FeesTransaction_BulkUpdate_StudentFees { get; } =
		@"CREATE TABLE  #routineUpdatedRecords
			([Id] INT,[Status] NVARCHAR(20),[PaymentId] NVARCHAR(50),[Mode] NVARCHAR(50),
			[PaymentDate] DATETIME,[IsPaid] BIT);
			INSERT INTO #routineUpdatedRecords 
			VALUES(@Id, @Status, @PaymentId, @Mode, @PaymentDate, @IsPaid)
		
		UPDATE [sf] SET 
		[sf].[Status]		=	CASE WHEN [ur].[Status] IS NOT NULL THEN [ur].[Status] ELSE [ur].[Status] END, 
		[sf].[PaymentId]	=	CASE WHEN [ur].[PaymentId] IS NOT NULL THEN [ur].[PaymentId] ELSE [sf].[PaymentId] END, 
		[sf].[Mode]			=	CASE WHEN [ur].[Mode] IS NOT NULL THEN [ur].[Mode] ELSE [sf].[Mode] END, 
		[sf].[PaymentDate]	=	CASE WHEN [ur].[PaymentDate] IS NOT NULL THEN [ur].[PaymentDate] ELSE [sf].[PaymentDate] END, 
		[sf].[IsPaid]		=	CASE WHEN [ur].[IsPaid] IS NOT NULL THEN [ur].[IsPaid] ELSE [sf].[IsPaid] END
		FROM [StudentFees] [sf]
		INNER JOIN #routineUpdatedRecords [ur] 
		ON 
		[sf].[Id] = [ur].[Id] AND [sf].[IsActive] = 1 AND [sf].[IsDelete] = 0";
		public string QUERY_FindById_FeesTransaction { get; } = @"SELECT 
		[Id],
		[UserId],
		[TransactionId],
		[TransactionMode],
		[Amount],
		[IsComplete],
		[State],
		[Date]
		FROM [dbo].[FeesTranscation]
		WHERE [Id] = @Id";
		public string QUERY_FindByTranscationId_FeesTransaction { get; } = @"SELECT 
		[Id],
		[UserId],
		[TransactionId],
		[TransactionMode],
		[Amount],
		[IsComplete],
		[State],
		[Date]
		FROM [dbo].[FeesTranscation]
		WHERE [TransactionId] = @TransactionId";
		public string QUERY_GetAll_FeesTransaction { get; } = @"SELECT  
		[ft].[Id], 
		[ft].[UserId],
		[ft].[TransactionId], 
		[ft].[TransactionMode], 
		[ft].[Amount],
		[ft].[IsComplete], 
		[ft].[State], 
		[ft].[Date]
		FROM [dbo].[FeesTranscation] [ft]
		WHERE 
		[ft].[TransactionId] IS NOT NULL";
		public string QUERY_GetAll_FeesTransaction_WithStudentAdmission { get; } = @"SELECT 
		[ft].[Id],
		[ft].[UserId],
		[ft].[TransactionId],
		[ft].[TransactionMode],
		[ft].[Amount],
		[ft].[IsComplete],
		[ft].[State],
		[ft].[Date],
		[sa].[Id],
		[sa].[FormNo],
		[sa].[ClassId],
		[sa].[SectionId],
		[sa].[AdmissionDate],
		[sa].[FirstName],
		[sa].[LastName],
		[sa].[FatherName],
		[sa].[MobileNo],
		[sa].[Sr_RegNo],
		[sa].[MotherName]
		FROM [dbo].[FeesTranscation] [ft]
		INNER JOIN [dbo].[StudentAdmission] [sa] ON
		[sa].[StudentLoginId] = [ft].[UserId]
		WHERE 
		[ft].[TransactionId] IS NOT NULL";
		public string QUERY_GetAll_FeesTransaction_Where_FromDate { get; } = @" AND DATEADD(dd, DATEDIFF(dd, 0, [ft].[Date]), 0) >= DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0) ";
		public string QUERY_GetAll_FeesTransaction_Where_ToDate { get; } = @" AND DATEADD(dd, DATEDIFF(dd, 0, [ft].[Date]), 0) <= DATEADD(dd, DATEDIFF(dd, 0, @ToDate), 0) ";
		public string QUERY_GetAll_FeesTransaction_Where_UserId { get; } = @" AND [ft].[UserId] = @UserId ";
		public string QUERY_GetAll_FeesTransaction_Where_IsComplete { get; } = @" AND [ft].[IsComplete] = @IsComplete ";
		public string QUERY_GetAll_FeesTransaction_Where_Receipt { get; } = @" AND [ft].[Receipt] = N'%' + @Receipt + '%' ";
		public string QUERY_GetAll_FeesTransaction_Where_TransactionMode { get; } = @" AND [ft].[TransactionMode] = @TransactionMode ";
		public string QUERY_GetAll_FeesTransaction_Where_State { get; } = @" AND [ft].[State] = @State ";
		#endregion


		#region DOCUMENTS
		public string SP_Add_Document { get; } = @"SP_Add_Document";
		public string SP_Update_Document { get; } = @"SP_Update_Document";
		public string SP_Get_Document { get; } = @"SP_Get_Document";
		public string SP_Delete_Document { get; } = @"SP_Delete_Document";
		#endregion


		#region PAYMENT_GATEWAY_DOCUMENT
		public string SP_Add_PaymentGatewayDocument { get; } = @"SP_Add_PaymentGatewayDocument";
		public string SP_Update_PaymentGatewayDocument { get; } = @"SP_Update_PaymentGatewayDocument";
		public string SP_Get_PaymentGatewayDocument { get; } = @"SP_Get_PaymentGatewayDocument";
		public string SP_Delete_PaymentGatewayDocument { get; } = @"SP_Delete_PaymentGatewayDocument";
		public string QUERY_GetAll_PaymentGatewayDocument_WithSchoolData { get; } = @" SELECT 
		[pd].*,[rs].*
		FROM [dbo].[PaymentGatewayDocument] [pd]
		INNER JOIN [dbo].[RegisteredSchool] [rs] ON
		[rs].[Id] = [pd].[RegisteredSchoolId]
		WHERE 
		[rs].[IsDelete] = 0 AND
		[pd].[IsDelete] = 0 AND
		[rs].[IsActive] = 1 AND
		[pd].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [pd].[IsActive] END";
		#endregion
	}
}
