using TaskNoteManager.Communication.Requests;
using TaskNoteManager.Communication.Responses;

namespace TaskNoteManager.Application.Services.User.Register
{
    public interface IRegisterUserService
    {
        Task<ResponseRegisterUser> Register(RequestRegisterUser request);
    }
}
