using PropertyMap.API.Middleware;
using PropertyMap.Application;
using PropertyMap.Infrastructure;
using PropertyMap.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Enrich.FromLogContext()
          .WriteTo.Console();
});

// Add services to container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🔥 DEPENDENCY INJECTION - Clean Architecture Flow
builder.Services.AddApplication();      // Application layer (depends only on Core)
builder.Services.AddInfrastructure(builder.Configuration); // Infrastructure layer (depends on Core & Application)

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.WithOrigins(
                // "http://localhost:3000",
                // "https://localhost:3000",
                // "http://localhost:5173",
                // "https://localhost:5173",
                // "http://127.0.0.1:3000",
                // "https://127.0.0.1:3000",
                // "http://127.0.0.1:5173",
                // "https://127.0.0.1:5173",
                "https://property-map-viewer.vercel.app")
                // Allow Swagger / same-host requests
                // "https://localhost:7045",
                // "http://localhost:7045",
                // "http://localhost:5038",
                // "https://localhost:5038"
                //  )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure Authentication with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret is required"))),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactApp");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseRouting();

// Health check endpoint for Render
app.MapGet("/health", () => Results.Ok("Healthy"));

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

// Protect endpoints with rate limiting
app.UseMiddleware<RateLimitingMiddleware>();

app.MapControllers();

// Ensure database is created and seed demo user if empty (demo / demo123)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
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

            logger.LogInformation("Database connection successful. Running EnsureCreated...");
            await dbContext.Database.EnsureCreatedAsync();

            logger.LogInformation("EnsureCreated complete. Running seeder...");
            var passwordHasher = scope.ServiceProvider.GetRequiredService<PropertyMap.Infrastructure.Security.IPasswordHasher>();
            await DatabaseSeeder.SeedAsync(dbContext, passwordHasher);

            logger.LogInformation("Database initialized and seeded successfully.");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            logger.LogError(ex, "Database initialization failed. Retries left: {Retries}. Error: {Message}", retries, ex.Message);
            if (retries == 0)
                logger.LogCritical("All database initialization retries exhausted. App will run without a seeded database.");
            else
                await Task.Delay(5000);
        }
    }
}

// Support Render/Heroku port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
