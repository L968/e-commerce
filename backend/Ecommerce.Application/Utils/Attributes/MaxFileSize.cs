using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Utils.Attributes;

public class MaxFileSize(int maxFileSize) : ValidationAttribute
{
    private readonly int _maxFileSize = maxFileSize;

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

    public string GetErrorMessage()
    {
        return $"Maximum allowed file size is {_maxFileSize} bytes";
    }
}
