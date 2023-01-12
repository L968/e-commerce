namespace Ecommerce.Models.OrderModels
{
    public class Order : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public Guid? Guid { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public decimal? TotalProducts { get; set; }

        [Required]
        public decimal? ShippingCost { get; set; }

        public decimal? Discount { get; set; }

        [Required]
        public decimal? Total { get; set; }

        [Required]
        public string? ShippingPostalCode { get; set; }

        [Required]
        public string? ShippingStreetName { get; set; }

        [Required]
        public string? ShippingBuildingNumber { get; set; }

        public string? ShippingComplement { get; set; }

        public string? ShippingNeighborhood { get; set; }

        [Required]
        public string? ShippingCity { get; set; }

        [Required]
        public string? ShippingState { get; set; }

        [Required]
        public string? ShippingCountry { get; set; }
    }
}