using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Order : AuditableEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentMethod PaymentMethod{ get; set; }
    public decimal ShippingCost { get; private set; }
    public decimal? Discount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string? ExternalPaymentId { get; private set; }
    public string ShippingPostalCode { get; private set; } = "";
    public string ShippingStreetName { get; private set; } = "";
    public string ShippingBuildingNumber { get; private set; } = "";
    public string? ShippingComplement { get; private set; }
    public string? ShippingNeighborhood { get; private set; }
    public string? ShippingCity { get; private set; }
    public string? ShippingState { get; private set; }
    public string? ShippingCountry { get; private set; }

    private readonly List<OrderHistory> _history = [];
    public IReadOnlyCollection<OrderHistory> History => _history;

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order() { }

    private Order(
        int userId,
        PaymentMethod paymentMethod,
        decimal shippingCost,
        decimal? discount,
        decimal totalAmount,
        string? externalPaymentId,
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
        PaymentMethod = paymentMethod;
        ShippingCost = shippingCost;
        Discount = discount;
        TotalAmount = totalAmount;
        ExternalPaymentId = externalPaymentId;
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
        PaymentMethod paymentMethod,
        string? externalPaymentId,
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
            return Result.Fail(DomainErrors.Order.InvalidUserId);

        var selectedCartItems = cartItems.Where(ci => ci.IsSelectedForCheckout);

        if (!selectedCartItems.Any())
            return Result.Fail(DomainErrors.Order.EmptyProductList);

        decimal totalAmount = 0;
        decimal totalDiscount = 0;
        var orderItems = new List<OrderItem>();

        foreach (CartItem cartItem in selectedCartItems)
        {
            ProductCombination productCombination = cartItem.ProductCombination!;
            Product product = productCombination.Product;
            decimal productPrice = productCombination.Price;

            if (!product.Active)
                return Result.Fail(DomainErrors.Order.InactiveProduct);

            var validateStockResult = productCombination.Inventory.ValidateStock(cartItem.Quantity);

            if (validateStockResult.IsFailed)
                return validateStockResult;

            var discountResult = productCombination.GetDiscount();
            if (discountResult.IsFailed)
                return Result.Fail(discountResult.Errors);

            decimal productDiscount = discountResult.Value;
            productPrice -= productDiscount;
            totalDiscount += productDiscount;
            totalAmount += (productPrice * cartItem.Quantity);

            orderItems.Add(new OrderItem(
                orderId: Guid.Empty,
                productCombinationId: cartItem.ProductCombinationId,
                quantity: cartItem.Quantity,
                productName: productCombination.Product.Name,
                productSku: productCombination.Sku,
                productImagePath: productCombination.Images.ElementAt(0).ImagePath,
                productUnitPrice: productPrice,
                productDiscount: productDiscount == 0 ? null : productDiscount
            ));
        }

        decimal shippingCost = CalculateShippingCost(selectedCartItems.Count());

        var order = new Order(
            userId,
            paymentMethod,
            shippingCost,
            totalDiscount,
            totalAmount,
            externalPaymentId,
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
            var result = order.AddItem(orderItem);
            if (result.IsFailed) return result;
        }

        order.AddHistory(OrderStatus.PendingPayment, "Order created, awaiting payment");

        return Result.Ok(order);
    }

    public Result CompletePayment()
    {
        if (Status != OrderStatus.PendingPayment)
            return Result.Fail(DomainErrors.Order.InvalidPaymentStatus);

        Status = OrderStatus.Processing;
        AddHistory(OrderStatus.Processing, "Payment completed, order processing");

        return Result.Ok();
    }

    public void AddHistory(OrderStatus status, string? notes = null)
    {
        _history.Add(new OrderHistory(Id, status, notes));
    }

    public Result AddItem(OrderItem orderItem)
    {
        if (Status == OrderStatus.Cancelled)
            return Result.Fail(DomainErrors.Order.CannotAddItemToCancelledOrder);

        orderItem.SetOrderId(Id);
        _items.Add(orderItem);
        return Result.Ok();
    }

    private static decimal CalculateShippingCost(int orderItems)
    {
        // TODO: Calculate Shipping
        return orderItems * 10;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled) return;

        Status = OrderStatus.Cancelled;
        AddHistory(OrderStatus.Cancelled, "Order cancelled");
    }
}
