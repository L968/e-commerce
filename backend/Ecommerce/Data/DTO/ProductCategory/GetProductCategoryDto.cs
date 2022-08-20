namespace Ecommerce.Data.DTO
{
    public record GetProductCategoryDto
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public Guid? Guid { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }
}