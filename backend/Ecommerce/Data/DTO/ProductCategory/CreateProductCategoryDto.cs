namespace Ecommerce.Data.DTO
{
    public record CreateProductCategoryDto
    {
        [Required]
        public string? Name { get; init; }

        [Required]
        public string? Description { get; init; }
    }
}