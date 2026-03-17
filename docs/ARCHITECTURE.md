# Architecture & design (backend)

Backend follows **Clean Architecture**. For **running locally**, see the [README](../README.md).

## Layers

- **Core** — Entities (`Property`, `User`), repository and service interfaces. No dependencies.
- **Application** — DTOs, AutoMapper, application service interfaces. Depends only on Core.
- **Infrastructure** — DbContext, EF, repositories, JWT/password hashing, **database initializer and seeding**. Implements Core and Application. All EF/schema logic lives here.
- **API** — Controllers, middleware (error handling, rate limiting), CORS, JWT config. No EF Core references.

## Security

- JWT (short-lived) + refresh tokens (HttpOnly cookie).
- CORS and rate limiting.
- Passwords hashed with salt (e.g. PBKDF2) in Infrastructure.

For full-stack architecture and frontend design, see the root **ARCHITECTURE.md** in the solution.
