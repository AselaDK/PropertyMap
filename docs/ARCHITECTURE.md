# Architecture & Design

## 🏗️ Backend: Clean Architecture

The backend follows **Clean Architecture** (Onion Architecture), emphasizing independence of frameworks and UI.

### 1. **Core Layer**
*   **Entities:** Domain objects (`Property`, `User`, `BaseEntity`).
*   **Interfaces:** Definitions for repositories and core services.
*   **Business Logic:** Domain-specific logic, such as nearby property calculations.

### 2. **Application Layer**
*   **DTOs:** Data Transfer Objects to isolate internal models from external ones.
*   **AutoMapper:** Configuration for mapping between entities and DTOs.
*   **Service Interfaces:** Definitions for higher-level application services.

### 3. **Infrastructure Layer**
*   **Data:** Implements EF Core `DbContext` and PostgreSQL configuration.
*   **Repositories:** Concrete implementations of data access interfaces.
*   **Security:** Handles JWT token generation, refresh tokens, and password hashing (BCrypt-style).
*   **Services:** Concrete implementations of application services.

### 4. **API (Presentation) Layer**
*   **Controllers:** REST API endpoints.
*   **Middleware:** Custom error handling, authentication, and rate limiting.
*   **Configuration:** `Program.cs`, `appsettings.json`.

---

## 🏗️ Frontend: React & Component-Based Design

The frontend is structured to be modular and scalable.

### 1. **Layout & Components**
*   **Common Components:** Reusable UI components (Buttons, Inputs, Modals).
*   **Layout Components:** Page wrappers (Header, Footer, Sidebar).
*   **Map Integration:** Uses `react-leaflet` to interact with the map.

### 2. **State Management**
*   **Context API:** Used for authentication state and user sessions.
*   **Custom Hooks:** Encapsulate business logic (e.g., `useProperties`, `useAuth`).

### 3. **Service Layer**
*   **API Client:** A centralized `axios` instance with interceptors.
*   **Services:** Methods for interacting with the backend API.

---

## 🔐 Security Features

*   **JWT Authentication:** Short-lived access tokens (15 minutes).
*   **Refresh Tokens:** Stored in **HttpOnly, Secure, SameSite=Strict** cookies to prevent XSS attacks.
*   **CORS:** Strictly configured to allow only trusted origins.
*   **Rate Limiting:** Protects endpoints from brute-force attacks.
*   **Password Hashing:** Passwords are hashed with salt using HMAC-SHA256 (or similar secure algorithms).

---

## 🗺️ Map Implementation

The application uses **Leaflet.js** for its interactive mapping capabilities.

*   **Markers:** Properties are shown as custom markers on the map.
*   **Popups:** Clicking a marker opens a popup with property details and a link to the detail page.
*   **Clustering:** (Optional) Support for clustering markers when many properties are present.
*   **Search by Location:** Users can search for properties by city or nearby coordinates.
