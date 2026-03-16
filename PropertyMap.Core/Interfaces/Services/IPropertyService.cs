using PropertyMap.Core.Entities;

namespace PropertyMap.Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByIdAsync(int id);
        Task<IEnumerable<Property>> SearchPropertiesAsync(
            string? propertyType = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minBedrooms = null,
            string? city = null,
            string? state = null);
        Task<IEnumerable<Property>> GetNearbyPropertiesAsync(
            double latitude,
            double longitude,
            double radiusInKm);
        Task<Property> CreatePropertyAsync(Property property, int userId);
        Task<Property> UpdatePropertyAsync(Property property);
        Task<bool> DeletePropertyAsync(int id);
    }
}