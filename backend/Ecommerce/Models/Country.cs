namespace Ecommerce.Models
{
    public class Country
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        public List<State>? States { get; set; }
    }
}