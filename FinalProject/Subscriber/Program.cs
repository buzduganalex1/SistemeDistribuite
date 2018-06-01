using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Subscriber;

namespace CoinProcessor.Subscriber
{
    class Program
    {
        public static void Main(string[] args)
        {
            var subscriberProvider = new SubscriberProvider();
            
            var config = new SubscriberConfiguration()
            {
                ExchangeName = "brokerOutput",
                BindingKeys = new[] {"#"},
                Name = "0"
            };

            var subscriber = subscriberProvider.Get(config);

            subscriber.Subscribe();

        }
    }
}
