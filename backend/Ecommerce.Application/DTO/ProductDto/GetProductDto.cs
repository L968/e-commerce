namespace Ecommerce.Application.DTO.ProductDto;

public record GetProductDto
{
    [Key]
    public int? Id { get; set; }

    [Required]
    public Guid? Guid { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Sku { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Only positive numbers allowed")]
    public decimal? Price { get; set; }

    [Required]
    public bool? Active { get; set; }

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
    public float? Weight { get; set; }

    [Required]
    public int? ProductCategoryId { get; set; }

    public ProductCategory? ProductCategory { get; set; }

    public List<ProductImage>? Images { get; set; }
}