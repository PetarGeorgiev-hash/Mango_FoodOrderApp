using Mango.Web.IService;
using Mango.Web.Models;
using Mango.Web.Utilitiy;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "/api/auth/AssignRole",
                Data = registerDto
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "/api/auth/login",
                Data = loginDto
            }, withBearer : false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBase + "/api/auth/register",
                Data = registerDto
            }, withBearer : false);
        }
    }
}
