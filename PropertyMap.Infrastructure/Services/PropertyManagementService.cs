using AutoMapper;
using Microsoft.Extensions.Logging;
using PropertyMap.Application.DTOs.Properties;
using PropertyMap.Application.Interfaces;
using PropertyMap.Core.Entities;
using PropertyMap.Core.Interfaces.Repositories;
using PropertyMap.Core.Interfaces.Services;

namespace PropertyMap.Infrastructure.Services
{
    public class PropertyManagementService : IPropertyManagementService
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertyManagementService> _logger;

        public PropertyManagementService(
            IPropertyService propertyService,
            IPropertyRepository propertyRepository,
            IMapper mapper,
            ILogger<PropertyManagementService> logger)
        {
            _propertyService = propertyService;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
        {
            var properties = await _propertyService.GetAllPropertiesAsync();
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PropertyDto?> GetPropertyByIdAsync(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            return property != null ? _mapper.Map<PropertyDto>(property) : null;
        }

        public async Task<IEnumerable<PropertyDto>> SearchPropertiesAsync(PropertyFilterDto filter)
        {
            var properties = await _propertyService.SearchPropertiesAsync(
                filter.PropertyType,
                filter.MinPrice,
                filter.MaxPrice,
                filter.MinBedrooms,
                filter.City,
                filter.State);

            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<IEnumerable<PropertyDto>> GetNearbyPropertiesAsync(
            double latitude, double longitude, double radiusInKm)
        {
            var properties = await _propertyService.GetNearbyPropertiesAsync(latitude, longitude, radiusInKm);
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto, int userId)
        {
            var property = _mapper.Map<Property>(createDto);
            var created = await _propertyService.CreatePropertyAsync(property, userId);
            return _mapper.Map<PropertyDto>(created);
        }

        public async Task<PropertyDto> UpdatePropertyAsync(int id, CreatePropertyDto updateDto)
        {
            var existing = await _propertyService.GetPropertyByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Property with id {id} not found");

            _mapper.Map(updateDto, existing);
            var updated = await _propertyService.UpdatePropertyAsync(existing);
            return _mapper.Map<PropertyDto>(updated);
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            return await _propertyService.DeletePropertyAsync(id);
        }
    }
}