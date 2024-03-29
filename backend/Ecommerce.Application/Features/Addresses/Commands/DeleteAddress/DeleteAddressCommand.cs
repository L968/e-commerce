﻿namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;

[Authorize]
public record DeleteAddressCommand(int Id) : IRequest<Result>;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAddressCommandHandler(IAddressRepository addressRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _addressRepository = addressRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.GetByIdAndUserIdAsync(request.Id, _currentUserService.UserId);

        if (address is null) return Result.Fail(DomainErrors.NotFound(nameof(Address), request.Id));

        _addressRepository.Delete(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}