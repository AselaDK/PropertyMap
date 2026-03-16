# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["PropertyMap.sln", "./"]
COPY ["PropertyMap.API/PropertyMap.API.csproj", "PropertyMap.API/"]
COPY ["PropertyMap.Application/PropertyMap.Application.csproj", "PropertyMap.Application/"]
COPY ["PropertyMap.Infrastructure/PropertyMap.Infrastructure.csproj", "PropertyMap.Infrastructure/"]
COPY ["PropertyMap.Core/PropertyMap.Core.csproj", "PropertyMap.Core/"]

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/PropertyMap.API"
RUN dotnet build "PropertyMap.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "PropertyMap.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Standard .NET port
EXPOSE 8080

ENTRYPOINT ["dotnet", "PropertyMap.API.dll"]
