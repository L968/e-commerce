namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>().ReverseMap();
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();
    }
}