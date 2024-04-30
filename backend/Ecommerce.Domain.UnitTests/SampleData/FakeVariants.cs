using Bogus;
using Ecommerce.Domain.Entities.VariantEntities;

namespace Ecommerce.Domain.UnitTests.SampleData;

public static class FakeVariants
{
    public static Variant GetVariant(int optionsCount)
    {
        var faker = new Faker<Variant>()
            .CustomInstantiator(f =>
            {
                var options = new List<string>();

                for (int i = 0; i < optionsCount; i++)
                {
                    options.Add(f.Random.String2(4));
                }

                return Variant.Create(
                    name: f.Random.String2(4),
                    options: options
                ).Value;
            });

        var variant = faker.Generate();

        for (int i = 0; i < variant.Options.Count; i++)
        {
            VariantOption option = variant.Options.ElementAt(i);
            Utils.SetPrivateProperty(option, "Id", i + 1);
            Utils.SetPrivateProperty(option, "Variant", variant);
        }

        return variant;
    }

    public static IEnumerable<VariantOption> GetVariantOptions(int count = 1)
    {
        var variant = GetVariant(count);
        return variant.Options;
    }
}
