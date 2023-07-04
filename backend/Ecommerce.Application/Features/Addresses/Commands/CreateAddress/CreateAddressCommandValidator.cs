namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(address => address.RecipientFullName)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(address => address.RecipientPhoneNumber)
            .NotEmpty()
            .Length(8, 15)
            .Matches(@"^[0-9]+$").WithMessage("{PropertyName} must contain numbers only");

        RuleFor(address => address.PostalCode)
            .NotEmpty()
            .Length(5, 9);

        RuleFor(address => address.StreetName)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(address => address.BuildingNumber)
            .NotEmpty();

        RuleFor(address => address.Complement)
            .MaximumLength(100);

        RuleFor(address => address.Neighborhood)
            .MaximumLength(100);

        RuleFor(address => address.City)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(address => address.State)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(address => address.Country)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(address => address.AdditionalInformation)
            .MaximumLength(300);
    }
}