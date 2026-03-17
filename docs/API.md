# API Reference

The Property Map API follows standard REST practices. All requests and responses use `application/json`.

## Base URL
- **Local:** `http://localhost:5038/api`
- **Swagger UI:** `http://localhost:5038/swagger`

---

## 🔐 Authentication (`/auth`)

### 1. Login
*   **Method:** `POST /api/auth/login`
*   **Request Body:**
    ```json
    { "username": "demo", "password": "demo123" }
    ```
*   **Response:** Returns a JWT Access Token in the JSON body and a Refresh Token in an HttpOnly cookie.

### 2. Refresh Token
*   **Method:** `POST /api/auth/refresh`
*   **Request Body:**
    ```json
    { "token": "expired_access_token", "refreshToken": "refresh_token" }
    ```
*   **Response:** Returns a new Access Token.

### 3. Current User
*   **Method:** `GET /api/auth/me`
*   **Auth Required:** Yes (Bearer Token)
*   **Response:** Returns the current logged-in user profile.

---

## 🗺️ Properties (`/properties`)

### 1. List All Properties
*   **Method:** `GET /api/properties`
*   **Auth Required:** No (Public)
*   **Response:** Array of all active property listings.

### 2. Search Properties
*   **Method:** `GET /api/properties/search`
*   **Query Params:** `city`, `minPrice`, `maxPrice`, `propertyType`, `minBedrooms`, `latitude`, `longitude`, `radiusInKm`.
*   **Auth Required:** No (Public)
*   **Response:** Filtered property list.

### 3. Create Property
*   **Method:** `POST /api/properties`
*   **Auth Required:** Yes (Role: **Admin**)
*   **Request Body:** `CreatePropertyDto`.

### 4. Delete Property
*   **Method:** `DELETE /api/properties/{id}`
*   **Auth Required:** Yes (Role: **Admin**)
