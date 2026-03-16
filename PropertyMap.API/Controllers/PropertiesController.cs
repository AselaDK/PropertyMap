using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyMap.API.Extensions;
using PropertyMap.Application.DTOs.Properties;
using PropertyMap.Application.DTOs.Shared;
using PropertyMap.Application.Interfaces;

namespace PropertyMap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyManagementService _propertyManagementService;

        public PropertiesController(IPropertyManagementService propertyManagementService)
        {
            _propertyManagementService = propertyManagementService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PropertyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertyManagementService.GetAllPropertiesAsync();
            return Ok(ApiResponse<IEnumerable<PropertyDto>>.Ok(properties));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _propertyManagementService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound(ApiResponse<object>.Fail($"Property with id {id} not found"));

            return Ok(ApiResponse<PropertyDto>.Ok(property));
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProperties([FromQuery] PropertyFilterDto filter)
        {
            IEnumerable<PropertyDto> properties;

            if (filter.Latitude.HasValue && filter.Longitude.HasValue)
            {
                properties = await _propertyManagementService.GetNearbyPropertiesAsync(
                    filter.Latitude.Value,
                    filter.Longitude.Value,
                    filter.RadiusInKm ?? 10);
            }
            else
            {
                properties = await _propertyManagementService.SearchPropertiesAsync(filter);
            }

            return Ok(ApiResponse<IEnumerable<PropertyDto>>.Ok(properties));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto createDto)
        {
            var userId = User.GetUserId();
            if (!userId.HasValue)
                return Unauthorized(ApiResponse<object>.Fail("User not authenticated"));

            var property = await _propertyManagementService.CreatePropertyAsync(createDto, userId.Value);
            return CreatedAtAction(nameof(GetPropertyById), new { id = property.Id },
                ApiResponse<PropertyDto>.Ok(property, "Property created successfully"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] CreatePropertyDto updateDto)
        {
            var property = await _propertyManagementService.UpdatePropertyAsync(id, updateDto);
            return Ok(ApiResponse<PropertyDto>.Ok(property, "Property updated successfully"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var result = await _propertyManagementService.DeletePropertyAsync(id);
            if (!result)
                return NotFound(ApiResponse<object>.Fail($"Property with id {id} not found"));

            return Ok(ApiResponse<object>.Ok(null, "Property deleted successfully"));
        }
    }
}
