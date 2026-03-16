using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMap.Core.Entities;

namespace PropertyMap.Infrastructure.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.State)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ZipCode)
                .HasMaxLength(20);

            builder.Property(p => p.Latitude)
                .IsRequired();

            builder.Property(p => p.Longitude)
                .IsRequired();

            builder.Property(p => p.PropertyType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            // Seed sample data
            builder.HasData(
                new Property
                {
                    Id = 1,
                    Title = "Modern Downtown Loft",
                    Description = "Beautiful modern loft in the heart of downtown with amazing city views",
                    Price = 450000m,
                    Address = "123 Main St",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001",
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                    PropertyType = "Apartment",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    SquareFeet = 1200,
                    YearBuilt = 2015,
                    ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = 2,
                    Title = "Suburban Family Home",
                    Description = "Spacious family home with large backyard and swimming pool",
                    Price = 650000m,
                    Address = "456 Oak Ave",
                    City = "Los Angeles",
                    State = "CA",
                    ZipCode = "90001",
                    Latitude = 34.0522,
                    Longitude = -118.2437,
                    PropertyType = "House",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    SquareFeet = 2500,
                    YearBuilt = 2008,
                    ImageUrl = "https://images.unsplash.com/photo-1568605114967-8130f3a36994",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = 3,
                    Title = "Waterfront Condo",
                    Description = "Luxury condo with stunning water views and private balcony",
                    Price = 850000m,
                    Address = "789 Beach Dr",
                    City = "Miami",
                    State = "FL",
                    ZipCode = "33101",
                    Latitude = 25.7617,
                    Longitude = -80.1918,
                    PropertyType = "Condo",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    SquareFeet = 1800,
                    YearBuilt = 2020,
                    ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}