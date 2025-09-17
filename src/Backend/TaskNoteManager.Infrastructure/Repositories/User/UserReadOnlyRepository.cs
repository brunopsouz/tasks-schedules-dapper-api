using Dapper;
using System.Data;
using TaskNoteManager.Domain.DataAccess.UnitOfWork;
using TaskNoteManager.Domain.Repositories.User;
using TaskNoteManager.Infrastructure.DataAccess;

namespace TaskNoteManager.Infrastructure.Repositories.User
{

    /// <summary>
    /// Repository responsible for handling read-only operations related to the User entity.
    /// Uses Dapper to query the database within a transactional context.
    /// </summary>
    internal sealed class UserReadOnlyRepository : IUserReadOnlyRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDbConnection Connection => _unitOfWork.Connection;
        private IDbTransaction Transaction => _unitOfWork.Transaction;

        public UserReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Checks whether a user with the specified email address already exists in the database.
        /// Executes a SELECT query using Dapper and returns a boolean result.
        /// </summary>
        /// <param name="email">The email address to check for existence.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// Returns <c>true</c> if a user with the given email exists; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> ExistsUserWithEmail(string email)
        {
            var sql = $"SELECT IIF(EXISTS (SELECT 1 FROM Users WHERE Email = @Email), 1, 0)";

            var result = await Connection.ExecuteScalarAsync<bool>(sql, new { Email = email }, transaction: Transaction);

            return result;
        }
    }
}
