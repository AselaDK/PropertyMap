using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PropertyMap.Application.DTOs.Auth;
using PropertyMap.Application.Interfaces;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Core.Interfaces.Services;
using PropertyMap.Infrastructure.Security;

namespace PropertyMap.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            IAuthService authService,
            IUserRepository userRepository,
            IJwtGenerator jwtGenerator,
            IMapper mapper,
            ILogger<AuthenticationService> logger,
            IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto.Username, loginDto.Password);

            if (!result.success || result.user == null)
                throw new UnauthorizedAccessException(result.error ?? "Invalid credentials");

            return new LoginResponseDto
            {
                Token = result.token!,
                RefreshToken = result.refreshToken!,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = _mapper.Map<UserDto>(result.user)
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string token, string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(token, refreshToken);

            if (!result.success)
                throw new UnauthorizedAccessException(result.error ?? "Invalid refresh token");

            var userId = _jwtGenerator.GetUserIdFromToken(token);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            return new LoginResponseDto
            {
                Token = result.token!,
                RefreshToken = result.refreshToken!,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<bool> RevokeTokenAsync(int userId)
        {
            return await _authService.LogoutAsync(userId);
        }

        public async Task<UserDto?> GetCurrentUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }
    }
}