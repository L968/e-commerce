using Bogus;
using Ecommerce.Domain.Entities.CartEntities;

namespace Ecommerce.Domain.UnitTests.SampleData;
public static class FakeCarts
{
    public static Cart GetCart()
    {
        var faker = new Faker<Cart>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.UserId, f => f.Random.Int());

        return faker.Generate();
    }

    public static IEnumerable<CartItem> GetCartItems()
    {
        throw new NotImplementedException();
    }
}
