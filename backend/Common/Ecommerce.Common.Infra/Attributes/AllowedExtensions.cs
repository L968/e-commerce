using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Common.Infra.Attributes;

/// <summary>
/// Validation attribute to check if the file extension of an IFormFile or IFormFileCollection is within the specified list of allowed extensions.
/// </summary>
public class AllowedExtensions(string[] extensions) : ValidationAttribute
{
    private readonly string[] _extensions = extensions;

    /// <summary>
    /// Validates if the file extension of the provided value (IFormFile or IFormFileCollection) is within the list of allowed extensions.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the value is valid or not.</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Value not provided");

        if (value is IFormFile file)
        {
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
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
                    var extension = Path.GetExtension(fileItem.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
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
    /// Gets the error message indicating the allowed extensions.
    /// </summary>
    /// <returns>The error message.</returns>
    private string GetErrorMessage()
    {
        return $"Only \"{string.Join(',', _extensions)}\" extensions are allowed";
    }
}
