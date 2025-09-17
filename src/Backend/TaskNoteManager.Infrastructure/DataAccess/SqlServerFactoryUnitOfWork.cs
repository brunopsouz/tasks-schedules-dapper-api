using Microsoft.Data.SqlClient;
using TaskNoteManager.Infrastructure.DataAccess.UnitOfWork;

namespace TaskNoteManager.Infrastructure.DataAccess
{
    /// <summary>
    /// Specialized Unit of Work implementation for SQL Server.
    /// Inherits from the generic <see cref="UnitOfWork{TConnection}"/> using <see cref="SqlConnection"/> as the database connection type.
    /// </summary>
    internal sealed class SqlServerFactoryUnitOfWork : UnitOfWork<SqlConnection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerFactoryUnitOfWork"/> class
        /// with the specified SQL Server connection string.
        /// </summary>
        /// <param name="connectionString">The connection string used to establish a SQL Server database connection.</param>
        public SqlServerFactoryUnitOfWork(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
