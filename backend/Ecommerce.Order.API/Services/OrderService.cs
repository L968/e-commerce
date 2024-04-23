using AutoMapper;
using Ecommerce.Domain.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Errors;
using Ecommerce.Order.API.Interfaces;
using Ecommerce.Order.API.Models.PayPal;
using Ecommerce.Order.API.Repositories;
using Address = Ecommerce.Domain.Entities.Address;

namespace Ecommerce.Order.API.Services;

public class OrderService(
    IMapper mapper,
    IOrderRepository orderRepository,
    IPayPalService payPalService,
    IEcommerceService ecommerceService
    ) : IOrderService
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IPayPalService _payPalService = payPalService;
    private readonly IEcommerceService _ecommerceService = ecommerceService;

    public async Task<Pagination<OrderDto>> GetAllAsync(int page, int pageSize, OrderStatus? status)
    {
        var (orders, totalItems) = await _orderRepository.GetAllAsync(page, pageSize, status);

        return new Pagination<OrderDto>(
            page,
            pageSize,
            totalItems,
            _mapper.Map<IEnumerable<OrderDto>>(orders)
        );
    }

    public async Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize)
    {
        var (orders, totalItems) = await _orderRepository.GetByUserIdAsync(userId, page, pageSize);

        return new Pagination<OrderDto>(
            page,
            pageSize,
            totalItems,
            _mapper.Map<IEnumerable<OrderDto>>(orders)
        );
    }

    public async Task<OrderStatusCountDto> GetStatusCountAsync()
    {
        return await _orderRepository.GetStatusCountAsync();
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, int userId)
    {
        var order = await _orderRepository.GetByIdAsync(id, userId);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<Result<string>> CreateOrderAsync(OrderCheckoutDto orderCheckout)
    {
        var validator = new OrderCheckoutDtoValidator();
        var validatorResult = validator.Validate(orderCheckout);

        if (!validatorResult.IsValid)
        {
            var errorMessage = "Invalid order checkout. Errors:" + Environment.NewLine +
            $"{string.Join(Environment.NewLine, validatorResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"))}";

            return Result.Fail(errorMessage);
        }

        CreateOrderAddressDto? address = await _ecommerceService.GetAddressByIdAsync(orderCheckout.ShippingAddressId);

        if (address is null)
            return Result.Fail(DomainErrors.NotFound(nameof(Address), orderCheckout.ShippingAddressId));

        var cartItems = new List<CreateOrderCartItemDto>();

        foreach (var cartItemDto in orderCheckout.OrderCheckoutItems)
        {
            CreateOrderProductCombinationDto? productCombinationDto = await _ecommerceService.GetProductCombinationByIdAsync(cartItemDto.ProductCombinationId);

            if (productCombinationDto is null)
                return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), cartItemDto.ProductCombinationId));

            if (!productCombinationDto.Product.Active)
                return Result.Fail(DomainErrors.Product.InactiveProduct(productCombinationDto.Product.Id));

            ProductCombination productCombination = _mapper.Map<ProductCombination>(productCombinationDto);

            cartItems.Add(new CreateOrderCartItemDto
            {
                ProductCombination = productCombination,
                Quantity = cartItemDto.Quantity
            });
        }

        var result = Domain.Entities.OrderEntities.Order.Create(
            userId: orderCheckout.UserId,
            cartItems: cartItems,
            paymentMethod: orderCheckout.PaymentMethod,
            shippingCost: 20, // TODO: Implement shipping cost service
            shippingPostalCode: address.PostalCode,
            shippingStreetName: address.StreetName,
            shippingBuildingNumber: address.BuildingNumber,
            shippingComplement: address.Complement,
            shippingNeighborhood: address.Neighborhood,
            shippingCity: address.City,
            shippingState: address.State,
            shippingCountry: address.Country
        );

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        var order = result.Value;

        // Payment ==================================================================================================

        string checkoutUrl = "";

        if (orderCheckout.PaymentMethod == PaymentMethod.PayPal)
        {
            decimal totalAmount = order.GetTotalAmount();
            CreateOrderResponse response = await _payPalService.CreateOrderAsync(totalAmount, "CAD");

            order.SetExternalPaymentId(response.id);
            string? paypalCheckoutUrl = response.links.FirstOrDefault(link => link.rel == "payer-action")?.href;

            if (string.IsNullOrEmpty(paypalCheckoutUrl))
                return Result.Fail(DomainErrors.PayPal.CheckoutUrlNotFound);

            checkoutUrl = paypalCheckoutUrl;
        }

        await _orderRepository.CreateAsync(order);
        await _ecommerceService.ClearCartAsync(orderCheckout.OrderCheckoutItems.Select(i => i.ProductCombinationId));

        return Result.Ok(checkoutUrl);
    }

    public async Task<Result> ProcessPayPalReturnAsync(string token)
    {
        GetOrderResponse? paypalOrder = await _payPalService.GetOrderAsync(token);

        if (paypalOrder is null)
            return Result.Fail(DomainErrors.PayPal.OrderNotFound);

        if (paypalOrder.status != "APPROVED")
            return Result.Fail(DomainErrors.PayPal.OrderNotApproved);

        var order = await _orderRepository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            return Result.Fail(DomainErrors.Order.OrderNotFoundByExternalPaymentId);

        var result = order.CompletePayment();

        if (result.IsFailed)
            return result;

        await _orderRepository.UpdateAsync(order);

        return Result.Ok();
    }

    public async Task<Result> ProcessPayPalCancelAsync(string token)
    {
        var order = await _orderRepository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            return Result.Fail(DomainErrors.Order.OrderNotFoundByExternalPaymentId);

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
        return Result.Ok();
    }


}
