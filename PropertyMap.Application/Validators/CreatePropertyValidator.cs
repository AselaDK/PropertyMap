using FluentValidation;
using PropertyMap.Application.DTOs.Properties;

namespace PropertyMap.Application.Validators
{
    public class CreatePropertyValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required")
                .Length(2).WithMessage("State must be 2 characters");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("ZIP code is required")
                .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid ZIP code format");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.Bedrooms)
                .InclusiveBetween(0, 20).WithMessage("Bedrooms must be between 0 and 20");

            RuleFor(x => x.Bathrooms)
                .InclusiveBetween(0, 20).WithMessage("Bathrooms must be between 0 and 20");

            RuleFor(x => x.SquareFeet)
                .InclusiveBetween(0, 100000).WithMessage("Square feet must be between 0 and 100,000");

            RuleFor(x => x.YearBuilt)
                .InclusiveBetween(1800, DateTime.Now.Year + 1)
                .WithMessage($"Year built must be between 1800 and {DateTime.Now.Year + 1}");
        }
    }
}