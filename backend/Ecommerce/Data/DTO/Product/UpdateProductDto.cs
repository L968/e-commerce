namespace Ecommerce.Data.DTO
{
    public record UpdateProductDto
    {
        [Required]
        public string? Name { get; init; }

        [Required]
        public string? Description { get; init; }

        [Required]
        public string? Sku { get; init; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public decimal? Price { get; init; }

        [Required]
        public bool? Active { get; init; }

        [Required]
        public bool? Visible { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public float? Length { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public float? Width { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public float? Height { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public float? Weight { get; init; }

        [Required]
        public int? ProductCategoryId { get; init; }
    }
}