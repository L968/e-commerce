//using Ecommerce.Application.DTOs.OrderCheckout;
//using RabbitMQ.Client;
//using System.Text;
//using System.Text.Json;

//namespace Ecommerce.Order.API.RabbitMqClient;

//public class RabbitMqClient : IRabbitMqClient
//{
//    private readonly IConfiguration _configuration;
//    private readonly IConnection _connection;
//    private readonly IModel _channel;

//    public RabbitMqClient(IConfiguration configuration)
//    {
//        _configuration = configuration;
//        string hostName = _configuration["RabbitMqHost"]!;
//        int port = int.Parse(_configuration["RabbitMqPort"]!);

//        _connection = new ConnectionFactory()
//        {
//            HostName = hostName,
//            Port = port
//        }.CreateConnection();

//        _channel = _connection.CreateModel();
//        _channel.QueueDeclare(
//            queue: "orderCheckout",
//            durable: false,
//            exclusive: false,
//            autoDelete: false,
//            arguments: null
//        );
//    }

//    public void PublishOrder(OrderCheckoutDto order)
//    {
//        string message = JsonSerializer.Serialize(order);
//        byte[] body = Encoding.UTF8.GetBytes(message);

//        _channel.BasicPublish(
//            exchange: "",
//            routingKey: "orderCheckout",
//            basicProperties: null,
//            body: body
//        );
//    }
//}
