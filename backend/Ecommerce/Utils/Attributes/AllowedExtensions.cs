namespace Ecommerce.Utils.Attributes
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensions(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Value not provided");

            var files = value as IFormFileCollection;

            foreach (IFormFile file in files)
            {
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success!;
        }

        public string GetErrorMessage()
        {
            return $"Only \"{string.Join(',', _extensions)}\" extensions are allowed";
        }
    }
}