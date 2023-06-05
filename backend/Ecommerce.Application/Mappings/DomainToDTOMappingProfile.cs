using AutoMapper;

namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>().ReverseMap();
        CreateMap<Address, CreateAddressDto>();
        CreateMap<Address, UpdateAddressDto>();
    }
}