using Ecommerce.Data.DTO.Address;

namespace Ecommerce.Models.AddressModels
{
    public class Address : BaseModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public string? RecipientFullName { get; set; }

        [Required]
        public string? RecipientPhoneNumber { get; set; }

        [Required]
        public string? PostalCode { get; set; }

        [Required]
        public string? StreetName { get; set; }

        [Required]
        public string? BuildingNumber { get; set; }

        public string? Complement { get; set; }

        public string? Neighborhood { get; set; }

        [Required]
        public int? CityId { get; set; }

        [JsonIgnore]
        public City? City { get; set; }

        public string? AdditionalInformation { get; set; }

        public GetAddressDto ToDto()
        {
            return new GetAddressDto()
            {
                Id = Id,
                UserId = UserId,
                RecipientFullName = RecipientFullName,
                RecipientPhoneNumber = RecipientPhoneNumber,
                PostalCode = PostalCode,
                StreetName = StreetName,
                BuildingNumber = BuildingNumber,
                Complement = Complement,
                Neighborhood = Neighborhood,
                CityId = CityId,
                City = City,
                AdditionalInformation = AdditionalInformation
            };
        }
    }
}