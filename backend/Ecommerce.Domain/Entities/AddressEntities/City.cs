namespace Ecommerce.Domain.Entities.AddressEntities;

public sealed class City
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int StateId { get; private set; }

    public State? State { get; set; }
    public List<Address>? Addresses { get; set; }

    public City(string name, int stateId)
    {
        ValidateName(name);
        ValidateStateId(stateId);

        Name = name;
        StateId = stateId;
    }

    public City(int id, string name, int stateId)
    {
        ValidateId(id);
        ValidateName(name);
        ValidateStateId(stateId);

        Id = id;
        Name = name;
        StateId = stateId;
    }

    public void Update(string name, int stateId)
    {
        ValidateName(name);
        ValidateStateId(stateId);

        Name = name;
        StateId = stateId;
    }

    private void ValidateId(int id)
    {
        DomainExceptionValidation.When(id <= 0,
            $"Invalid {nameof(id)} value");
    }

    private void ValidateName(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
            $"Invalid {nameof(name)}. It cannot be null or empty");

        DomainExceptionValidation.When(
            name.Length < 3 ||
            name.Length > 100,
            $"Invalid {nameof(name)}. It must have between 3 and 100 characters"
        );
    }

    private void ValidateStateId(int stateId)
    {
        DomainExceptionValidation.When(stateId <= 0,
            $"Invalid {nameof(stateId)} value");
    }
}