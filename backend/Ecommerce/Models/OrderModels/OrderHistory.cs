namespace Ecommerce.Models.OrderModels
{
    public class OrderHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? OrderId { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime? Date { get; set; }
    }
}