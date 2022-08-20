namespace Ecommerce.Data.DTO
{
    public record UpdateProductCategoryDto
    {
        [Required]
        public string? Name { get; init; }

        [Required]
        public string? Description { get; init; }
    }
}