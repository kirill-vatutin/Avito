
using Avito.Application;
using RabbitMQ.Client;
using System.Text;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnectionFactory _factory;

    public RabbitMqPublisher(IConnectionFactory factory)
    {
        _factory = factory;
    }

    public void PublishPriceChange(string productName, double newPrice, string chatId)
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "price_change_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = $"{chatId}:{productName}:{newPrice}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "price_change_queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
