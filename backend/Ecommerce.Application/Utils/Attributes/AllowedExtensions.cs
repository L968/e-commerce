using Microsoft.AspNetCore.Http;

namespace Ecommerce.Utils.Attributes;

public class AllowedExtensions : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensions(string[] extensions)
    {
        _extensions = extensions;
    }

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

    public string GetErrorMessage()
    {
        return $"Only \"{string.Join(',', _extensions)}\" extensions are allowed";
    }
}