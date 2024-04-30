using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Common.Infra.Attributes;

/// <summary>
/// Validation attribute to check if the file size of an IFormFile or IFormFileCollection is less than or equal to the specified maximum file size.
/// </summary>
public class MaxFileSize(int maxFileSize) : ValidationAttribute
{
    private readonly int _maxFileSize = maxFileSize;

    /// <summary>
    /// Validates if the file size of the provided value (IFormFile or IFormFileCollection) is less than or equal to the maximum file size.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the value is valid or not.</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Value not provided");

        if (value is IFormFile file)
        {
            if (file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
        }
        else if (value is IFormFileCollection fileCollection)
        {
            foreach (IFormFile fileItem in fileCollection)
            {
                if (fileItem is not null)
                {
                    if (fileItem.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
        }
        else
        {
            return new ValidationResult("Invalid value type");
        }

        return ValidationResult.Success!;
    }

    /// <summary>
    /// Gets the error message indicating the maximum allowed file size.
    /// </summary>
    /// <returns>The error message.</returns>
    private string GetErrorMessage()
    {
        return $"Maximum allowed file size is {_maxFileSize} bytes";
    }
}
