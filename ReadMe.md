# Property Map — Backend

.NET 8 Web API for the Property Map app. Uses **Clean Architecture** and **PostgreSQL**.

---

## Features

- **Auth** — Login, refresh token, logout; JWT + HttpOnly refresh cookie.
- **Properties** — List, get by id, search (filters + nearby by lat/lng/radius); Admin CRUD.
- **Roles** — Public read for properties; Admin-only create/update/delete.
- **Infrastructure** — EF Core + Npgsql; schema creation and seeding in Infrastructure (no EF in API layer).

---

## Architecture (Clean Architecture)

- **Core** — Entities (`Property`, `User`), repository/service interfaces. No dependencies.
- **Application** — DTOs, AutoMapper profiles, app service interfaces (`IAuthenticationService`, `IPropertyManagementService`).
- **Infrastructure** — DbContext, repositories, JWT/password hashing, app service implementations, **database initializer and seeding** (all EF/schema logic here).
- **API** — Controllers, middleware (error handling, rate limiting), CORS, JWT config. No EF Core references.

---

## Run locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)

### Steps

1. **Connection string**  
   In `PropertyMap.API/appsettings.json`, set:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=propertymap;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

2. **Run the API**
   ```bash
   cd PropertyMap.API
   dotnet run
   ```

3. **Verify**
   - API: http://localhost:5038  
   - Swagger: http://localhost:5038/swagger  
   - Tables are created and users seeded on first run (see `DatabaseInitializer` in Infrastructure).

### Seeded users

| Role  | Username | Password |
|-------|----------|----------|
| Admin | admin    | demo123  |
| User  | demo     | demo123  |

---

## Project layout

```text
PropertyMap/
├── PropertyMap.Core/           # Entities, interfaces
├── PropertyMap.Application/   # DTOs, mappings, service interfaces
├── PropertyMap.Infrastructure/# DbContext, repos, auth, DatabaseInitializer, seeding
├── PropertyMap.API/            # Controllers, middleware, Program.cs
├── PropertyMap.sln
└── Dockerfile
```

More: [ARCHITECTURE.md](docs/ARCHITECTURE.md), [docs/API.md](docs/API.md), [docs/DEPLOYMENT.md](docs/DEPLOYMENT.md).
