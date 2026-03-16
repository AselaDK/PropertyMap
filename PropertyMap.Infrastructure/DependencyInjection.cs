using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyMap.Application.Interfaces;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Core.Interfaces.Services;
using PropertyMap.Infrastructure.Data;
using PropertyMap.Infrastructure.Repositories;
using PropertyMap.Infrastructure.Security;
using PropertyMap.Infrastructure.Services;

namespace PropertyMap.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database: PostgreSQL only (dev and prod)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Configuration
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Security
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Repositories (Core)
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Core domain services (implemented in Infrastructure)
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPropertyService, PropertyService>();

            // Application services (implemented in Infrastructure)
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPropertyManagementService, PropertyManagementService>();

            return services;
        }
    }
}
