using Ecommerce.Domain.DTOs;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities.OrderEntities;

public sealed class Order : AuditableEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public decimal ShippingCost { get; private set; }
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
        IEnumerable<CreateOrderItemDto> items,
        PaymentMethod paymentMethod,
        decimal shippingCost,
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
        ShippingPostalCode = shippingPostalCode;
        ShippingStreetName = shippingStreetName;
        ShippingBuildingNumber = shippingBuildingNumber;
        ShippingComplement = shippingComplement;
        ShippingNeighborhood = shippingNeighborhood;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingCountry = shippingCountry;

        foreach (var item in items)
        {
            _items.Add(new OrderItem(
                Id,
                item.ProductCombinationId,
                item.Quantity,
                item.ProductName,
                item.ProductSku,
                item.ProductImagePath,
                item.ProductUnitPrice,
                item.ProductDiscount
                )
            );
        }
    }

    public static Result<Order> Create(
        int userId,
        IEnumerable<CreateOrderCartItemDto> cartItems,
        PaymentMethod paymentMethod,
        decimal shippingCost,
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

        if (!cartItems.Any())
            return Result.Fail(DomainErrors.Order.EmptyProductList);

        var orderItems = new List<CreateOrderItemDto>();

        foreach (CreateOrderCartItemDto cartItem in cartItems)
        {
            ProductCombination productCombination = cartItem.ProductCombination;
            Product product = productCombination.Product;

            if (!product.Active)
                return Result.Fail(DomainErrors.Order.InactiveProduct);

            if (cartItem.Quantity <= 0)
                return Result.Fail(DomainErrors.CartItem.InvalidQuantity);

            var validateStockResult = productCombination.Inventory.ValidateStock(cartItem.Quantity);
            if (validateStockResult.IsFailed)
                return validateStockResult;

            var discountResult = productCombination.GetDiscount();
            if (discountResult.IsFailed)
                return Result.Fail(discountResult.Errors);

            decimal productDiscount = discountResult.Value;

            orderItems.Add(new CreateOrderItemDto {
                ProductCombinationId = cartItem.ProductCombination.Id,
                Quantity = cartItem.Quantity,
                ProductName = product.Name,
                ProductSku = productCombination.Sku,
                ProductImagePath = productCombination.Images.ElementAt(0).ImagePath,
                ProductUnitPrice = productCombination.Price,
                ProductDiscount = productDiscount == 0 ? null : productDiscount
            });
        }

        var order = new Order(
            userId,
            orderItems,
            paymentMethod,
            shippingCost,
            shippingPostalCode,
            shippingStreetName,
            shippingBuildingNumber,
            shippingComplement,
            shippingNeighborhood,
            shippingCity,
            shippingState,
            shippingCountry
        );

        order.AddHistory(OrderStatus.PendingPayment, "Order created, awaiting payment");

        return Result.Ok(order);
    }

    public decimal GetTotalAmount()
    {
        decimal itemsTotal = _items.Sum(item => item.GetTotalAmount());
        return itemsTotal + ShippingCost;
    }

    public decimal GetTotalDiscount()
    {
        return _items.Sum(item => item.GetTotalDiscount());
    }

    public Result CompletePayment()
    {
        if (Status != OrderStatus.PendingPayment)
            return Result.Fail(DomainErrors.Order.InvalidPaymentStatus);

        Status = OrderStatus.Processing;
        AddHistory(OrderStatus.Processing, "Payment completed, order processing");

        return Result.Ok();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled) return;

        Status = OrderStatus.Cancelled;
        AddHistory(OrderStatus.Cancelled, "Order cancelled");
    }

    public void SetExternalPaymentId(string externalPaymentId)
    {
        ExternalPaymentId = externalPaymentId;
    }

    private void AddHistory(OrderStatus status, string? notes = null)
    {
        _history.Add(new OrderHistory(Id, status, notes));
    }
}
