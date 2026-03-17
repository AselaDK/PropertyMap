# Property Map Viewer

A full-stack application for viewing and managing properties on an interactive map. Built with **.NET 8** and **React**.

## 🏗️ Architecture

### Backend (.NET 8) - Clean Architecture
The backend follows the principles of **Clean Architecture** (Onion Architecture) to ensure separation of concerns, testability, and independence from external frameworks.

*   **Core:** Contains domain entities (`Property`, `User`), interfaces, and business logic. No external dependencies.
*   **Application:** Contains DTOs, mapping profiles (AutoMapper), service interfaces, and application-specific logic.
*   **Infrastructure:** Implements the core interfaces. Handles data access (EF Core + PostgreSQL), security (JWT, Password Hashing), and external services.
*   **API (Presentation):** The entry point of the application. Contains Controllers, Middlewares (Error Handling, Rate Limiting), and configuration.

### Frontend (React + Vite)
The frontend is a modern React application built for speed and developer experience.

*   **Vite:** Fast build tool and development server.
*   **Leaflet:** Interactive map library used to visualize properties.
*   **Tailwind CSS:** Utility-first CSS framework for responsive design.
*   **Axios:** Configured with interceptors for automatic JWT handling and token refresh.

---

## 🚀 Features

*   **Interactive Map:** View properties with custom markers and popups.
*   **Property Search:** Filter properties by city, price, type, and bedrooms.
*   **Nearby Search:** Find properties within a specific radius of a location.
*   **Authentication:** Secure login with JWT and refresh token mechanism (HttpOnly cookies).
*   **Admin Dashboard:** Admins can Create, Update, and Delete properties.
*   **Responsive UI:** Fully functional on mobile and desktop devices.

---

## 🛠️ Local Setup

### Prerequisites
*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js (v20+)](https://nodejs.org/)
*   [PostgreSQL](https://www.postgresql.org/) (Running locally or via Docker)

### 1. Database Setup
Ensure you have a PostgreSQL database running. You can use the following connection string format in your `appsettings.json`:
`Host=localhost;Port=5432;Database=propertymap;Username=postgres;Password=your_password`

### 2. Backend Setup
1.  Navigate to the backend directory: `cd PropertyMap/PropertyMap.API`
2.  Update `appsettings.json` with your database credentials.
3.  Run the application: `dotnet run`
    *   The API will automatically create the database and seed it with demo users.
    *   Default API URL: `http://localhost:5038/api`

### 3. Frontend Setup
1.  Navigate to the frontend directory: `cd property-map-viewer`
2.  Install dependencies: `npm install`
3.  Create a `.env` file based on `.env.example`:
    ```env
    VITE_API_URL=http://localhost:5038/api
    ```
4.  Run the development server: `npm run dev`
    *   Default URL: `http://localhost:3000`

---

## 🐳 Running with Docker
You can run the entire stack (Database, API, and Frontend) using Docker Compose:

```bash
docker-compose up --build
```
*   Frontend: `http://localhost:3000`
*   Backend API: `http://localhost:5038/api`
*   PostgreSQL: `localhost:5432`

---

## 🔐 Credentials (Seeded)

| Role  | Username | Password |
|-------|----------|----------|
| Admin | admin    | demo123  |
| User  | demo     | demo123  |

---

## 📡 API Documentation
Once the backend is running, you can explore the API using Swagger:
`http://localhost:5038/swagger`
