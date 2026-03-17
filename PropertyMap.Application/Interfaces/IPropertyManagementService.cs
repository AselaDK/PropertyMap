using PropertyMap.Application.DTOs.Properties;
using PropertyMap.Application.DTOs.Shared;

namespace PropertyMap.Application.Interfaces
{
    public interface IPropertyManagementService
    {
        Task<PagedResponse<PropertyDto>> GetAllPropertiesAsync(int pageNumber, int pageSize);
        Task<PropertyDto?> GetPropertyByIdAsync(int id);
        Task<PagedResponse<PropertyDto>> SearchPropertiesAsync(PropertyFilterDto filter);
        Task<IEnumerable<PropertyDto>> GetNearbyPropertiesAsync(double latitude, double longitude, double radiusInKm);
        Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto, int userId);
        Task<PropertyDto> UpdatePropertyAsync(int id, CreatePropertyDto updateDto);
        Task<bool> DeletePropertyAsync(int id);
    }
}