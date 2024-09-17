using Mango.Services.AuthApi.IServices;
using Mango.Services.AuthApi.MapperProfiles;

namespace Mango.Services.AuthApi.Services
{
    public static class DependancyInjectionService
    {
        public static void InitDiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
