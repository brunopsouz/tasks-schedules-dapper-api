namespace TaskNoteManager.Domain.Repositories.User
{
    /// <summary>
    /// Defines read-only operations related to the User entity.
    /// Intended for queries that do not modify the database state.
    /// </summary>
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);
    }
}
