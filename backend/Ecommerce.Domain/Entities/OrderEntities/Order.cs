namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Order : AuditableEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public OrderStatus Status { get; set; }
    public decimal ShippingCost { get; private set; }
    public decimal? Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string ShippingPostalCode { get; private set; } = "";
    public string ShippingStreetName { get; private set; } = "";
    public string ShippingBuildingNumber { get; private set; } = "";
    public string? ShippingComplement { get; private set; }
    public string? ShippingNeighborhood { get; private set; }
    public string? ShippingCity { get; private set; }
    public string? ShippingState { get; private set; }
    public string? ShippingCountry { get; private set; }

    private readonly List<OrderHistory> _history = new();
    public IReadOnlyCollection<OrderHistory> History => _history;

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order() { }

    private Order(
        int userId,
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
        Status = OrderStatus.PendingPayment;
        ShippingCost = shippingCost;
        Discount = discount;
        TotalAmount = total;
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

        decimal totalAmount = 0;
        decimal totalDiscount = 0;
        var orderItems = new List<OrderItem>();

        foreach (CartItem cartItem in selectedCartItems)
        {
            ProductCombination productCombination = cartItem.ProductCombination!;
            Product product = productCombination.Product;
            decimal productPrice = productCombination.Price;

            if (!product.Active) return Result.Fail(DomainErrors.Order.InactiveProduct);

            var reduceStockResult = productCombination.Inventory.ReduceStock(cartItem.Quantity);
            if (reduceStockResult.IsFailed) return reduceStockResult;

            var discountResult = productCombination.GetDiscount();
            if (discountResult.IsFailed) return Result.Fail(discountResult.Errors);

            decimal productDiscount = discountResult.Value;
            productPrice -= productDiscount;
            totalDiscount += productDiscount;
            totalAmount += (productPrice * cartItem.Quantity);

            orderItems.Add(new OrderItem(
                orderId: Guid.Empty,
                productCombinationId: cartItem.ProductCombinationId,
                productName: cartItem.ProductCombination!.Product.Name,
                productSku: cartItem.ProductCombination.Sku,
                productImagePath: cartItem.ProductCombination.Images.ElementAt(0).ImagePath,
                productUnitPrice: productPrice,
                productDiscount: productDiscount == 0 ? null : productDiscount
            ));
        }

        decimal shippingCost = CalculateShippingCost(selectedCartItems.Count());

        var order = new Order(
            userId,
            shippingCost,
            totalDiscount,
            totalAmount,
            shippingPostalCode,
            shippingStreetName,
            shippingBuildingNumber,
            shippingComplement,
            shippingNeighborhood,
            shippingCity,
            shippingState,
            shippingCountry
        );

        foreach (OrderItem orderItem in orderItems)
        {
            order.AddItem(orderItem);
        }

        order.AddHistory(OrderStatus.PendingPayment, "Order created, awaiting payment");

        return Result.Ok(order);
    }

    public void AddHistory(OrderStatus status, string? notes = null)
    {
        _history.Add(new OrderHistory(Id, status, notes));
    }

    public void AddItem(OrderItem orderItem)
    {
        orderItem.SetOrderId(Id);
        _items.Add(orderItem);
    }

    private static decimal CalculateShippingCost(int orderItems)
    {
        // TODO: Calculate Shipping
        return orderItems * 10;
    }
}
