namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
