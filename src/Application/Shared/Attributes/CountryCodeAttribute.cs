using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Shared.Attributes;

public class CountryCodeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Country Code is required.");
        }

        var countryCode = value.ToString()!.Trim().ToLowerInvariant();

        if (countryCode.Length != 2)
        {
            return new ValidationResult("Country Code must be exactly 2 characters due that we are working with ISO-3166 ALPHA-2 ");
        }

        if (!countryCode.All(char.IsLetter))
        {
            return new ValidationResult("Country Code must contain only letters.");
        }

        return ValidationResult.Success!;
    }
}
