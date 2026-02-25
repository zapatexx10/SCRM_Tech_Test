using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Shared.Attributes;

public class LanguageCodeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Language code is required.");
        }

        var languageCode = value.ToString()!.Trim().ToLowerInvariant();

        if (languageCode.Length != 2)
        {
            return new ValidationResult("Language code must be exactly 2 characters.");
        }

        if (!languageCode.All(char.IsLetter))
        {
            return new ValidationResult("Language code must contain only letters.");
        }

        return ValidationResult.Success!;
    }
}
