using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Addresses.Commands;

public record DeleteAddressCommand(int Id) : IRequest<Result>;

public class AddressDeleteCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddressDeleteCommandHandler(IAddressRepository addressRepository, ICurrentUserService currentUserService)
    {
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        int userId = _currentUserService.UserId!.Value;
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, userId);

        if (address == null)
        {
            return Result.Fail("Address not found");
        }

        await _addressRepository.DeleteAsync(address);
        return Result.Ok();
    }
}