using System.Data;

namespace TaskNoteManager.Domain.DataAccess.UnitOfWork
{
    /// <summary>
    /// Defines a contract for managing database transactions and connections  within a unit of work pattern.
    /// </summary>
    /// <remarks>The <see cref="IUnitOfWork"/> interface provides methods to begin, commit,  and roll back
    /// transactions, as well as access the underlying database  connection and transaction objects. It is typically
    /// used to ensure  consistency and atomicity when performing multiple database operations  within a single logical
    /// unit of work.</remarks>
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
