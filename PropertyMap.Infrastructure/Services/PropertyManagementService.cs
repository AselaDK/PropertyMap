using AutoMapper;
using Microsoft.Extensions.Logging;
using PropertyMap.Application.DTOs.Properties;
using PropertyMap.Application.DTOs.Shared;
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

        public async Task<PagedResponse<PropertyDto>> GetAllPropertiesAsync(int pageNumber, int pageSize)
        {
            var properties = await _propertyService.GetAllPropertiesAsync(pageNumber, pageSize);
            var totalCount = await _propertyRepository.CountAsync();
            
            var dtos = _mapper.Map<IEnumerable<PropertyDto>>(properties);
            return new PagedResponse<PropertyDto>(dtos, totalCount, pageNumber, pageSize);
        }

        public async Task<PropertyDto?> GetPropertyByIdAsync(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            return property != null ? _mapper.Map<PropertyDto>(property) : null;
        }

        public async Task<PagedResponse<PropertyDto>> SearchPropertiesAsync(PropertyFilterDto filter)
        {
            var properties = await _propertyService.SearchPropertiesAsync(
                filter.PropertyType,
                filter.MinPrice,
                filter.MaxPrice,
                filter.MinBedrooms,
                filter.City,
                filter.State,
                filter.PageNumber,
                filter.PageSize);

            // For search total count, we should ideally count with the same filters.
            // Let's add a CountSearchAsync to repository if needed, or for now just count total properties.
            // To be accurate, we need filtered count.
            var totalCount = await _propertyRepository.CountAsync(p => 
                (string.IsNullOrWhiteSpace(filter.PropertyType) || p.PropertyType == filter.PropertyType) &&
                (!filter.MinPrice.HasValue || p.Price >= filter.MinPrice.Value) &&
                (!filter.MaxPrice.HasValue || p.Price <= filter.MaxPrice.Value) &&
                (!filter.MinBedrooms.HasValue || p.Bedrooms >= filter.MinBedrooms.Value) &&
                (string.IsNullOrWhiteSpace(filter.City) || p.City.ToLower() == filter.City.ToLower()) &&
                (string.IsNullOrWhiteSpace(filter.State) || p.State.ToLower() == filter.State.ToLower())
            );

            var dtos = _mapper.Map<IEnumerable<PropertyDto>>(properties);
            return new PagedResponse<PropertyDto>(dtos, totalCount, filter.PageNumber, filter.PageSize);
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