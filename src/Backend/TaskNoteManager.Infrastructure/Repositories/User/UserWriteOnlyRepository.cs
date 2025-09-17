using Dapper;
using System.Data;
using TaskNoteManager.Domain.DataAccess.UnitOfWork;
using TaskNoteManager.Domain.Repositories.User;

namespace TaskNoteManager.Infrastructure.Repositories.User
{
    /// <summary>
    /// Repository responsible for handling write-only operations related to the User entity.
    /// Uses Dapper to persist user data to the database within a transactional context.
    /// </summary>
    internal sealed class UserWriteOnlyRepository : IUserWriteOnlyRepository
    {
        /// <summary>
        /// Unit of Work instance used to manage database connection and transaction lifecycle.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Gets the active database connection from the Unit of Work.
        /// </summary>
        private IDbConnection Connection => _unitOfWork.Connection;

        /// <summary>
        /// Gets the active database transaction from the Unit of Work.
        /// </summary>
        private IDbTransaction Transaction => _unitOfWork.Transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserWriteOnlyRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The Unit of Work instance used for database operations.</param>
        public UserWriteOnlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Persists a new user record into the database.
        /// Executes an INSERT statement using Dapper and binds user properties to SQL parameters.
        /// </summary>
        /// <param name="user">The user entity containing data to be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(Domain.Entities.User user)
        {
            var sql = @$"INSERT INTO Users (Name, Email, Password, Position, UserType, UserIdentifier)
                         VALUES (@Name, @Email, @Password, @Position, @UserType, @UserIdentifier)";

            var result = await Connection.ExecuteScalarAsync<Domain.Entities.User>(sql, new
            {
                user.Name,
                user.Email,
                user.Password,
                Position = user.Position.ToString(),
                UserType = user.UserType.ToString(),
                user.UserIdentifier

            }, transaction: Transaction);
        }
    }
}
