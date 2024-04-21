using Ecommerce.Consumers.OrderCheckout.Context;
using Ecommerce.Domain.Entities;
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

WriteLine(" [*] Waiting for messages.", ConsoleColor.Magenta);

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
        WriteLine($"\n [x] Received {message}", ConsoleColor.Cyan);

        //OrderCheckoutDto orderCheckout = JsonSerializer.Deserialize<OrderCheckoutDto>(message)!;

        //var validator = new OrderCheckoutDtoValidator();
        //var validatorResult = validator.Validate(orderCheckout);

        //if (!validatorResult.IsValid)
        //{
        //    WriteLine("Invalid order checkout. Errors:", ConsoleColor.Red);
        //    WriteLine(string.Join(Environment.NewLine, validatorResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}")), ConsoleColor.Red);
        //    return;
        //}

        //Address? address = await GetAddressById(orderCheckout.ShippingAddressId, orderCheckout.UserId);

        //if (address == null)
        //{
        //    WriteLine($"Address with id {orderCheckout.ShippingAddressId} not found", ConsoleColor.Red);
        //    return;
        //}

        //var cartItems = new List<CartItem>();

        //foreach (var cartItemDto in orderCheckout.OrderCheckoutItems)
        //{
        //    ProductCombination? productCombination = await GetProductCombinationById(cartItemDto.ProductCombinationId);

        //    if (productCombination is null)
        //    {
        //        WriteLine($"Product combination {cartItemDto.ProductCombinationId} not found", ConsoleColor.Red);
        //        return;
        //    }

        //    if (!productCombination.Product.Active)
        //    {
        //        WriteLine($"Product combination {productCombination.Product.Id} is inactive", ConsoleColor.Red);
        //        return;
        //    }

        //    var createResult = CartItem.Create(
        //        cartId: Guid.Empty,
        //        productCombinationId: cartItemDto.ProductCombinationId,
        //        quantity: cartItemDto.Quantity,
        //        isSelectedForCheckout: true,
        //        productCombination
        //    );

        //    if (createResult.IsFailed)
        //    {
        //        WriteLine("Invalid cart item. Errors:", ConsoleColor.Red);
        //        Console.WriteLine(string.Join(Environment.NewLine, createResult.Errors));
        //        return;
        //    }

        //    cartItems.Add(createResult.Value);
        //}

        //var result = Order.Create(
        //    orderCheckout.UserId,
        //    cartItems,
        //    orderCheckout.PaymentMethod,
        //    orderCheckout.ExternalPaymentId,
        //    address.PostalCode,
        //    address.StreetName,
        //    address.BuildingNumber,
        //    address.Complement,
        //    address.Neighborhood,
        //    address.City,
        //    address.State,
        //    address.Country
        //);

        //if (result.IsFailed)
        //{
        //    Console.WriteLine(result.Errors[0]);
        //    return;
        //}

        //ClearCart(orderCheckout.UserId);

        //using var db = new AppDbContext();
        //await db.Orders.AddAsync(result.Value);
        //await db.SaveChangesAsync();

        channel.BasicAck(ea.DeliveryTag, false);
        WriteLine("Order processed successfully", ConsoleColor.Green);
    }
    catch (Exception ex)
    {
        WriteLine("\nException: " + ex.Message, ConsoleColor.Red);

        if (ex.InnerException is not null)
        {
            WriteLine("\nInnerException: " + ex.InnerException.Message, ConsoleColor.Red);
        }

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

async Task<Address?> GetAddressById(Guid id, int userId)
{
    using var db = new AppDbContext();
    return await db.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
}

async void ClearCart(int userId)
{
    using var db = new AppDbContext();
    var cart = await db.Carts
        .Include(c => c.CartItems)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart is null)
        throw new Exception($"Cart not found for user {userId}");

    cart.ClearItems();
    await db.SaveChangesAsync();
}

void WriteLine(string? message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}
