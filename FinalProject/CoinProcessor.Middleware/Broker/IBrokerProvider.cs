using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Broker
{
    public interface IBrokerProvider
    {
        Broker Get(ICommunicationConfiguration configuration);
    }
}