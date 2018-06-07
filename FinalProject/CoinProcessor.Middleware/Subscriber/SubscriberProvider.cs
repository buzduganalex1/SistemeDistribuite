using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Subscriber
{
    public class SubscriberProvider : ISubscriberProvider
    {
        public Subscriber Get(ICommunicationConfiguration configuration)
        {
            return new Subscriber(configuration);
        }
    }
}