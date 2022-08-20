using Ecommerce.Data.DTO.Address;

namespace Ecommerce.Services
{
    public class AddressService
    {
        private readonly Context _context;

        public AddressService(Context context)
        {
            _context = context;
        }

        public IEnumerable<GetAddressDto> Get(int userId)
        {
            return _context.Addresses
                .Include(address => address.City)
                .Where(address => address.UserId == userId)
                .Select(address => address.ToDto());
        }

        public GetAddressDto? Get(int id, int userId)
        {
            return _context.Addresses
                .Include(address => address.City)
                .Where(address => address.Id == id)
                .Where(address => address.UserId == userId)
                .FirstOrDefault()?
                .ToDto();
        }

        public GetAddressDto Create(CreateAddressDto addressDto)
        {
            var address = new Address()
            {
                UserId = addressDto.UserId,
                RecipientFullName = addressDto.RecipientFullName,
                RecipientPhoneNumber = addressDto.RecipientPhoneNumber,
                PostalCode = addressDto.PostalCode,
                StreetName = addressDto.StreetName,
                BuildingNumber = addressDto.BuildingNumber,
                Complement = addressDto.Complement,
                Neighborhood = addressDto.Neighborhood,
                CityId = addressDto.CityId,
                AdditionalInformation = addressDto.AdditionalInformation,
            };

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return address.ToDto();
        }

        public Result Update(int id, int userId, UpdateAddressDto addressDto)
        {
            Address? address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
            {
                return Result.Fail("Address not found");
            }

            if (address.UserId != userId)
            {
                return Result.Fail("You do not own this address");
            }

            address.RecipientFullName = addressDto.RecipientFullName;
            address.RecipientPhoneNumber = addressDto.RecipientPhoneNumber;
            address.PostalCode = addressDto.PostalCode;
            address.StreetName = addressDto.StreetName;
            address.BuildingNumber = addressDto.BuildingNumber;
            address.Complement = addressDto.Complement;
            address.Neighborhood = addressDto.Neighborhood;
            address.CityId = addressDto.CityId;
            address.AdditionalInformation = addressDto.AdditionalInformation;

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result Delete(int id, int userId)
        {
            Address? address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
            {
                return Result.Fail("Address not found");
            }

            if (address.UserId != userId)
            {
                return Result.Fail("You do not own this address");
            }

            _context.Remove(address);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}