using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PropertyMap.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            var retries = 5;

            while (retries > 0)
            {
                try
                {
                    logger.LogInformation("Attempting database initialization (retries left: {Retries})...", retries);
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var canConnect = await dbContext.Database.CanConnectAsync();
                    if (!canConnect)
                    {
                        logger.LogWarning("Cannot connect to database. Retrying in 5 seconds...");
                        await Task.Delay(5000);
                        retries--;
                        continue;
                    }

                    // Check if OUR tables exist specifically.
                    // EnsureCreatedAsync is unreliable on Supabase because it sees Supabase's
                    // own internal tables and skips creation, thinking the DB is already set up.
                    var connection = dbContext.Database.GetDbConnection();
                    await connection.OpenAsync();
                    using var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'Users'";
                    var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    await connection.CloseAsync();

                    if (count == 0)
                    {
                        logger.LogInformation("Tables not found. Generating schema from EF Core model...");
                        var script = dbContext.Database.GenerateCreateScript();
                        await dbContext.Database.ExecuteSqlRawAsync(script);
                        logger.LogInformation("Schema created successfully.");
                    }
                    else
                    {
                        logger.LogInformation("Tables already exist. Skipping schema creation.");
                    }

                    var passwordHasher = scope.ServiceProvider.GetRequiredService<Security.IPasswordHasher>();
                    await DatabaseSeeder.SeedAsync(dbContext, passwordHasher);

                    logger.LogInformation("Database initialized and seeded successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    retries--;
                    logger.LogError(ex, "Database initialization failed. Retries left: {Retries}. Error: {Message}", retries, ex.Message);
                    if (retries == 0)
                        logger.LogCritical("All database initialization retries exhausted.");
                    else
                        await Task.Delay(5000);
                }
            }
        }
    }
}
