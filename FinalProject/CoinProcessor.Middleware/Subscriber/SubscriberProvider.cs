using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Subscriber
{
    public class SubscriberProvider : ISubscriberProvider
    {
        public Subscriber Get(ICommunicationConfiguration configuration)
        {
            var subscriber = new Subscriber(configuration);

            SystemManager.Subscribers.Add(subscriber);

            return subscriber;
        }
    }
}