using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyMap.Core.Entities;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Infrastructure.Data;

namespace PropertyMap.Infrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Property>> SearchAsync(
            string? propertyType = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minBedrooms = null,
            string? city = null,
            string? state = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(propertyType))
                query = query.Where(p => p.PropertyType == propertyType);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (minBedrooms.HasValue)
                query = query.Where(p => p.Bedrooms >= minBedrooms.Value);

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.City.ToLower() == city.ToLower());

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(p => p.State.ToLower() == state.ToLower());

            return await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetNearbyPropertiesAsync(
            double latitude,
            double longitude,
            double radiusInKm)
        {
            // Approximate conversion: 1 degree latitude ≈ 111 km
            double latDelta = radiusInKm / 111.0;
            double lonDelta = radiusInKm / (111.0 * Math.Cos(latitude * Math.PI / 180));

            double minLat = latitude - latDelta;
            double maxLat = latitude + latDelta;
            double minLon = longitude - lonDelta;
            double maxLon = longitude + lonDelta;

            return await _dbSet
                .Where(p => p.Latitude >= minLat && p.Latitude <= maxLat &&
                           p.Longitude >= minLon && p.Longitude <= maxLon &&
                           p.IsAvailable)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByUserAsync(int userId)
        {
            return await _dbSet
                .Where(p => p.CreatedByUserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}