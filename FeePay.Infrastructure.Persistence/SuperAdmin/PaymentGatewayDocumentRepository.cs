using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.SuperAdmin;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.SuperAdmin;

namespace FeePay.Infrastructure.Persistence.SuperAdmin
{
    public class PaymentGatewayDocumentRepository : IPaymentGatewayDocumentRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly string _defaultConnectionString;
        public PaymentGatewayDocumentRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _defaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
        }

        #region Execute
        public async Task<int> AddAsync(PaymentGatewayDocument paymentGatewayDocument)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var Parameters = new
                {
                    paymentGatewayDocument.BusinessDisplayName,
                    paymentGatewayDocument.BusinessPANCardNumber,
                    paymentGatewayDocument.BusinessPANCardCopy,
                    paymentGatewayDocument.RegisteredAddress,
                    paymentGatewayDocument.AddressPinCode,
                    paymentGatewayDocument.AddressCityId,
                    paymentGatewayDocument.AddressStateId,
                    paymentGatewayDocument.AccountNumber,
                    paymentGatewayDocument.IFSC,
                    paymentGatewayDocument.BankPassbookCopy,
                    paymentGatewayDocument.ContactName,
                    paymentGatewayDocument.ContactEmail,
                    paymentGatewayDocument.ContactPhoneNumber,
                    paymentGatewayDocument.IdentityProof,
                    paymentGatewayDocument.RegisteredSchoolId,
                    paymentGatewayDocument.IsApproved,
                    paymentGatewayDocument.IsActive,
                    paymentGatewayDocument.AddedBy
                };
                return await connection.ExecuteScalarAsync<int>(
                    sql: _dBVariables.SP_Add_PaymentGatewayDocument,
                    param: Parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> UpdateAsync(PaymentGatewayDocument paymentGatewayDocument)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var Parameters = new
                {
                    paymentGatewayDocument.Id,
                    paymentGatewayDocument.BusinessDisplayName,
                    paymentGatewayDocument.BusinessPANCardNumber,
                    paymentGatewayDocument.BusinessPANCardCopy,
                    paymentGatewayDocument.RegisteredAddress,
                    paymentGatewayDocument.AddressPinCode,
                    paymentGatewayDocument.AddressCityId,
                    paymentGatewayDocument.AddressStateId,
                    paymentGatewayDocument.AccountNumber,
                    paymentGatewayDocument.IFSC,
                    paymentGatewayDocument.BankPassbookCopy,
                    paymentGatewayDocument.ContactName,
                    paymentGatewayDocument.ContactEmail,
                    paymentGatewayDocument.ContactPhoneNumber,
                    paymentGatewayDocument.IdentityProof,
                    paymentGatewayDocument.RegisteredSchoolId,
                    paymentGatewayDocument.IsApproved,
                    paymentGatewayDocument.IsActive,
                    paymentGatewayDocument.ModifyBy
                };
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Update_PaymentGatewayDocument,
                    param: Parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<int> DeleteAsync(int id, int userId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                return await connection.ExecuteAsync(
                    sql: _dBVariables.SP_Delete_PaymentGatewayDocument,
                    param: new { Id = id, ModifyBy = userId },
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion

        #region Find
        public async Task<PaymentGatewayDocument> FindByIdAsync(int id, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QuerySingleOrDefaultAsync<PaymentGatewayDocument>(
                    sql: _dBVariables.SP_Get_PaymentGatewayDocument,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<PaymentGatewayDocument> FindByRegisteredSchoolIdAsync(int registeredSchoolId, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@RegisteredSchoolId", registeredSchoolId, DbType.Int32, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QuerySingleOrDefaultAsync<PaymentGatewayDocument>(
                    sql: _dBVariables.SP_Get_PaymentGatewayDocument,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<PaymentGatewayDocument>> GetByIsApprovedAsync(bool isApproved, bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@RegisteredSchoolId", isApproved, DbType.Boolean, ParameterDirection.Input);
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QueryAsync<PaymentGatewayDocument>(
                    sql: _dBVariables.SP_Get_PaymentGatewayDocument,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion


        #region Get All
        public async Task<IEnumerable<PaymentGatewayDocument>> GetAllAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);

                return await connection.QueryAsync<PaymentGatewayDocument>(
                    sql: _dBVariables.SP_Get_PaymentGatewayDocument,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<PaymentGatewayDocument>> GetAll_WithSchoolDataAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@IsActive", isActive ?? (object)DBNull.Value, DbType.Boolean, ParameterDirection.Input);
                return await connection.QueryAsync<PaymentGatewayDocument, RegisteredSchool, PaymentGatewayDocument>(
                    sql: _dBVariables.QUERY_GetAll_PaymentGatewayDocument_WithSchoolData,
                    map: (pd, rs) => { pd.RegisteredSchool = rs; return pd; },
                    param: dynamicParams,
                    splitOn: "Id,Id",
                    commandType: CommandType.Text);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        public async Task<IEnumerable<PaymentGatewayDocument>> GetAll_WithAddEditUserAsync(bool? isActive = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_defaultConnectionString);
                var users = await connection.QueryAsync<SchoolAdminUser>(
                    sql: _dBVariables.SP_Get_SchoolAdmin_User,
                    param: new { IsActive = true },
                    commandType: CommandType.StoredProcedure);

                var list = await GetAllAsync(isActive);
                if (list != null && users != null && users.Any())
                {
                    var _classes = list.ToList();
                    _classes.ForEach(f =>
                    {
                        if (f.AddedBy != 0)
                            f.AddedByUser = users.Where(w => w.Id == f.AddedBy).SingleOrDefault();
                        if (f.ModifyBy != 0)
                            f.ModifyByUser = users.Where(w => w.Id == f.ModifyBy).SingleOrDefault();
                    });
                    return _classes;
                }
                return list;
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
        }
        #endregion
    }
}
