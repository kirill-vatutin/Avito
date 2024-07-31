namespace Avito.Application
{
    public interface IRabbitMqPublisher
    {
        void PublishPriceChange(string productName, double newPrice, string chatId);
    }
}
