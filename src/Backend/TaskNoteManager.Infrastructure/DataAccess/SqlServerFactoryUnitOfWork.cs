using Microsoft.Data.SqlClient;
using TaskNoteManager.Infrastructure.DataAccess.UnitOfWork;

namespace TaskNoteManager.Infrastructure.DataAccess
{
    internal sealed class SqlServerFactoryUnitOfWork : UnitOfWork<SqlConnection>
    {
        public SqlServerFactoryUnitOfWork(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
