using Ecommerce.Domain.Entities.CartEntities;
using Ecommerce.Domain.Entities.ProductEntities;

namespace Ecommerce.Domain.UnitTests
{
    public static class SampleData
    {
        public static List<ProductCategory> GetProductCategories()
        {
            var variantIds = new List<Guid>();

            return
            [
                ProductCategory.Create("Electronics", "Electronic devices and accessories", variantIds).Value,
                ProductCategory.Create("Clothing", "Fashionable clothing items", variantIds).Value,
                ProductCategory.Create("Home and Kitchen", "Household items and kitchenware", variantIds).Value
            ];
        }

        public static List<Product> GetProducts()
        {
            var product1 = Product.Create("Smartphone XYZ", "High-end smartphone with advanced features", true, true, Guid.NewGuid()).Value;
            var productCombination1 = ProductCombination.Create(
                productId: product1.Id,
                existingCombinations: product1.Combinations,
                variantOptions: [],
                sku: "XYZ-128GB-BLACK",
                price: 1000,
                stock: 100,
                length: 5.5f,
                width: 2.8f,
                height: 0.3f,
                weight: 0.5f,
                imagePaths: ["/images/smartphone_xyz_black.jpg"]
            ).Value;
            SetPrivateProperty(productCombination1, "Product", product1);
            product1.Combinations.Add(productCombination1);

            var product2 = Product.Create("Laptop ABC", "Powerful laptop for professional use", true, true, Guid.NewGuid()).Value;
            var productCombination2 = ProductCombination.Create(
                productId: product2.Id,
                existingCombinations: product2.Combinations,
                variantOptions: [],
                sku: "ABC-512GB-PRO",
                price: 1500,
                stock: 50,
                length: 14.0f,
                width: 9.8f,
                height: 0.7f,
                weight: 2.0f,
                imagePaths: ["/images/laptop_abc_pro.jpg"]
            ).Value;

            SetPrivateProperty(productCombination2, "Product", product2);
            product2.Combinations.Add(productCombination2);

            var product3 = Product.Create("Men\'s Jeans", "Classic blue jeans for men", true, true, Guid.NewGuid()).Value;

            var productCombination3 = ProductCombination.Create(
                productId: product3.Id,
                existingCombinations: product3.Combinations,
                variantOptions: [],
                sku: "JEANS-34-BLUE",
                price: 50,
                stock: 200,
                length: 1,
                width: 1,
                height: 1,
                weight: 0.7f,
                imagePaths: ["/images/mens_jeans_blue.jpg"]
            ).Value;
            SetPrivateProperty(productCombination3, "Product", product3);
            product3.Combinations.Add(productCombination3);

            var product4 = Product.Create("Cookware Set", "Complete set of non-stick cookware", true, true, Guid.NewGuid()).Value;
            var productCombination4 = ProductCombination.Create(
                productId: product4.Id,
                existingCombinations: product4.Combinations,
                variantOptions: [],
                sku: "COOKWARE-10PC-NONSTICK",
                price: 200,
                stock: 30,
                length: 1,
                width: 1,
                height: 1,
                weight: 0.5f,
                imagePaths: ["/images/cookware_set_nonstick.jpg"]
            ).Value;
            SetPrivateProperty(productCombination4, "Product", product4);
            product4.Combinations.Add(productCombination4);

            return
            [
                product1,
                product2,
                product3,
                product4
            ];
        }

        public static List<Cart> GetCarts()
        {
            return [new Cart(1)];
        }

        public static List<CartItem> GetCartItems()
        {
            var carts = GetCarts();
            var products = GetProducts();

            return
            [
                CartItem.Create(carts[0].Id, products[0].Combinations[0].Id, 2).Value,
                CartItem.Create(carts[0].Id, products[1].Combinations[0].Id, 1).Value,
                CartItem.Create(carts[0].Id, products[2].Combinations[0].Id, 3).Value
            ];
        }

        private static void SetPrivateProperty(object instance, string propertyName, object value)
        {
            var property = instance.GetType().GetProperty(propertyName);
            property?.SetValue(instance, value);
        }
    }
}
