namespace Avito.Application
{
    public interface IRabbitMqPublisher
    {
        void PublishPriceChange(string productName, decimal newPrice, string chatId);
    }
}
