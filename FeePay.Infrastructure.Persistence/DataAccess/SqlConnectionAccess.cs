using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.Persistence.DataAccess
{
    public sealed class SqlConnectionAccess : ISqlConnectionAccess
    {
        internal SqlConnectionAccess(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }

        IDbConnection _connection = null;
        IDbTransaction _transaction = null;
        Guid _id = Guid.Empty;

        IDbConnection ISqlConnectionAccess.Connection
        {
            get { return _connection; }
        }
        IDbTransaction ISqlConnectionAccess.Transaction
        {
            get { return _transaction; }
        }
        Guid ISqlConnectionAccess.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }

    }
}
