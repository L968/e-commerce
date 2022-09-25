namespace Ecommerce.Models
{
    public class ProductReview : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [Required]
        public int? Rating { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool? Anonymous { get; set; }
    }
}