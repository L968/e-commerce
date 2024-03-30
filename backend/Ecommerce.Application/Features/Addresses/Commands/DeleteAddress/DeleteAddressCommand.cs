namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;

[Authorize]
public record DeleteAddressCommand(int Id) : IRequest<Result>;

public class DeleteAddressCommandHandler(
    IUnitOfWork unitOfWork,
    IAddressRepository addressRepository, 
    ICurrentUserService currentUserService
    ) : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);

        if (address is null) return Result.Fail(DomainErrors.NotFound(nameof(Address), request.Id));

        _addressRepository.Delete(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
