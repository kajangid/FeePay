using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Common
{
    public interface IDBVariables
    {
        #region SUPER ADMIN IDENTITY
        string SP_Add_SuperAdmin_User { get; }
        string SP_Delete_SuperAdmin_User { get; }
        string SP_Get_SuperAdmin_User { get; }
        string SP_GetAll_SuperAdmin_User { get; }
        string SP_Update_SuperAdmin_User { get; }
        string SP_Add_SuperAdmin_Role { get; }
        string SP_Delete_SuperAdmin_Role { get; }
        string SP_Get_SuperAdmin_Role { get; }
        string SP_GetAll_SuperAdmin_Role { get; }
        string SP_Update_SuperAdmin_Role { get; }
        string SP_Add_SuperAdmin_UserRole { get; }
        string SP_Delete_SuperAdmin_UserRole { get; }
        string SP_Get_SuperAdmin_UserRole { get; }
        string SP_Update_SuperAdmin_UserRole { get; }
        string SP_GetUserRoles_SuperAdmin { get; }
        string SP_Get_SuperAdmin_UsersInRole { get; }
        string SP_GetAll_SuperAdmin_UserRole { get; }
        string SP_AddLoginInfo_SuperAdmin { get; }
        string QUERY_Password_SuperAdmin { get; }
        #endregion


        #region SCHOOL ADMIN IDENTITY
        string SP_Add_SchoolAdmin_User { get; }
        string SP_Delete_SchoolAdmin_User { get; }
        string SP_Get_SchoolAdmin_User { get; }
        string SP_GetAll_SchoolAdmin_User { get; }
        string SP_Get_SchoolAdmin_User_With_AddEditUser { get; }
        string SP_GetAll_SchoolAdmin_User_With_AddEditUser { get; }
        string SP_Update_SchoolAdmin_User { get; }
        string SP_Add_SchoolAdmin_Role { get; }
        string SP_Delete_SchoolAdmin_Role { get; }
        string SP_Get_SchoolAdmin_Role { get; }
        string SP_GetAll_SchoolAdmin_Role { get; }
        string SP_Get_SchoolAdmin_Role_With_AddEditUser { get; }
        string SP_GetAll_SchoolAdmin_Role_With_AddEditUser { get; }
        string SP_Update_SchoolAdmin_Role { get; }
        string SP_Add_SchoolAdmin_UserRole { get; }
        string SP_Delete_SchoolAdmin_UserRole { get; }
        string SP_Get_SchoolAdmin_UserRole { get; }
        string SP_Update_SchoolAdmin_UserRole { get; }
        string SP_Get_SchoolAdmin_UserRoles { get; }
        string SP_Get_SchoolAdmin_UsersInRole { get; }
        string SP_GetAll_SchoolAdmin_UserRole { get; }
        string SP_Add_SchoolAdmin_User_LoginInfo { get; }
        string QUERY_FindUserPassword_SchoolAdminUser { get; }
        string QUERY_GetAllUserPassword_SchoolAdminUser { get; }
        #endregion


        #region STUDENT LOGIN IDENTITY
        string SP_Add_StudentLogin { get; }
        string SP_Update_StudentLogin { get; }
        string SP_Delete_StudentLogin { get; }
        string SP_Get_StudentLogin { get; }
        string SP_GetAll_StudentLogin { get; }
        string SP_Add_StudentLogin_LoginInfo { get; }
        string QUERY_FindUserPassword_StudentLogin { get; }
        string QUERY_GetAllUserPassword_StudentLogin { get; }
        #endregion


        #region REGISTER SCHOOL 
        string SP_Add_RegisteredSchool { get; }
        string SP_Update_RegisteredSchool { get; }
        string SP_Delete_RegisteredSchool { get; }
        string SP_Get_RegisteredSchool { get; }
        string SP_GetAll_RegisteredSchool { get; }
        #endregion


        #region FEE TYPE 
        string SP_Add_FeeType { get; }
        string SP_Update_FeeType { get; }
        string SP_Delete_FeeType { get; }
        string SP_Get_FeeType { get; }
        string SP_GetAll_FeeType { get; }
        string SP_Get_FeeType_AddEditUser { get; }
        string SP_GetAll_FeeType_AddEditUser { get; }
        #endregion


        #region FEE GROUP
        string SP_Add_FeeGroup { get; }
        string SP_Update_FeeGroup { get; }
        string SP_Delete_FeeGroup { get; }
        string SP_Get_FeeGroup { get; }
        string SP_GetAll_FeeGroup { get; }
        string SP_Get_FeeGroup_AddEditUser { get; }
        string SP_GetAll_FeeGroup_AddEditUser { get; }
        string SP_GetAll_FeeGroup_MasterAndType { get; }
        #endregion


        #region FEE MASTER
        string SP_Add_FeeMaster { get; }
        string SP_Update_FeeMaster { get; }
        string SP_Delete_FeeMaster { get; }
        string SP_Get_FeeMaster { get; }
        string SP_GetAll_FeeMaster { get; }
        string SP_Get_FeeMaster_AddEditUser { get; }
        string SP_GetAll_FeeMaster_AddEditUser { get; }
        #endregion


        #region CLASS
        string SP_Add_Class { get; }
        string SP_Update_Class { get; }
        string SP_Delete_Class { get; }
        string SP_Get_Class { get; }
        string SP_Get_Class_AddEditUser { get; }
        #endregion


        #region SECTION
        string SP_Add_Section { get; }
        string SP_Update_Section { get; }
        string SP_Delete_Section { get; }
        string SP_Get_Section { get; }
        string SP_Get_Section_AddEditUser { get; }
        #endregion


        #region CLASSSECTION
        string SP_Add_ClassSection { get; }
        string SP_Remove_ClassSection { get; }
        string SP_Get_ClassSection { get; }
        string SP_GetAll_ClassesSections_AddEditUser { get; }
        #endregion


        #region SESSION
        string SP_Add_Session { get; }
        string SP_Update_Session { get; }
        string SP_Delete_Session { get; }
        string SP_Get_Session { get; }
        string SP_SetDefault_Session { get; }
        string SP_Get_Session_AddEditUser { get; }
        #endregion


        #region STUDENTADMISSION
        string SP_Add_StudentAdmission { get; }
        string SP_Update_StudentAdmission { get; }
        string SP_Delete_StudentAdmission { get; }
        string SP_Get_StudentAdmission { get; }
        string SP_Search_Students { get; }
        string SP_Get_StudentAdmission_AddEditUser { get; }
        string QUERY_BulkUpdate_StudentAdmission { get; }
        string QUERY_StudentSearch_Select_AddEditUser { get; }
        string QUERY_StudentSearch_Select { get; }
        string QUERY_StudentSearch_Where_IsActive { get; }
        string QUERY_StudentSearch_Where_ClassId { get; }
        string QUERY_StudentSearch_Where_SectionId { get; }
        string QUERY_StudentSearch_Where_Gender { get; }
        string QUERY_StudentSearch_Where_SearchIn { get; }
        string QUERY_StudentSearch_Where_StudentId { get; }
        string QUERY_StudentSearch_Where_AcademicSessionId { get; }
        #endregion


        #region STATE & CITY
        string QUERY_GetAll_Active_Cities { get; }
        string QUERY_GetAll_Active_States { get; }
        string QUERY_Find_Active_Cities_ByStateId { get; }
        string QUERY_Find_Active_CityByIdAsync { get; }
        string QUERY_Find_Active_StateByIdAsync { get; }
        #endregion


        #region STUDENTFEES
        string SP_Add_StudentFees { get; }
        string SP_Get_StudentFees { get; }
        string SP_Remove_StudentFees { get; }
        string QUERY_IsFeeAssignToStudent { get; }
        string QUERY_StudentFeeList { get; }
        string QUERY_Find_StudentFee { get; }
        string QUERY_StudentFeeList_ByTranscationId { get; }
        string QUERY_GetClasses_Fees { get; }
        string QUERY_GetClassStudents_Fees { get; }
        string QUERY_STUDENTFEES_GetAll { get; }
        string QUERY_STUDENTFEES_GetAll_Where_FromDate { get; }
        string QUERY_STUDENTFEES_GetAll_Where_ToDate { get; }
        string QUERY_STUDENTFEES_GetAll_Where_ClassId { get; }
        string QUERY_STUDENTFEES_GetAll_Where_SectionId { get; }
        string QUERY_STUDENTFEES_GetAll_Where_StudentAdmissionId { get; }
        string QUERY_STUDENTFEES_GetAll_Where_AcademicSessionId { get; }
        string QUERY_STUDENTFEES_GetAll_Where_IsPaid { get; }
        string QUERY_STUDENTFEES_GetAll_Where_SearchParam { get; }
        #endregion


        #region SEARCH DYNAMIC QUERIES

        #endregion


        #region STUDENT ACADEMIC SESSIONS
        string SP_Add_Student_Academic_Sessions { get; }
        string SP_Update_Student_Academic_Sessions { get; }
        string SP_Delete_Student_Academic_Sessions { get; }
        string SP_Get_Student_Academic_Sessions { get; }
        string QUERY_BulkUpdate_Student_Academic_Sessions { get; }
        #endregion

        #region FEESTRANSACTION
        string QUERY_Add_FeesTransaction { get; }
        string QUERY_Update_FeesTransaction { get; }
        string QUERY_FeesTransaction_BulkUpdate_StudentFees { get; }
        string QUERY_FindById_FeesTransaction { get; }
        string QUERY_FindByTranscationId_FeesTransaction { get; }
        string QUERY_GetAll_FeesTransaction { get; }
        string QUERY_GetAll_FeesTransaction_WithStudentAdmission { get; }
        string QUERY_GetAll_FeesTransaction_Where_FromDate { get; }
        string QUERY_GetAll_FeesTransaction_Where_ToDate { get; }
        string QUERY_GetAll_FeesTransaction_Where_UserId { get; }
        string QUERY_GetAll_FeesTransaction_Where_IsComplete { get; }
        string QUERY_GetAll_FeesTransaction_Where_Receipt { get; }
        string QUERY_GetAll_FeesTransaction_Where_TransactionMode { get; }
        string QUERY_GetAll_FeesTransaction_Where_State { get; }
        #endregion


        #region DOCUMENTS
        string SP_Add_Document { get; }
        string SP_Update_Document { get; }
        string SP_Get_Document { get; }
        string SP_Delete_Document { get; }
        #endregion


        #region PAYMENT_GATEWAY_DOCUMENT
        string SP_Add_PaymentGatewayDocument { get; }
        string SP_Update_PaymentGatewayDocument { get; }
        string SP_Get_PaymentGatewayDocument { get; }
        string SP_Delete_PaymentGatewayDocument { get; }
        string QUERY_GetAll_PaymentGatewayDocument_WithSchoolData { get; }
        #endregion
    }
}
