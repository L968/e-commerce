using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;

[Authorize]
public record DeleteAddressCommand(Guid Id) : IRequest<Result>;

public class DeleteAddressCommandHandler(
    IUnitOfWork unitOfWork,
    IAddressRepository addressRepository,
    ICurrentUserService currentUserService,
    IAuthorizationService authorizationService
    ) : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);
        if (address is null) return DomainErrors.NotFound(nameof(Address), request.Id);

        Guid? defaultAddressId = await _authorizationService.GetDefaultAddressIdAsync();

        if (defaultAddressId == address.Id)
        {
            await _authorizationService.UpdateDefaultAddressIdAsync(null);
        }

        _addressRepository.Delete(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
