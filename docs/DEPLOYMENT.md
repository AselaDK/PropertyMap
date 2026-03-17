# Deployment Guide (Backend)

How to deploy the backend to production. For **running locally**, see the [README](../README.md).  
**Do not add production secrets to the repo or to this doc** — use placeholders and set real values in Render.

---

## Render

- **Source:** This repo (PropertyMap folder / Dockerfile).
- **Environment:** Docker.

### Environment variables (Render Dashboard → Environment)

| Key | Description |
|-----|-------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string (Npgsql format or postgresql:// URI). |
| `JwtSettings__Secret` | JWT signing key (min 32 characters). Generate a secure value; do not commit. |
| `JwtSettings__Issuer` | JWT issuer (e.g. `propertymap-api`). |
| `JwtSettings__Audience` | JWT audience (e.g. `propertymap-client`). |
| `ASPNETCORE_ENVIRONMENT` | Set to `Production`. |
| `PORT` | Optional; e.g. `8080`. |

Use your own values; do not paste production secrets into docs.

### CORS

- Allowed origins are read from **appsettings.json** → `Cors:AllowedOrigins` in production (e.g. your Vercel URL only).
- To **override or add origins** on Render without code changes, add:
  - `Cors__AllowedOrigins__0` = your frontend URL (e.g. `https://your-app.vercel.app`)
  - `Cors__AllowedOrigins__1` = optional second origin  
  (Use `__` for nested keys and `__0`, `__1` for array indices.)

### Health check

- `https://your-service-name.onrender.com/health`

---

## Database (Supabase / Neon)

- Use **Connection Pooler** (Npgsql format) for Supabase.
- If the password contains `#`, `!`, or `;`, wrap it in single quotes: `Password='...'`.
- Do not commit real connection strings.
