namespace Ecommerce.Models
{
    public class Cart
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? UserId { get; set; }
    }
}