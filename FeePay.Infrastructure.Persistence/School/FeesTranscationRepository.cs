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

        public async Task<int> AddAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = @"INSERT INTO [dbo].[FeesTranscation]
                                ([UserId],[TransactionId],[TransactionMode],[Amount],[IsComplete],[State],[Date],[Receipt])
                                VALUES(@UserId,@TransactionId,@TransactionMode,@Amount,@IsComplete,@State,@Date,@Receipt)
                                SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var parameters = new
                {
                    feesTranscation.UserId,
                    feesTranscation.TransactionId,
                    feesTranscation.TransactionMode,
                    feesTranscation.Amount,
                    feesTranscation.IsComplete,
                    feesTranscation.State,
                    feesTranscation.Date,
                    feesTranscation.Receipt,
                };
                var paymentId = await connection.ExecuteScalarAsync<int>(sql,parameters, commandType: CommandType.Text);
                if(paymentId < 0 )
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
                    tempParams.Add("@IsPaid", DBNull.Value, DbType.Boolean, ParameterDirection.Input);
                    return tempParams;
                });
                var sql1 = @"CREATE TABLE  #routineUpdatedRecords
                            ([Id] INT,[Status] NVARCHAR(20),[PaymentId] NVARCHAR(50),[Mode] NVARCHAR(50),
                            [PaymentDate] DATETIME,[IsPaid] BIT);
                            INSERT INTO #routineUpdatedRecords 
                            VALUES(@Id, @Status, @PaymentId, @Mode, @PaymentDate, @IsPaid)";
                var res = await connection.ExecuteAsync(sql1, parameters1, commandType: CommandType.Text);

                var sql2 = @"UPDATE [sf] SET 
                            [sf].[Status]		=	CASE WHEN [ur].[Status] IS NOT NULL THEN [ur].[Status] ELSE [ur].[Status] END, 
                            [sf].[PaymentId]	=	CASE WHEN [ur].[PaymentId] IS NOT NULL THEN [ur].[PaymentId] ELSE [sf].[PaymentId] END, 
                            [sf].[Mode]			=	CASE WHEN [ur].[Mode] IS NOT NULL THEN [ur].[Mode] ELSE [sf].[Mode] END, 
                            [sf].[PaymentDate]	=	CASE WHEN [ur].[PaymentDate] IS NOT NULL THEN [ur].[PaymentDate] ELSE [sf].[PaymentDate] END, 
                            [sf].[IsPaid]		=	CASE WHEN [ur].[IsPaid] IS NOT NULL THEN [ur].[IsPaid] ELSE [sf].[IsPaid] END
                            FROM [StudentFees] [sf]
                            INNER JOIN #routineUpdatedRecords [ur] 
                            ON 
                            [sf].[Id] = [ur].[Id] AND [sf].[IsActive] = 1 AND [sf].[IsDelete] = 0";
                var numResults = await connection.ExecuteAsync(sql2, commandType: CommandType.Text);
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
        public async Task<int> UpdateAsync(FeesTranscation feesTranscation, List<StudentFees> fees, string dbId)
        {
            IDbConnection connection = new SqlConnection(GetConStr(dbId));
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var sql = @"UPDATE [dbo].[FeesTranscation]  SET
                                [TransactionId]		=	CASE WHEN @TransactionId IS NOT NULL THEN @TransactionId ELSE [TransactionId] END,
                                [TransactionMode]	=	CASE WHEN @TransactionMode IS NOT NULL THEN @TransactionMode ELSE [TransactionMode] END,
                                [Amount]			=	CASE WHEN @Amount IS NOT NULL THEN @Amount ELSE [Amount] END,
                                [IsComplete]		=	CASE WHEN @IsComplete IS NOT NULL THEN @IsComplete ELSE [IsComplete] END,
                                [State]				=	CASE WHEN @State IS NOT NULL THEN @State ELSE [State] END,
                                [Date]				=	CASE WHEN @Date IS NOT NULL THEN @Date ELSE [Date] END,
                                [Receipt]			=	CASE WHEN @Receipt IS NOT NULL THEN @Receipt ELSE [Receipt] END
                                Where [Id] = @Id
                                SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var parameters = new
                {
                    feesTranscation.Id,
                    feesTranscation.UserId,
                    feesTranscation.TransactionId,
                    feesTranscation.TransactionMode,
                    feesTranscation.Amount,
                    feesTranscation.IsComplete,
                    feesTranscation.State,
                    feesTranscation.Date,
                    feesTranscation.Receipt,
                };
                var paymentId = await connection.ExecuteScalarAsync<int>(sql, parameters, commandType: CommandType.Text);
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
                    tempParams.Add("@IsPaid", DBNull.Value, DbType.Boolean, ParameterDirection.Input);
                    return tempParams;
                });
                var sql1 = @"CREATE TABLE  #routineUpdatedRecords
                            ([Id] INT,[Status] NVARCHAR(20),[PaymentId] NVARCHAR(50),[Mode] NVARCHAR(50),
                            [PaymentDate] DATETIME,[IsPaid] BIT);
                            INSERT INTO #routineUpdatedRecords 
                            VALUES(@Id, @Status, @PaymentId, @Mode, @PaymentDate, @IsPaid)";
                var res = await connection.ExecuteAsync(sql1, parameters1, commandType: CommandType.Text);

                var sql2 = @"UPDATE [sf] SET 
                            [sf].[Status]		=	CASE WHEN [ur].[Status] IS NOT NULL THEN [ur].[Status] ELSE [ur].[Status] END, 
                            [sf].[PaymentId]	=	CASE WHEN [ur].[PaymentId] IS NOT NULL THEN [ur].[PaymentId] ELSE [sf].[PaymentId] END, 
                            [sf].[Mode]			=	CASE WHEN [ur].[Mode] IS NOT NULL THEN [ur].[Mode] ELSE [sf].[Mode] END, 
                            [sf].[PaymentDate]	=	CASE WHEN [ur].[PaymentDate] IS NOT NULL THEN [ur].[PaymentDate] ELSE [sf].[PaymentDate] END, 
                            [sf].[IsPaid]		=	CASE WHEN [ur].[IsPaid] IS NOT NULL THEN [ur].[IsPaid] ELSE [sf].[IsPaid] END
                            FROM [StudentFees] [sf]
                            INNER JOIN #routineUpdatedRecords [ur] 
                            ON 
                            [sf].[Id] = [ur].[Id] AND [sf].[IsActive] = 1 AND [sf].[IsDelete] = 0";
                var numResults = await connection.ExecuteAsync(sql2, commandType: CommandType.Text);
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

        public async Task<FeesTranscation> FindByIdAsync(int feeTranscationId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                string sql = @"SELECT [Id],[UserId],[TransactionId],[TransactionMode],[Amount],
                                [IsComplete],[State],[Date],[Receipt]
                                FROM [dbo].[FeesTranscation]
                                WHERE [Id] = @Id";
                var SpRequiredParameters = new { Id = feeTranscationId };
                return await connection.QuerySingleOrDefaultAsync<FeesTranscation>(
                    sql,
                    SpRequiredParameters,
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
        public async Task<FeesTranscation> FindByTranscationIdAsync(string transcationId, string dbId)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(GetConStr(dbId));
                string sql = @"SELECT [Id],[UserId],[TransactionId],[TransactionMode],[Amount],
                                [IsComplete],[State],[Date],[Receipt]
                                FROM [dbo].[FeesTranscation]
                                WHERE [TransactionId] = @TransactionId";
                var SpRequiredParameters = new { TransactionId = transcationId};
                return await connection.QuerySingleOrDefaultAsync<FeesTranscation>(
                    sql,
                    SpRequiredParameters,
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
