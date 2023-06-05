namespace Ecommerce.Domain.Entities.AddressEntities;

public sealed class Country
{
    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }

    public List<State>? States { get; set; }

    public Country(string code, string name)
    {
        ValidateCode(code);
        ValidateName(name);

        Code = code.ToUpper();
        Name = name;
    }

    public Country(int id, string code, string name)
    {
        ValidateId(id);
        ValidateCode(code);
        ValidateName(name);

        Id = id;
        Code = code.ToUpper();
        Name = name;
    }

    public void Update(string code, string name)
    {
        ValidateCode(code);
        ValidateName(name);

        Code = code.ToUpper();
        Name = name;
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
}