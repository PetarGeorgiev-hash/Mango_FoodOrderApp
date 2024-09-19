using Mango.Services.AuthApi.Models;

namespace Mango.Services.AuthApi.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
