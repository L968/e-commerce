using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Consumers.OrderCheckout.Context;
using Ecommerce.Domain.Entities.CartEntities;
using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Domain.Entities.ProductEntities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "orderCheckout",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Process;

channel.BasicConsume(
    queue: "orderCheckout",
    autoAck: false,
    consumer: consumer
);

ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");

Console.ReadLine();

async void Process(object? model, BasicDeliverEventArgs ea)
{
    try
    {
        byte[] body = ea.Body.ToArray();
        string message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"\n [x] Received {message}");

        OrderCheckoutDto orderCheckout = JsonSerializer.Deserialize<OrderCheckoutDto>(message)!;

        var validator = new OrderCheckoutDtoValidator();
        var validatorResult = validator.Validate(orderCheckout);

        if (!validatorResult.IsValid)
        {
            Console.WriteLine("Invalid order checkout. Errors:");
            Console.WriteLine(string.Join(Environment.NewLine, validatorResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}")));
            return;
        }

        var cartItems = new List<CartItem>();

        foreach (var cartItemDto in orderCheckout.CartItems)
        {
            ProductCombination? productCombination = await GetProductCombinationById(cartItemDto.ProductCombinationId);

            if (productCombination is null)
            {
                Console.WriteLine($"Product combination {cartItemDto.ProductCombinationId} not found");
                return;
            }

            var createResult = CartItem.Create(
                cartId: cartItemDto.CartId,
                productCombinationId: cartItemDto.ProductCombinationId,
                quantity: cartItemDto.Quantity,
                isSelectedForCheckout: true,
                productCombination
            );

            if (createResult.IsFailed)
            {
                Console.WriteLine("Invalid cart item. Errors:");
                Console.WriteLine(string.Join(Environment.NewLine, createResult.Errors));
                return;
            }

            cartItems.Add(createResult.Value);
        }

        var result = Order.Create(
            orderCheckout.UserId,
            cartItems,
            orderCheckout.ShippingPostalCode,
            orderCheckout.ShippingStreetName,
            orderCheckout.ShippingBuildingNumber,
            orderCheckout.ShippingComplement,
            orderCheckout.ShippingNeighborhood,
            orderCheckout.ShippingCity,
            orderCheckout.ShippingState,
            orderCheckout.ShippingCountry
        );

        if (result.IsFailed)
        {
            Console.WriteLine(result.Errors[0]);
            return;
        }

        using var db = new AppDbContext();
        await db.Orders.AddAsync(result.Value);
        await db.SaveChangesAsync();

        channel.BasicAck(ea.DeliveryTag, false);

        Console.WriteLine("Order processed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nException: " + ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
}

async Task<ProductCombination?> GetProductCombinationById(Guid id)
{
    using var db = new AppDbContext();

    return await db
        .ProductCombinations
        .Include(p => p.Product)
        .ThenInclude(p => p.Discounts)
        .Include(p => p.Inventory)
        .Include(p => p.Images)
        .FirstOrDefaultAsync(p => p.Id == id);
}
