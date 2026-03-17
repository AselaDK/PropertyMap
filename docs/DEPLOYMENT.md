# Deployment Guide

This project is optimized for deployment on **Render (Backend)** and **Vercel (Frontend)**.

## 1. Backend (Render)
*   **Source:** `PropertyMap` folder.
*   **Environment:** Docker (Render will automatically detect the `Dockerfile`).
*   **Environment Variables:**
    *   `ConnectionStrings__DefaultConnection`: (PostgreSQL Key=Value or postgresql:// URI)
    *   `JwtSettings__Secret`: A long secret key (min 32 chars).
    *   `ASPNETCORE_ENVIRONMENT`: `Production`
    *   `PORT`: `8080` (Optional but recommended)
*   **Health Check:** `https://your-api.onrender.com/health` (The backend has a built-in `/health` endpoint).

## 2. Frontend (Vercel)
*   **Source:** `property-map-viewer` folder.
*   **Build Settings:**
    *   **Build Command:** `npm run build`
    *   **Output Directory:** `dist`
*   **Environment Variables:**
    *   `VITE_API_URL`: `https://your-api-onrender.com/api` (Remember the `/api` at the end!)
*   **Redeploy:** If you change the `VITE_API_URL`, you must trigger a **Manual Redeploy** to bake the new URL into the build.

## 3. Database (Supabase / Neon)
*   **Supabase:** Use the **Connection Pooler (Npgsql format)** string.
*   **Neon:** Standard connection string works.
*   **Crucial:** Wrap the password in single quotes (`Password='...'`) if it has special characters like `#`, `!`, or `;`.
