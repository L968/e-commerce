namespace Ecommerce.Models.CartModels
{
    public class CartItem
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? CartId { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
}