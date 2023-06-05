namespace Ecommerce.Domain.Entities.AddressEntities;

public sealed class State
{
    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int CountryId { get; private set; }

    public Country? Country { get; set; }
    public List<City>? Cities { get; set; }

    public State(string code, string name, int countryId)
    {
        ValidateCode(code);
        ValidateName(name);
        ValidateCountryId(countryId);

        Code = code;
        Name = name;
        CountryId = countryId;
    }

    public State(int id, string code, string name, int countryId)
    {
        ValidateId(id);
        ValidateCode(code);
        ValidateName(name);
        ValidateCountryId(countryId);

        Id = id;
        Code = code;
        Name = name;
        CountryId = countryId;
    }

    public void Update(string code, string name, int countryId)
    {
        ValidateCode(code);
        ValidateName(name);
        ValidateCountryId(countryId);

        Code = code;
        Name = name;
        CountryId = countryId;
    }

    private void ValidateId(int id)
    {
        DomainExceptionValidation.When(id < 0,
            $"Invalid {nameof(id)} value");
    }

    private void ValidateCode(string code)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(code),
            $"Invalid {nameof(code)}. It cannot be null or empty");

        DomainExceptionValidation.When(code.Length != 2,
            $"Invalid {nameof(code)}. It must have 2 characters");
    }

    private void ValidateName(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
            $"Invalid {nameof(name)}. It cannot be null or empty");
    }

    private void ValidateCountryId(int countryId)
    {
        DomainExceptionValidation.When(countryId < 0,
            $"Invalid {nameof(countryId)} value");
    }
}