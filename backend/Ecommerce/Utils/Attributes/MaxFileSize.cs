namespace Ecommerce.Utils.Attributes
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Value not provided");

            var files = value as IFormFileCollection;

            foreach (IFormFile file in files)
            {
                if (file != null)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success!;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes";
        }
    }
}