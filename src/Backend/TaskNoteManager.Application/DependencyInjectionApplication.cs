using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskNoteManager.Application.Map;
using TaskNoteManager.Application.Services.User.Register;

namespace TaskNoteManager.Application
{
    public static class DependencyInjectionApplication
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
            AddAutoMapping(services);
                
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserService, RegisterUserService>();
        }

        private static void AddAutoMapping(IServiceCollection services)
        {
            services.AddScoped(opt=> new MapperConfiguration(mapOpt =>
            {
                mapOpt.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
    }
}
