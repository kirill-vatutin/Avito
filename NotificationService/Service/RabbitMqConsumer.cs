using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Telegram.Bot;

public class RabbitMqConsumer
{
    private readonly TelegramBotService _telegramBotService;

    public RabbitMqConsumer(TelegramBotService telegramBotService)
    {
        _telegramBotService = telegramBotService;
    }

    public void StartConsuming()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "price_change_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var parts = message.Split(':');
                var chatId = parts[0];
                var productName = parts[1];
                var newPrice = parts[2];

                var notificationMessage = $"Цена на товар {productName} изменена на {newPrice}.";

                await _telegramBotService.SendPriceChangeNotificationAsync(chatId, notificationMessage);
            };
            channel.BasicConsume(queue: "price_change_queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }


}
