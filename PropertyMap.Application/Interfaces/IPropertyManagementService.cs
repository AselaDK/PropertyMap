using PropertyMap.Application.DTOs.Properties;

namespace PropertyMap.Application.Interfaces
{
    public interface IPropertyManagementService
    {
        Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
        Task<PropertyDto?> GetPropertyByIdAsync(int id);
        Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(PropertyFilterDto filter);
        Task<IEnumerable<PropertyDto>> GetNearbyPropertiesAsync(double latitude, double longitude, double radiusInKm);
        Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto, int userId);
        Task<PropertyDto> UpdatePropertyAsync(int id, CreatePropertyDto updateDto);
        Task<bool> DeletePropertyAsync(int id);
    }
}