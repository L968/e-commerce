namespace Ecommerce.Application.Features.Orders.Commands.OrderCheckout;

[Authorize]
public record OrderCheckoutCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
    public string ShippingPostalCode { get; set; } = "";
    public string ShippingStreetName { get; set; } = "";
    public string ShippingBuildingNumber { get; set; } = "";
    public string? ShippingComplement { get; set; }
    public string? ShippingNeighborhood { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingCountry { get; set; }
}

public class OrderCheckoutCommandHandler : IRequestHandler<OrderCheckoutCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public OrderCheckoutCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(OrderCheckoutCommand request, CancellationToken cancellationToken)
    {
        var result = Order.Create(
            request.UserId,
            request.CartItems,
            request.ShippingPostalCode,
            request.ShippingStreetName,
            request.ShippingBuildingNumber,
            request.ShippingComplement,
            request.ShippingNeighborhood,
            request.ShippingCity,
            request.ShippingState,
            request.ShippingCountry
        );

        if (result.IsFailed) return Result.Fail(result.Errors);

        Order order = result.Value;

        await _orderRepository.CreateAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}