using PropertyMap.Application.DTOs.Auth;

namespace PropertyMap.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> AuthenticateAsync(LoginDto loginDto);
        Task<LoginResponseDto> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> RevokeTokenAsync(int userId);
        Task<UserDto?> GetCurrentUserAsync(int userId);
    }
}