﻿namespace TaskNoteManager.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        Task AddAsync(Entities.User user);
    }
}
