namespace FeePay.Infrastructure.Persistence.Common
{
	using FeePay.Core.Application.Interface.Common;
	internal class DBVariables : IDBVariables
	{
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
		public string QUERY_Password_SuperAdmin { get; } = @"SELECT [Id],[UserName],[Password]
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
		public string QUERY_FindUserPassword_SchoolAdminUser { get; } = @"SELECT [Password],[UserName] FROM [dbo].[SchoolAdmin_User] WHERE [Id] = @UserId AND 
								[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE  [IsActive] END";
		public string QUERY_GetAllUserPassword_SchoolAdminUser { get; } = @"SELECT [Password],[UserName] FROM [dbo].[SchoolAdmin_User] WHERE 
								[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE  [IsActive] END";
		#endregion


		#region STUDENT LOGIN IDENTITY
		public string SP_Add_StudentLogin { get; } = "SP_Add_StudentLogin";
		public string SP_Delete_StudentLogin { get; } = "SP_Delete_StudentLogin";
		public string SP_Get_StudentLogin { get; } = "SP_Get_StudentLogin";
		public string SP_GetAll_StudentLogin { get; } = "SP_GetAll_StudentLogin";
		public string SP_Update_StudentLogin { get; } = "SP_Update_StudentLogin";
		public string SP_Add_StudentLogin_LoginInfo { get; } = "SP_Add_StudentLogin_LoginInfo";
		public string QUERY_FindUserPassword_StudentLogin { get; } = @"SELECT [sa].[Id],[sl].[Password],[sl].[UserName] FROM [dbo].[StudentLogin] [sl]
						INNER JOIN [dbo].[StudentAdmission] [sa] ON [sa].[StudentLoginId] = [sl].[Id] AND [sa].[IsDelete] = 0
						WHERE [sa].[Id] = @UserId AND [sl].[IsDelete] = 0 AND 
						[sl].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sl].[IsActive] END";
		public string QUERY_GetAllUserPassword_StudentLogin { get; } = @"SELECT [sa].[Id],[sl].[Password],[sl].[UserName] FROM [dbo].[StudentLogin] [sl]
						INNER JOIN [dbo].[StudentAdmission] [sa] ON [sa].[StudentLoginId] = [sl].[Id] AND [sa].[IsDelete] = 0
						WHERE [sl].[IsDelete] = 0 AND [sl].[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [sl].[IsActive] END";
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
		public string SP_Get_Session_AddEditUser { get; } = "SP_Get_Session_AddEditUser";
		#endregion


		#region STUDENT_ADMISSION
		public string SP_Add_StudentAdmission { get; } = "SP_Add_StudentAdmission";
		public string SP_Update_StudentAdmission { get; } = "SP_Update_StudentAdmission";
		public string SP_Delete_StudentAdmission { get; } = "SP_Delete_StudentAdmission";
		public string SP_Get_StudentAdmission { get; } = "SP_Get_StudentAdmission";
		public string SP_Search_Students { get; } = "SP_Search_Students";
		public string SP_Get_StudentAdmission_AddEditUser { get; } = "SP_Get_StudentAdmission_AddEditUser";
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
		[fg].[Name] AS [FeeGroupName],
		[fm].[Amount] AS [FeeAmount],
		[fm].[DueDate] AS [FeeDueDate],				
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
		[sf].[StudentAdmissionId] = @StudentAdmissionId";
		public string QUERY_Find_StudentFee { get; } = @"SELECT 
							[sf].[Id],[sf].[StudentAdmissionId],[sf].[FeeMasterId],[sf].[Status],[sf].[FeeGroupId],[sf].[PaymentId],
							[sf].[Mode],[sf].[PaymentDate],[fg].[Name] AS [FeeGroupName],[fm].[Amount] AS [FeeAmount],
							[fm].[DueDate] AS [FeeDueDate],[ft].[Id] AS [FeeTypeId],[ft].[Name] AS [FeeTypeName],[ft].[Code] AS [FeeTypeCode]
							FROM 
							[dbo].[StudentFees] [sf] 
							INNER JOIN [dbo].[FeeMaster] AS [fm] 
							ON [fm].[Id] = [sf].[FeeMasterId] AND [fm].[IsDelete] = 0 AND [fm].[IsActive] = 1
							INNER JOIN [dbo].[FeeType] AS [ft] 
							ON [ft].[Id] = [fm].[FeeTypeId] AND [ft].[IsDelete] = 0  AND [ft].[IsActive] = 1
							INNER JOIN [dbo].[FeeGroup] AS [fg]  
							ON [fg].[Id] = [fm].[FeeGroupId] AND [fg].[IsDelete] = 0 AND [fg].[IsActive] = 1
							WHERE [sf].[Id] = @Id AND [sf].[IsActive] = 1 AND [sf].[IsDelete] = 0";
		#endregion


		#region SEARCH DYNAMIC QUERIES
		// TODO : For Quality change this Query to SP
		public string QUERY_StudentSearch_Select_AddEditUser { get; } = @"SELECT [sa].[Id], [sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
		[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], [sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
		[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
		[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
		[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
		, [c].[Id], [c].[Name], [c].[NormalizedName]	
		, [s].[Id], [s].[Name], [s].[NormalizedName]
	
		, [t4].[Id], [t4].[FullName], [t4].[Email]
		, [t5].[Id], [t5].[FullName], [t5].[Email]
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
		public string QUERY_StudentSearch_Select { get; } = @"SELECT [sa].[Id], [sa].[FormNo], [sa].[ClassId], [sa].[SectionId], [sa].[StudentLoginId], [sa].[AdmissionDate],
		[sa].[FirstName], [sa].[LastName], [sa].[FatherName], [sa].[MobileNo], [sa].[Gender], [sa].[GuardianMobileNo],
		[sa].[Sr_RegNo], [sa].[EnrollNo], [sa].[MACHINEID], [sa].[PreviousClass], [sa].[PreviousInstituteName], [sa].[YearOfPassing],
		[sa].[PreviousRollNo], [sa].[PreviousPercent], [sa].[MotherName], [sa].[DateOfBirth], [sa].[Category], [sa].[AlternateMobileNo],
		[sa].[StudentEmail], [sa].[GuardianEmail], [sa].[StudentType], [sa].[Medium], [sa].[Address], [sa].[CityId], [sa].[Village],
		[sa].[StateId], [sa].[Religion], [sa].[Image], [sa].[Remarks], [sa].[IsActive], [sa].[IsDelete], [sa].[ModifyDate], [sa].[AddedDate]
	
		, [c].[Id], [c].[Name], [c].[NormalizedName]	
		, [s].[Id], [s].[Name], [s].[NormalizedName]
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

		#endregion
	}
}
