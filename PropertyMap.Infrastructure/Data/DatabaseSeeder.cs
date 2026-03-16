using Microsoft.EntityFrameworkCore;
using PropertyMap.Core.Entities;

namespace PropertyMap.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, Security.IPasswordHasher passwordHasher)
        {
            if (await dbContext.Users.AnyAsync())
                return;

            var hash = passwordHasher.HashPassword("demo123", out string salt);
            dbContext.Users.Add(new User
            {
                Username = "demo",
                Email = "demo@example.com",
                PasswordHash = hash,
                Salt = salt,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
