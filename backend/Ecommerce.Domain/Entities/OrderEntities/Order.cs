using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Order : AuditableEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public decimal TotalProducts { get; private set; }
    public decimal ShippingCost { get; private set; }
    public decimal? Discount { get; private set; }
    public decimal Total { get; private set; }
    public string ShippingPostalCode { get; private set; } = "";
    public string ShippingStreetName { get; private set; } = "";
    public string ShippingBuildingNumber { get; private set; } = "";
    public string? ShippingComplement { get; private set; }
    public string? ShippingNeighborhood { get; private set; }
    public string? ShippingCity { get; private set; }
    public string? ShippingState { get; private set; }
    public string? ShippingCountry { get; private set; }
    public ICollection<OrderHistory> OrderHistory { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    private Order() { }

    private Order(
        int userId,
        decimal totalProducts,
        decimal shippingCost,
        decimal? discount,
        decimal total,
        string shippingPostalCode,
        string shippingStreetName,
        string shippingBuildingNumber,
        string? shippingComplement,
        string? shippingNeighborhood,
        string? shippingCity,
        string? shippingState,
        string? shippingCountry
    )
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TotalProducts = totalProducts;
        ShippingCost = shippingCost;
        Discount = discount;
        Total = total;
        ShippingPostalCode = shippingPostalCode;
        ShippingStreetName = shippingStreetName;
        ShippingBuildingNumber = shippingBuildingNumber;
        ShippingComplement = shippingComplement;
        ShippingNeighborhood = shippingNeighborhood;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingCountry = shippingCountry;
    }

    public static Result<Order> Create(
        int userId,
        IEnumerable<CartItem> cartItems,
        string shippingPostalCode,
        string shippingStreetName,
        string shippingBuildingNumber,
        string? shippingComplement,
        string? shippingNeighborhood,
        string? shippingCity,
        string? shippingState,
        string? shippingCountry
    )
    {
        if (userId <= 0)
        {
            return Result.Fail(DomainErrors.Order.InvalidUserId);
        }

        var selectedCartItems = cartItems.Where(ci => ci.IsSelectedForCheckout);
        if (!selectedCartItems.Any()) return Result.Fail(DomainErrors.Order.EmptyProductList);

        decimal total = 0;
        decimal discount = 0;

        foreach (CartItem cartItem in selectedCartItems)
        {
            ProductCombination productCombination = cartItem.ProductCombination;
            Product product = productCombination.Product;
            decimal productPrice = productCombination.Price;

            if (!product.Active) return Result.Fail(DomainErrors.Order.InactiveProduct);

            if (productCombination.Inventory.Stock < cartItem.Quantity) return Result.Fail(DomainErrors.Order.InsufficientStock);

            ProductDiscount? activeProductDiscount = product.Discounts.Where(d => d.IsCurrentlyActive()).FirstOrDefault();

            if (activeProductDiscount is not null)
            {
                switch (activeProductDiscount.DiscountUnit)
                {
                    case DiscountUnit.Percentage:
                        productPrice = productPrice * activeProductDiscount.DiscountValue / 100;
                        discount += productCombination.Price - productPrice;
                        break;
                    case DiscountUnit.FixedAmount:
                        productPrice -= activeProductDiscount.DiscountValue;
                        discount += activeProductDiscount.DiscountValue;
                        break;
                    default:
                        return Result.Fail(DomainErrors.Order.DiscountUnitNotImplemented);
                }
            }

            var result = productCombination.Inventory.ReduceStock(cartItem.Quantity);
            if (result.IsFailed) return result;

            total += productPrice;
        }

        decimal shippingCost = CalculateShippingCost(selectedCartItems.Count());

        var order = new Order(
            userId,
            totalProducts: selectedCartItems.Count(),
            shippingCost,
            discount,
            total,
            shippingPostalCode,
            shippingStreetName,
            shippingBuildingNumber,
            shippingComplement,
            shippingNeighborhood,
            shippingCity,
            shippingState,
            shippingCountry
        );

        // insere historico
        // insere pedido
        // limpar carrinho
        return order;
    }

    private static decimal CalculateShippingCost(int orderItems)
    {
        return orderItems * 10;
    }

    // (Application) Verifica se produto existe
}