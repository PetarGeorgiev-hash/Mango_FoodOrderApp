using Mango.Services.AuthApi.Dto;

namespace Mango.Services.AuthApi.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> Register(RegisterDto register);

        Task<LoginResponseDto> Login(LoginDto login);

        Task<bool> AssignRole(string email, string role);
    }
}
