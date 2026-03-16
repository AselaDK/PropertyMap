using PropertyMap.Core.Entities;

namespace PropertyMap.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(bool success, string? token, string? refreshToken, User? user, string? error)>
            LoginAsync(string username, string password);

        Task<(bool success, string? token, string? refreshToken, string? error)>
            RefreshTokenAsync(string token, string refreshToken);

        Task<bool> LogoutAsync(int userId);
        Task<User?> ValidateTokenAsync(string token);
    }
}