namespace Ecommerce.Models
{
    public class City
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int? StateId { get; set; }

        public State? State { get; set; }

        [JsonIgnore]
        public List<Address>? Addresses { get; set; }
    }
}