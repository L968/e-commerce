namespace Ecommerce.Models.AddressModels
{
    public class State
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public int? CountryId { get; set; }

        public Country? Country { get; set; }

        [JsonIgnore]
        public List<City>? Cities { get; set; }
    }
}