public class RabbitMqBackgroundService : IHostedService
{
    private readonly TelegramBotService _telegramBotService;
    private RabbitMqConsumer _consumer;

    public RabbitMqBackgroundService(TelegramBotService telegramBotService)
    {
        _telegramBotService = telegramBotService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => _telegramBotService.StartPollingAsync(cancellationToken), cancellationToken);
        _consumer = new RabbitMqConsumer(_telegramBotService);
        Task.Run(() => _consumer.StartConsuming());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Логика для завершения работы
        return Task.CompletedTask;
    }
}

