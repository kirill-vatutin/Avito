using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

public class TelegramBotService
{
    private readonly TelegramBotClient _botClient;

   
    public TelegramBotService(string botToken)
    {
        _botClient = new TelegramBotClient(botToken);
    }

    public async Task HandleStartCommand(Update update)
    {
        if (update.Message?.Text == "/start")
        {
            long chatId = update.Message.From.Id; // Получение chatId
            string userName = update.Message.From.Username;
            

            // Здесь вы можете сохранить chatId в базе данных
           // await StoreChatIdInDatabase(chatId.ToString(), userName);

            string welcomeMessage = $"Добро пожаловать, {userName}, ваш chat id: {chatId}!";
            await _botClient.SendTextMessageAsync(chatId, welcomeMessage);
        }
    }

    public async Task StartPollingAsync(CancellationToken cancellationToken)
    {
        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"Bot id: {me.Id}. Bot name: {me.FirstName}.");

        var offset = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            var updates = await _botClient.GetUpdatesAsync(offset, cancellationToken: cancellationToken);

            foreach (var update in updates)
            {
                offset = update.Id + 1;
                await HandleStartCommand(update); // Передаем обновление в обработчик
            }

            await Task.Delay(1000, cancellationToken); // Задержка между запросами
        }
    }

    public async Task SendPriceChangeNotificationAsync(string chatId, string message)
    {
    
        long chatIdLong = long.Parse(chatId);

        await _botClient.SendTextMessageAsync(chatIdLong, message);
    }

    // Дополнительные методы для обработки команд и обновлений
    // public async Task HandleUpdateAsync(Update update) { ... }
}
