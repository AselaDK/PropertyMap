# Deployment Guide

How to deploy the backend to production. For **running locally**, see the [README](../README.md).

## Backend (Render)
*   **Source:** This repo (PropertyMap folder / Dockerfile).
*   **Environment:** Docker.
*   **Environment Variables:** `ConnectionStrings__DefaultConnection`, `JwtSettings__Secret` (min 32 chars), `JwtSettings__Issuer`, `JwtSettings__Audience`, `ASPNETCORE_ENVIRONMENT=Production`, `PORT=8080` (optional).
*   **Health:** `https://your-api.onrender.com/health`

## Database (Supabase / Neon)
*   Use **Connection Pooler** (Npgsql format) for Supabase.
*   Wrap password in single quotes if it contains `#`, `!`, or `;`.
