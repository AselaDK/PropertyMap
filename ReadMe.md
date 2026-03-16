property-map-viewer/
├── .github/
│   └── workflows/
│       ├── backend-ci.yml
│       └── frontend-ci.yml
│
├── backend/
│   ├── PropertyMap.Core/                      # DOMAIN LAYER (No Dependencies)
│   │   ├── Entities/
│   │   │   ├── BaseEntity.cs
│   │   │   ├── Property.cs
│   │   │   └── User.cs
│   │   ├── Enums/
│   │   │   ├── PropertyType.cs
│   │   │   └── UserRole.cs
│   │   ├── Interfaces/
│   │   │   ├── Repositories/
│   │   │   │   ├── IGenericRepository.cs
│   │   │   │   ├── IPropertyRepository.cs
│   │   │   │   └── IUserRepository.cs
│   │   │   └── Services/
│   │   │       ├── IAuthService.cs
│   │   │       └── IPropertyService.cs
│   │   ├── Specifications/
│   │   │   ├── ISpecification.cs
│   │   │   ├── BaseSpecification.cs
│   │   │   └── PropertySpecifications.cs
│   │   └── PropertyMap.Core.csproj
│   │
│   ├── PropertyMap.Application/                # APPLICATION LAYER (Depends only on Core)
│   │   ├── DTOs/
│   │   │   ├── Auth/
│   │   │   │   ├── LoginDto.cs
│   │   │   │   ├── LoginResponseDto.cs
│   │   │   │   └── UserDto.cs
│   │   │   ├── Properties/
│   │   │   │   ├── PropertyDto.cs
│   │   │   │   ├── CreatePropertyDto.cs
│   │   │   │   └── PropertyFilterDto.cs
│   │   │   └── Shared/
│   │   │       ├── ApiResponse.cs
│   │   │       └── PagedResponse.cs
│   │   ├── Interfaces/
│   │   │   ├── IAuthenticationService.cs       # Application-level service interfaces
│   │   │   └── IPropertyManagementService.cs
│   │   ├── Mappings/
│   │   │   └── AutoMapperProfile.cs
│   │   ├── Validators/
│   │   │   ├── CreatePropertyValidator.cs
│   │   │   └── LoginValidator.cs
│   │   ├── Features/
│   │   │   ├── Auth/
│   │   │   │   ├── Commands/
│   │   │   │   │   └── LoginCommand.cs
│   │   │   │   └── Queries/
│   │   │   │       └── GetCurrentUserQuery.cs
│   │   │   └── Properties/
│   │   │       ├── Commands/
│   │   │       │   └── CreatePropertyCommand.cs
│   │   │       └── Queries/
│   │   │           ├── GetAllPropertiesQuery.cs
│   │   │           ├── GetPropertyByIdQuery.cs
│   │   │           └── SearchPropertiesQuery.cs
│   │   ├── DependencyInjection.cs
│   │   └── PropertyMap.Application.csproj
│   │
│   ├── PropertyMap.Infrastructure/             # INFRASTRUCTURE LAYER (Depends on Core & Application)
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── PropertyConfiguration.cs
│   │   │   │   └── UserConfiguration.cs
│   │   │   └── Migrations/
│   │   ├── Repositories/                       # Implements Core.Interfaces.Repositories
│   │   │   ├── GenericRepository.cs
│   │   │   ├── PropertyRepository.cs
│   │   │   └── UserRepository.cs
│   │   ├── Security/
│   │   │   ├── JwtSettings.cs
│   │   │   ├── IJwtGenerator.cs
│   │   │   ├── JwtGenerator.cs
│   │   │   ├── IPasswordHasher.cs
│   │   │   └── PasswordHasher.cs
│   │   ├── Services/                            # Implements Application.Interfaces
│   │   │   ├── AuthenticationService.cs         # Implements IAuthenticationService
│   │   │   └── PropertyManagementService.cs     # Implements IPropertyManagementService
│   │   ├── DependencyInjection.cs
│   │   └── PropertyMap.Infrastructure.csproj
│   │
│   ├── PropertyMap.API/                         # PRESENTATION LAYER (Depends on Application & Infrastructure)
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   └── PropertiesController.cs
│   │   ├── Middleware/
│   │   │   ├── ErrorHandlingMiddleware.cs
│   │   │   └── JwtMiddleware.cs
│   │   ├── Extensions/
│   │   │   └── ClaimsPrincipalExtensions.cs
│   │   ├── appsettings.json
│   │   ├── Program.cs
│   │   ├── PropertyMap.API.csproj
│   │   └── Dockerfile
│   │
│   ├── PropertyMap.sln
│   └── docker-compose.yml
│
├── frontend/
│   ├── public/
│   │   ├── index.html
│   │   ├── favicon.ico
│   │   └── manifest.json
│   │
│   ├── src/
│   │   ├── assets/
│   │   │   ├── images/
│   │   │   ├── icons/
│   │   │   └── styles/
│   │   │       └── tailwind.css
│   │   │
│   │   ├── components/
│   │   │   ├── common/
│   │   │   │   ├── Button.tsx
│   │   │   │   ├── Input.tsx
│   │   │   │   ├── Modal.tsx
│   │   │   │   ├── Spinner.tsx
│   │   │   │   └── ErrorBoundary.tsx
│   │   │   │
│   │   │   ├── layout/
│   │   │   │   ├── Header.tsx
│   │   │   │   ├── Footer.tsx
│   │   │   │   └── Sidebar.tsx
│   │   │   │
│   │   │   ├── map/
│   │   │   │   ├── MapView.tsx
│   │   │   │   ├── MapMarker.tsx
│   │   │   │   ├── MapControls.tsx
│   │   │   │   └── PropertyPopup.tsx
│   │   │   │
│   │   │   ├── properties/
│   │   │   │   ├── PropertyCard.tsx
│   │   │   │   ├── PropertyList.tsx
│   │   │   │   ├── PropertyDetails.tsx
│   │   │   │   └── PropertyFilters.tsx
│   │   │   │
│   │   │   ├── auth/
│   │   │   │   ├── LoginForm.tsx
│   │   │   │   ├── ProtectedRoute.tsx
│   │   │   │   └── AuthContext.tsx
│   │   │   │
│   │   │   └── ui/
│   │   │       ├── Alert.tsx
│   │   │       ├── Badge.tsx
│   │   │       └── Card.tsx
│   │   │
│   │   ├── pages/
│   │   │   ├── Login.tsx
│   │   │   ├── Dashboard.tsx
│   │   │   ├── PropertyDetail.tsx
│   │   │   └── NotFound.tsx
│   │   │
│   │   ├── services/
│   │   │   ├── api/
│   │   │   │   ├── axiosConfig.ts
│   │   │   │   ├── authApi.ts
│   │   │   │   └── propertyApi.ts
│   │   │   ├── auth.service.ts
│   │   │   └── property.service.ts
│   │   │
│   │   ├── hooks/
│   │   │   ├── useAuth.ts
│   │   │   ├── useProperties.ts
│   │   │   └── useMap.ts
│   │   │
│   │   ├── store/
│   │   │   ├── authSlice.ts
│   │   │   ├── propertySlice.ts
│   │   │   └── index.ts
│   │   │
│   │   ├── types/
│   │   │   ├── property.ts
│   │   │   ├── user.ts
│   │   │   └── api.ts
│   │   │
│   │   ├── utils/
│   │   │   ├── constants.ts
│   │   │   ├── formatters.ts
│   │   │   └── validators.ts
│   │   │
│   │   ├── config/
│   │   │   └── environment.ts
│   │   │
│   │   ├── App.tsx
│   │   ├── index.tsx
│   │   └── routes.tsx
│   │
│   ├── .env
│   ├── .env.example
│   ├── .eslintrc.js
│   ├── .prettierrc
│   ├── tailwind.config.js
│   ├── tsconfig.json
│   ├── package.json
│   └── Dockerfile
│
├── database/
│   ├── init.sql
│   ├── seed.sql
│   └── Dockerfile
│
├── docs/
│   ├── API.md
│   ├── DEPLOYMENT.md
│   └── ARCHITECTURE.md
│
├── .gitignore
├── .dockerignore
├── docker-compose.yml
├── docker-compose.prod.yml
└── README.md