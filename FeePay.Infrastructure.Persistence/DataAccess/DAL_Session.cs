using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.DataAccess
{
    public sealed class DAL_Session : IDisposable
    {
        public DAL_Session(string ConnectionString = null)
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException(ConnectionString);
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _unitOfWork_Transaction = new SqlConnectionAccess(_connection);
        }

        IDbConnection _connection = null;
        SqlConnectionAccess _unitOfWork_Transaction = null;

        public SqlConnectionAccess UnitOfWork_Transaction
        {
            get { return _unitOfWork_Transaction; }
        }

        public void Dispose()
        {
            _unitOfWork_Transaction.Dispose();
            _connection.Dispose();
        }
    }
}
