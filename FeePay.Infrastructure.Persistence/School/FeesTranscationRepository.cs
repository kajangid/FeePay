using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Application.Enums;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Common;
using FeePay.Core.Application.Interface.Repository.School;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Infrastructure.Persistence.School
{
    public class FeesTranscationRepository : IFeesTranscationRepository
    {
        private readonly IDBVariables _dBVariables;
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public FeesTranscationRepository(IConnectionStringBuilder connectionStringBuilder, IDBVariables dBVariables)
        {
            _dBVariables = dBVariables;
            _connectionStringBuilder = connectionStringBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feesTranscation"></param>
        /// <param name="fees"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = new
                {
                    feesTranscation.UserId,
                    feesTranscation.TransactionId,
                    feesTranscation.TransactionMode,
                    feesTranscation.Amount,
                    feesTranscation.IsComplete,
                    feesTranscation.State,
                    feesTranscation.Date
                };
                var paymentId = await connection.ExecuteScalarAsync<int>(
                    sql: _dBVariables.QUERY_Add_FeesTransaction,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (paymentId < 0)
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }

                var parameters1 = fees.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Id", s.Id, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@PaymentId", s.PaymentId, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Status", s.Status, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Mode", s.Mode, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@PaymentDate", DBNull.Value, DbType.DateTime, ParameterDirection.Input);
                    tempParams.Add("@IsPaid", s.IsPaid, DbType.Boolean, ParameterDirection.Input);
                    return tempParams;
                });

                var numResults = await connection.ExecuteAsync(
                    sql: _dBVariables.QUERY_FeesTransaction_BulkUpdate_StudentFees,
                    param: parameters1,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (numResults < 0)
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
                transaction?.Commit();
                connection?.Close();
                return 1;
            }
            catch (TimeoutException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
            finally
            {
                connection?.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feesTranscation"></param>
        /// <param name="fees"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var parameters = new
                {
                    feesTranscation.Id,
                    feesTranscation.UserId,
                    feesTranscation.TransactionId,
                    feesTranscation.TransactionMode,
                    feesTranscation.Amount,
                    feesTranscation.IsComplete,
                    feesTranscation.State,
                    feesTranscation.Date
                };
                var paymentId = await connection.ExecuteScalarAsync<int>(
                    sql: _dBVariables.QUERY_Update_FeesTransaction,
                    param: parameters,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (paymentId < 0)
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }

                var parameters1 = fees.Select(s =>
                {
                    var tempParams = new DynamicParameters();
                    tempParams.Add("@Id", s.Id, DbType.Int32, ParameterDirection.Input);
                    tempParams.Add("@PaymentId", s.PaymentId, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Status", s.Status, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@Mode", s.Mode, DbType.String, ParameterDirection.Input);
                    tempParams.Add("@PaymentDate", s.PaymentDate, DbType.DateTime, ParameterDirection.Input);
                    tempParams.Add("@IsPaid", s.IsPaid, DbType.Boolean, ParameterDirection.Input);
                    return tempParams;
                });

                var numResults = await connection.ExecuteAsync(
                    sql: _dBVariables.QUERY_FeesTransaction_BulkUpdate_StudentFees,
                    param: parameters1,
                    transaction: transaction,
                    commandType: CommandType.Text);

                if (numResults < 0)
                {
                    transaction?.Rollback();
                    connection?.Close();
                    return 0;
                }
                transaction?.Commit();
                connection?.Close();
                return 1;
            }
            catch (TimeoutException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception(String.Format("{0}.WithConnection() experienced a exception", GetType().FullName), ex);
            }
            finally
            {
                connection?.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeTranscationId"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public async Task<FeesTranscation> FindByIdAsync(int feeTranscationId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { Id = feeTranscationId };
                return await connection.QuerySingleOrDefaultAsync<FeesTranscation>(
                    sql: _dBVariables.QUERY_FindById_FeesTransaction,
                    param: SpRequiredParameters,
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
                throw new Exception(String.Format("{0}.WithConnection() experienced an exception", GetType().FullName), ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transcationId"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public async Task<FeesTranscation> FindByTranscationIdAsync(string transcationId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                var SpRequiredParameters = new { TransactionId = transcationId };
                return await connection.QuerySingleOrDefaultAsync<FeesTranscation>(
                    sql: _dBVariables.QUERY_FindByTranscationId_FeesTransaction,
                    param: SpRequiredParameters,
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="isComplated"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studentLoginId"></param>
        /// <param name="Receipt"></param>
        /// <param name="transactionMode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FeesTranscation>> GetAllAsync(string dbId, bool? isComplated = null,
            DateTime? fromDate = null, DateTime? toDate = null, int? studentLoginId = null, string Receipt = null,
            TransactionMode? transactionMode = null, TransactionStatus? status = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                StringBuilder Query = new StringBuilder();
                Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction);
                var parameters = new DynamicParameters();
                if (fromDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_FromDate);
                    parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (toDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_ToDate);
                    parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (studentLoginId is not null and not 0)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_UserId);
                    parameters.Add("@UserId", studentLoginId, DbType.Int32, ParameterDirection.Input);
                }
                if (isComplated is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_IsComplete);
                    parameters.Add("@IsComplete", isComplated, DbType.Boolean, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(Receipt) && !string.IsNullOrWhiteSpace(Receipt))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_Receipt);
                    parameters.Add("@Receipt", Receipt, DbType.String, ParameterDirection.Input, Receipt.Length);
                }
                if (transactionMode != null && !string.IsNullOrEmpty(nameof(transactionMode)))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_TransactionMode);
                    parameters.Add("@TransactionMode", nameof(transactionMode), DbType.String, ParameterDirection.Input);
                }
                if (status != null && !string.IsNullOrEmpty(nameof(status)))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_State);
                    parameters.Add("@State", nameof(transactionMode), DbType.String, ParameterDirection.Input);
                }

                return await connection.QueryAsync<FeesTranscation>(
                    Query.ToString(),
                    parameters,
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="isComplated"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="studentLoginId"></param>
        /// <param name="Receipt"></param>
        /// <param name="transactionMode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FeesTranscation>> GetAll_WithStudentAdmissionAsync(string dbId, bool? isComplated = null,
            DateTime? fromDate = null, DateTime? toDate = null, int? studentLoginId = null, string Receipt = null,
            TransactionMode? transactionMode = null, TransactionStatus? status = null)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                StringBuilder Query = new StringBuilder();
                Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_WithStudentAdmission);
                var parameters = new DynamicParameters();
                if (fromDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_FromDate);
                    parameters.Add("@FromDate", fromDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (toDate is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_ToDate);
                    parameters.Add("@ToDate", toDate, DbType.DateTime, ParameterDirection.Input);
                }
                if (studentLoginId is not null and not 0)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_FromDate);
                    parameters.Add("@UserId", studentLoginId, DbType.Int32, ParameterDirection.Input);
                }
                if (isComplated is not null)
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_IsComplete);
                    parameters.Add("@IsComplete", isComplated, DbType.Boolean, ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(Receipt) && !string.IsNullOrWhiteSpace(Receipt))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_Receipt);
                    parameters.Add("@Receipt", Receipt, DbType.String, ParameterDirection.Input, Receipt.Length);
                }
                if (transactionMode != null && !string.IsNullOrEmpty(nameof(transactionMode)))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_TransactionMode);
                    parameters.Add("@TransactionMode", nameof(transactionMode), DbType.String, ParameterDirection.Input);
                }
                if (status != null && !string.IsNullOrEmpty(nameof(status)))
                {
                    Query.Append(_dBVariables.QUERY_GetAll_FeesTransaction_Where_State);
                    parameters.Add("@State", nameof(transactionMode), DbType.String, ParameterDirection.Input);
                }

                return await connection.QueryAsync<FeesTranscation, StudentAdmission, FeesTranscation>(
                    Query.ToString(),
                    (ft, sa) =>
                    {
                        ft.StudentAdmission = sa;
                        return ft;
                    },
                    parameters,
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

        // private methods
        private string GetConStr(string dbId = null)
        {
            return string.IsNullOrEmpty(dbId) ?
                _connectionStringBuilder.GetSchoolConnectionString() : // Demo DB
                _connectionStringBuilder.GetDynamicSchoolConnectionString(dbId);
        }
    }
}
