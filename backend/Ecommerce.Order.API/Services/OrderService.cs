using AutoMapper;
using Ecommerce.Common.Infra.Representation;
using Ecommerce.Common.Infra.Representation.Grid;
using Ecommerce.Domain.DTOs;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Errors;
using Ecommerce.Order.API.Interfaces;
using Ecommerce.Order.API.Models.PayPal;
using Ecommerce.Order.API.Repositories;

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

    public async Task<Pagination<OrderDto>> GetAllAsync(GridParams gridParams)
    {
        var (orders, totalItems) = await _orderRepository.GetAllAsync(gridParams);

        return new Pagination<OrderDto>(
            gridParams.Page,
            gridParams.PageSize,
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

    public async Task<string> CreateOrderAsync(OrderCheckoutDto orderCheckout)
    {
        CreateOrderAddressDto? address = await _ecommerceService.GetAddressByIdAsync(orderCheckout.ShippingAddressId);
        DomainException.ThrowIfNull(address, orderCheckout.ShippingAddressId);

        var cartItems = new List<CreateOrderCartItemDto>();

        foreach (var cartItemDto in orderCheckout.OrderCheckoutItems)
        {
            CreateOrderProductCombinationDto? productCombinationDto = await _ecommerceService.GetProductCombinationByIdAsync(cartItemDto.ProductCombinationId);
            DomainException.ThrowIfNull(productCombinationDto, cartItemDto.ProductCombinationId);

            if (!productCombinationDto.Product.Active)
                throw new DomainException(DomainErrors.Product.InactiveProduct(productCombinationDto.Product.Id));

            ProductCombination productCombination = _mapper.Map<ProductCombination>(productCombinationDto);

            cartItems.Add(new CreateOrderCartItemDto
            {
                ProductCombination = productCombination,
                Quantity = cartItemDto.Quantity
            });
        }

        var order = Domain.Entities.OrderEntities.Order.Create(
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

        // Payment ==================================================================================================

        string checkoutUrl = "";

        if (orderCheckout.PaymentMethod == PaymentMethod.PayPal)
        {
            decimal totalAmount = order.GetTotalAmount();
            CreateOrderResponse response = await _payPalService.CreateOrderAsync(totalAmount, "CAD");

            order.SetExternalPaymentId(response.id);
            string? paypalCheckoutUrl = response.links.FirstOrDefault(link => link.rel == "payer-action")?.href;

            if (string.IsNullOrEmpty(paypalCheckoutUrl))
                throw new DomainException(DomainErrors.PayPal.CheckoutUrlNotFound);

            checkoutUrl = paypalCheckoutUrl;
        }

        await _orderRepository.CreateAsync(order);
        await _ecommerceService.ClearCartAsync(orderCheckout.OrderCheckoutItems.Select(i => i.ProductCombinationId));

        return checkoutUrl;
    }

    public async Task ProcessPayPalReturnAsync(string token)
    {
        GetOrderResponse? paypalOrder = await _payPalService.GetOrderAsync(token);

        if (paypalOrder is null)
            throw new DomainException(DomainErrors.PayPal.OrderNotFound);

        if (paypalOrder.status != "APPROVED")
            throw new DomainException(DomainErrors.PayPal.OrderNotApproved);

        var order = await _orderRepository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            DomainException.ThrowIfNull(order, DomainErrors.Order.OrderNotFoundByExternalPaymentId, token);

        order.CompletePayment();

        await _orderRepository.UpdateAsync(order);
    }

    public async Task ProcessPayPalCancelAsync(string token)
    {
        var order = await _orderRepository.GetByExternalPaymentIdAsync(token);

        if (order is null)
            DomainException.ThrowIfNull(order, DomainErrors.Order.OrderNotFoundByExternalPaymentId, token);

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
    }
}
