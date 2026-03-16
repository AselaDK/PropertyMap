using System.ComponentModel.DataAnnotations;

namespace PropertyMap.Application.DTOs.Properties
{
    public class CreatePropertyDto
    {
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid ZIP code")]
        public string ZipCode { get; set; } = string.Empty;

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Required]
        public string PropertyType { get; set; } = string.Empty;

        [Required]
        [Range(0, 20)]
        public int Bedrooms { get; set; }

        [Required]
        [Range(0, 20)]
        public int Bathrooms { get; set; }

        [Required]
        [Range(0, 100000)]
        public double SquareFeet { get; set; }

        [Range(1800, 2100)]
        public int YearBuilt { get; set; }

        [Url]
        public string ImageUrl { get; set; } = string.Empty;
    }
}