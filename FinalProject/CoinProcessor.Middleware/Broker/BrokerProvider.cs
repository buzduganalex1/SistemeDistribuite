using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Broker
{
    public class BrokerProvider : IBrokerProvider
    {
        public Broker Get(ICommunicationConfiguration configuration)
        {
            return new Broker(configuration);
        }
    }
}