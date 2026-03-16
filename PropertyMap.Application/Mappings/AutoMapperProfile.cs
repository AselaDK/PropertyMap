using AutoMapper;
using PropertyMap.Application.DTOs.Auth;
using PropertyMap.Application.DTOs.Properties;
using PropertyMap.Core.Entities;

namespace PropertyMap.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();

            // Property mappings
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.ImageUrls,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.ImageUrl)
                            ? new List<string> { src.ImageUrl }
                            : new List<string>()));

            CreateMap<CreatePropertyDto, Property>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrl ?? string.Empty))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());
        }
    }
}