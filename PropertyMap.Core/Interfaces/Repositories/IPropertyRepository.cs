using PropertyMap.Core.Entities;

namespace PropertyMap.Core.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<IEnumerable<Property>> SearchAsync(
            string? propertyType = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minBedrooms = null,
            string? city = null,
            string? state = null,
            int pageNumber = 1,
            int pageSize = 5);

        Task<IEnumerable<Property>> GetNearbyPropertiesAsync(
            double latitude,
            double longitude,
            double radiusInKm);

        Task<IEnumerable<Property>> GetPropertiesByUserAsync(int userId);
    }
}