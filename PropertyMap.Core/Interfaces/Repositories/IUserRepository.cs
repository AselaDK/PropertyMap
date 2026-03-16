using PropertyMap.Core.Entities;

namespace PropertyMap.Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);
        Task UpdateLoginAttemptsAsync(int userId, bool successful);
        Task<User?> GetUserWithRefreshTokenAsync(string refreshToken);
    }
}