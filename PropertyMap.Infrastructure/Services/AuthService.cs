using Microsoft.Extensions.Options;
using PropertyMap.Core.Entities;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Core.Interfaces.Services;
using PropertyMap.Infrastructure.Security;

namespace PropertyMap.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUserRepository userRepository,
            IJwtGenerator jwtGenerator,
            IPasswordHasher passwordHasher,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<(bool success, string? token, string? refreshToken, User? user, string? error)> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return (false, null, null, null, "Invalid username or password");

            if (!user.IsActive)
                return (false, null, null, null, "Account is deactivated");

            // if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
            //     return (false, null, null, null, "Account is locked. Try again later.");

            if (!_passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                await _userRepository.UpdateLoginAttemptsAsync(user.Id, false);
                return (false, null, null, null, "Invalid username or password");
            }

            await _userRepository.UpdateLoginAttemptsAsync(user.Id, true);

            var accessToken = _jwtGenerator.GenerateAccessToken(user);
            var refreshToken = _jwtGenerator.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userRepository.UpdateAsync(user);

            return (true, accessToken, refreshToken, user, null);
        }

        public async Task<(bool success, string? token, string? refreshToken, string? error)> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtGenerator.GetPrincipalFromExpiredToken(token);
            if (principal == null)
                return (false, null, null, "Invalid token");

            var user = await _userRepository.GetUserWithRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshToken != refreshToken)
                return (false, null, null, "Invalid refresh token");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return (false, null, null, "Refresh token expired");

            var newAccessToken = _jwtGenerator.GenerateAccessToken(user);
            var newRefreshToken = _jwtGenerator.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userRepository.UpdateAsync(user);

            return (true, newAccessToken, newRefreshToken, null);
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<User?> ValidateTokenAsync(string token)
        {
            if (!_jwtGenerator.ValidateToken(token))
                return null;
            var userId = _jwtGenerator.GetUserIdFromToken(token);
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}
