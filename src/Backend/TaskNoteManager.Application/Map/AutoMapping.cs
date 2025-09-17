using AutoMapper;
using TaskNoteManager.Communication.Requests;
using TaskNoteManager.Communication.Responses;
using TaskNoteManager.Domain.Entities;

namespace TaskNoteManager.Application.Map
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUser, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {
            CreateMap<User, ResponseRegisterUser>();
        }
    }
}
