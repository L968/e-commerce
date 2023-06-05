namespace Ecommerce.Application.Services.AddressServices;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAddressDto>> GetByUserIdAsync(int userId)
    {
        IEnumerable<Address> categories = await _addressRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<GetAddressDto>>(categories);
    }

    public async Task<GetAddressDto?> GetByIdAndUserIdAsync(int id, int userId)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(id, userId);
        return _mapper.Map<GetAddressDto?>(address);
    }

    public async Task<GetAddressDto> CreateAsync(CreateAddressDto addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);

        await _addressRepository.CreateAsync(address);

        return _mapper.Map<GetAddressDto>(address);
    }

    public async Task<Result> UpdateAsync(int id, int userId, UpdateAddressDto addressDto)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(id, userId);

        if (address == null)
        {
            return Result.Fail("Address not found");
        }

        _mapper.Map(addressDto, address);

        await _addressRepository.UpdateAsync(address);
        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id, int userId)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(id, userId);

        if (address == null)
        {
            return Result.Fail("Address not found");
        }

        await _addressRepository.DeleteAsync(address);
        return Result.Ok();
    }
}