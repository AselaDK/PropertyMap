# API Reference

REST API for Property Map. All requests and responses use `application/json`. For **running the backend locally**, see the [README](../README.md).

## Base URL
- **Local:** `http://localhost:5038/api`
- **Swagger UI (local):** `http://localhost:5038/swagger`
- **Production:** Your Render (or other) host URL + `/api`

---

## Authentication (`/auth`)

### 1. Login
*   **Method:** `POST /api/auth/login`
*   **Request Body:** `{ "username": "demo", "password": "demo123" }`
*   **Response:** JWT in body; Refresh Token in HttpOnly cookie.

### 2. Refresh Token
*   **Method:** `POST /api/auth/refresh`
*   **Request Body:** `{ "token": "...", "refreshToken": "..." }`

### 3. Current User
*   **Method:** `GET /api/auth/me`
*   **Auth:** Bearer token required.

---

## Properties (`/properties`)

### List / Search (public)
*   `GET /api/properties` — list all.
*   `GET /api/properties/search` — query params: `city`, `minPrice`, `maxPrice`, `propertyType`, `minBedrooms`, `latitude`, `longitude`, `radiusInKm`.

### By id (public)
*   `GET /api/properties/{id}`

### Admin only
*   `POST /api/properties` — create (body: `CreatePropertyDto`).
*   `PUT /api/properties/{id}` — update.
*   `DELETE /api/properties/{id}` — delete.
