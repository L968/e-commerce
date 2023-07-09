using Ecommerce.Application.Features.Orders.Commands.OrderCheckout;
using Ecommerce.Infra.IoC;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

var serviceProvider = new ServiceCollection()
            .AddInfrastructure(configuration)
            .AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
            .BuildServiceProvider();

var mediator = serviceProvider.GetService<IMediator>()!;

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

Console.ReadLine();

void Process(object? model, BasicDeliverEventArgs ea)
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

    var command = JsonSerializer.Deserialize<OrderCheckoutCommand>(message)!;
    mediator.Send(command);
}