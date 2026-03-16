using System.Collections.Generic;

namespace PropertyMap.Application.DTOs.Properties
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string FullAddress => $"{Address}, {City}, {State} {ZipCode}".Trim();
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PropertyType { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double SquareFeet { get; set; }
        public int YearBuilt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new();
        public bool IsAvailable { get; set; }
        public string FormattedPrice => $"${Price:N0}";
        public string FormattedSquareFeet => $"{SquareFeet:N0} sq ft";
    }
}