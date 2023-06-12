﻿using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Addresses.Queries;

public record GetAddressByIdAndUserIdQuery(int Id) : IRequest<GetAddressDto?>;

public class GetAddressByIdAndUserIdQueryHandler : IRequestHandler<GetAddressByIdAndUserIdQuery, GetAddressDto?>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetAddressByIdAndUserIdQueryHandler(IMapper mapper, IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<GetAddressDto?> Handle(GetAddressByIdAndUserIdQuery request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId!.Value;
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, userId);
        return _mapper.Map<GetAddressDto>(address);
    }
}