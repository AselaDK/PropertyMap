using PropertyMap.Core.Entities;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Core.Interfaces.Services;

namespace PropertyMap.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _propertyRepository.GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _propertyRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Property>> SearchPropertiesAsync(
            string? propertyType = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minBedrooms = null,
            string? city = null,
            string? state = null,
            int pageNumber = 1,
            int pageSize = 5)
        {
            return await _propertyRepository.SearchAsync(propertyType, minPrice, maxPrice, minBedrooms, city, state, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Property>> GetNearbyPropertiesAsync(double latitude, double longitude, double radiusInKm)
        {
            return await _propertyRepository.GetNearbyPropertiesAsync(latitude, longitude, radiusInKm);
        }

        public async Task<Property> CreatePropertyAsync(Property property, int userId)
        {
            property.CreatedByUserId = userId;
            return await _propertyRepository.AddAsync(property);
        }

        public async Task<Property> UpdatePropertyAsync(Property property)
        {
            await _propertyRepository.UpdateAsync(property);
            return property;
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            if (property == null) return false;
            await _propertyRepository.DeleteAsync(property);
            return true;
        }
    }
}
