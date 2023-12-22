using Ecommerce.Domain.Entities.CartEntities;
using Ecommerce.Domain.Entities.ProductEntities;

namespace Ecommerce.Domain.UnitTests
{
    public static class SampleData
    {
        public static List<ProductCategory> GetProductCategories()
        {
            var variantIds = new List<Guid>();

            return new List<ProductCategory>
            {
                ProductCategory.Create("Electronics", "Electronic devices and accessories", variantIds).Value,
                ProductCategory.Create("Clothing", "Fashionable clothing items", variantIds).Value,
                ProductCategory.Create("Home and Kitchen", "Household items and kitchenware", variantIds).Value
            };
        }

        public static List<Product> GetProducts()
        {
            var product1 = Product.Create("Smartphone XYZ", "High-end smartphone with advanced features", true, true, Guid.NewGuid()).Value;
            var productCombination1 = ProductCombination.Create(product1.Id, "Color: Black, Storage: 128GB", "XYZ-128GB-BLACK", 1000, 100, 5.5f, 2.8f, 0.3f, 0.5f, new List<string>() { "/images/smartphone_xyz_black.jpg" }).Value;
            SetPrivateProperty(productCombination1, "Product", product1);
            product1.Combinations.Add(productCombination1);

            var product2 = Product.Create("Laptop ABC", "Powerful laptop for professional use", true, true, Guid.NewGuid()).Value;
            var productCombination2 = ProductCombination.Create(product2.Id, "Model: Pro, Storage: 512GB", "ABC-512GB-PRO", 1500, 50, 14.0f, 9.8f, 0.7f, 2.0f, new List<string>() { "/images/laptop_abc_pro.jpg" }).Value;

            SetPrivateProperty(productCombination2, "Product", product2);
            product2.Combinations.Add(productCombination2);

            var product3 = Product.Create("Men\'s Jeans", "Classic blue jeans for men", true, true, Guid.NewGuid()).Value;

            var productCombination3 = ProductCombination.Create(product3.Id, "Size: 34, Color: Blue", "JEANS-34-BLUE", 50, 200, 1, 1, 1, 0.7f, new List<string>() { "/images/mens_jeans_blue.jpg" }).Value;
            SetPrivateProperty(productCombination3, "Product", product3);
            product3.Combinations.Add(productCombination3);

            var product4 = Product.Create("Cookware Set", "Complete set of non-stick cookware", true, true, Guid.NewGuid()).Value;
            var productCombination4 = ProductCombination.Create(product4.Id, "Set: 10-piece, Material: Non-stick", "COOKWARE-10PC-NONSTICK", 200, 30, 1, 1, 1, 0.5f, new List<string>() { "/images/cookware_set_nonstick.jpg" }).Value;
            SetPrivateProperty(productCombination4, "Product", product4);
            product4.Combinations.Add(productCombination4);

            return new List<Product>
            {
                product1,
                product2,
                product3,
                product4
            };
        }

        public static List<Cart> GetCarts()
        {
            return new List<Cart>()
            {
                new Cart(1)
            };
        }

        public static List<CartItem> GetCartItems()
        {
            var carts = GetCarts();
            var products = GetProducts();

            return new List<CartItem>()
            {
                CartItem.Create(carts[0].Id, products[0].Combinations[0].Id, 2, true, products[0].Combinations[0]).Value,
                CartItem.Create(carts[0].Id, products[1].Combinations[0].Id, 1, true, products[1].Combinations[0]).Value,
                CartItem.Create(carts[0].Id, products[2].Combinations[0].Id, 3, true, products[2].Combinations[0]).Value
            };
        }

        private static void SetPrivateProperty(object instance, string propertyName, object value)
        {
            var property = instance.GetType().GetProperty(propertyName);
            property?.SetValue(instance, value);
        }
    }
}
