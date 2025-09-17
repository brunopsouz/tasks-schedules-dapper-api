using System.Data;
using System.Data.Common;
using TaskNoteManager.Domain.DataAccess.UnitOfWork;

namespace TaskNoteManager.Infrastructure.DataAccess.UnitOfWork
{
    /// <summary>
    /// Represents an abstract unit of work pattern implementation for managing database connections and transactions.
    /// </summary>
    /// <remarks>This class provides a base implementation for the unit of work pattern, encapsulating a
    /// database connection and transaction. Derived classes should implement the specific behavior for managing the
    /// lifecycle of the connection and transaction.</remarks>
    /// <typeparam name="TDbConnection">The type of database connection used by the unit of work. Must implement <see cref="IDbConnection"/> and have a
    /// parameterless constructor.</typeparam>
    internal abstract class UnitOfWork<TDbConnection> : IUnitOfWork where TDbConnection : IDbConnection, new()
    {
        /// <summary>
        /// Represents the database connection used for executing database operations.
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// Gets the current database connection. If _connection is null, the CreateConnection is called to open the connection.
        /// </summary>
        public IDbConnection Connection => _connection ?? CreateConnection();

        /// <summary>
        /// Represents the current database transaction associated with the operation.
        /// </summary>
        public IDbTransaction _transaction;

        /// <summary>
        /// Gets the current database transaction associated with the connection.
        /// </summary>
        public IDbTransaction Transaction => _transaction;

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <remarks>The connection string must be in a valid format recognized by the database provider. 
        /// Ensure sensitive information, such as passwords, is handled securely.</remarks>
        public string ConnectionString { get; set; }

        public UnitOfWork() { }

        /// <summary>
        /// Creates and opens a new database connection using the configured connection string.
        /// </summary>
        /// <remarks>The connection is opened immediately after being created. Ensure that the connection
        /// string  is valid and properly configured before calling this method. The caller is responsible for 
        /// disposing of the returned connection when it is no longer needed.</remarks>
        /// <returns>An <see cref="IDbConnection"/> instance representing the open database connection.</returns>
        internal IDbConnection CreateConnection()
        {
            _connection = new TDbConnection
            {
                ConnectionString = ConnectionString
            };

            _connection.Open();
            return _connection;
        }
        
        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <remarks>This method starts a transaction on the current database connection.  Ensure that the
        /// connection is open before calling this method.  After starting a transaction, you can execute multiple
        /// commands within the transaction  and commit or roll back the transaction as needed.</remarks>
        public void BeginTransaction()
        {
            _transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// Commits the current transaction.
        /// </summary>
        /// <remarks>This method finalizes the current transaction, making all changes within the
        /// transaction permanent. Ensure that a transaction has been initiated before calling this method.</remarks>
        /// <exception cref="InvalidOperationException">Thrown if no transaction has been initiated prior to calling this method.</exception>
        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No transaction has been initiated.");

            _transaction.Commit();
        }

        /// <summary>
        /// Rolls back the current transaction, if one is active.
        /// </summary>
        /// <remarks>If no transaction is active, this method does nothing. Use this method to undo
        /// changes made during the transaction.</remarks>
        public void Rollback()
        {
            if (_transaction == null)
                return;

            _transaction.Rollback();
        }

        /// <summary>
        /// Releases all resources used by the current instance of the class.
        /// </summary>
        /// <remarks>This method disposes of the underlying transaction and database connection objects, 
        /// freeing any resources they hold. After calling this method, the instance should not  be used
        /// further.</remarks>
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
