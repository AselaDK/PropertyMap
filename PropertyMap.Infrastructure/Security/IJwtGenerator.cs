using System.Security.Claims;
using PropertyMap.Core.Entities;

namespace PropertyMap.Infrastructure.Security
{
    public interface IJwtGenerator
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        bool ValidateToken(string token);
        int GetUserIdFromToken(string token);
    }
}