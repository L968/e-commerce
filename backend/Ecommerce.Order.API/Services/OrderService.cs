using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Ecommerce.Order.API.Models.PayPal;
using Ecommerce.Order.API.RabbitMqClient;
using Ecommerce.Order.API.Repositories;
using FluentResults;

namespace Ecommerce.Order.API.Services;

public class OrderService(
    IMapper mapper,
    IOrderRepository repository,
    IPayPalService payPalService,
    IRabbitMqClient publisher
    ) : IOrderService
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _repository = repository;
    private readonly IPayPalService _payPalService = payPalService;
    private readonly IRabbitMqClient _publisher = publisher;

    public async Task<OrderDto?> GetByIdAsync(Guid id, int userId)
    {
        var order = await _repository.GetByIdAsync(id, userId);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize)
    {
        var (orders, totalItems) = await _repository.GetByUserIdAsync(userId, page, pageSize);

        return new Pagination<OrderDto>(
            page,
            pageSize,
            totalItems,
            _mapper.Map<IEnumerable<OrderDto>>(orders)
        );
    }

    public async Task<IEnumerable<OrderDto>> GetPendingOrdersAsync()
    {
        var orders = await _repository.GetPendingOrdersAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<string> CreateOrderAsync(OrderCheckoutDto orderCheckout)
    {
        string checkoutUrl = "";
        orderCheckout.ExternalPaymentId = null;

        if (orderCheckout.PaymentMethod == PaymentMethod.PayPal)
        {
            CreateOrderResponse response = await _payPalService.CreateOrderAsync();

            orderCheckout.ExternalPaymentId = response.id;
            checkoutUrl = response.links.FirstOrDefault(link => link.rel == "payer-action")?.href!;
        }

        _publisher.PublishOrder(orderCheckout);

        return checkoutUrl;
    }

    public async Task<Result> ProcessPayPalReturnAsync(string token)
    {
        GetOrderResponse? paypalOrder = await _payPalService.GetOrderAsync(token);

        if (paypalOrder is null)
            return Result.Fail("Failed to retrieve PayPal order details. Please try again");

        if (paypalOrder.status != "APPROVED")
            return Result.Fail("The PayPal order status is not approved. Cannot process payment");

        var order = await _repository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            return Result.Fail("Failed to find an order with the provided PayPal token");

        var result = order.CompletePayment();

        if (result.IsFailed)
            return result;

        await _repository.UpdateAsync(order);

        return Result.Ok();
    }

    public async Task<Result> ProcessPayPalCancelAsync(string token)
    {
        var order = await _repository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            return Result.Fail("Failed to find an order with the provided PayPal token");

        order.Cancel();
        await _repository.UpdateAsync(order);
        return Result.Ok();
    }
}
