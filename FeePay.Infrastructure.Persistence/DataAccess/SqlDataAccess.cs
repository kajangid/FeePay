using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FeePay.Core.Application.Interface;

namespace FeePay.Infrastructure.Persistence.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> SP_LoadDataAsync<T, U>(string storedProcedure, U parameters);
        Task<int> SP_SaveDataAsync<T>(string storedProcedure, T parameters);
        void StartTransaction();
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        void CommitTransaction();
        void RollbackTransaction();

    }
    internal class SqlDataAccess : ISqlDataAccess, IDisposable
    {
        public SqlDataAccess(IConnectionStringBuilder connectionStringBuilder)
        {
            _DefaultConnectionString = connectionStringBuilder.GetDefaultConnectionString();
        }
        private readonly string _DefaultConnectionString;

        public async Task<List<T>> SP_LoadDataAsync<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
            List<T> rows = (await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure)).ToList();
            return rows;
        }

        public async Task<int> SP_SaveDataAsync<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new SqlConnection(_DefaultConnectionString);
            return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction()
        {
            _connection = new SqlConnection(_DefaultConnectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            isClosed = false;
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                       commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;

        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: _transaction);

        }

        private bool isClosed = false;

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (!isClosed)
            {
                try
                {
                    CommitTransaction();
                }
                catch
                {
                    // TODO: Log this issue
                }
            }

            _transaction = null;
            _connection = null;
        }

        // TODO: Open connection/start transaction method
        // load using the transaction
        // save using the transaction
        // close connection/stop transaction method
        // dispose
    }
}
