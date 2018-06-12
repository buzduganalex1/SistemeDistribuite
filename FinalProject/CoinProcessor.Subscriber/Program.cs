using System;
using System.Linq;
using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Subscriber;

namespace CoinProcessor.Subscriber
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Any())
            {
                Console.WriteLine("Parameters");

                var subscriberProvider = new SubscriberProvider();
                var config = new SubscriberConfiguration
                {
                    ExchangeName = EnpointConfigurationEnum.BrokerOutput.ToString(),
                    BindingKeys = args
                };
                args.ToList().ForEach(Console.WriteLine);
                subscriberProvider.Get(config).StartSubscription();
            }
            else
            {
                throw new Exception("Parameters missing.");
            }
        }
    }
}
