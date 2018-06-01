using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Subscriber
{
    public interface ISubscriberProvider
    {
        Subscriber Get(ICommunicationConfiguration configuration);
    }
}