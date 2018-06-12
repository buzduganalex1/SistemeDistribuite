using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Subscriber;

namespace CoinProcessor.Subscriber
{
    class Program
    {
        public static void Main(string[] args)
        {
            var subscriberProvider = new SubscriberProvider();
            var config = new SubscriberConfiguration
            {
                ExchangeName = EnpointConfigurationEnum.BrokerOutput.ToString(),
                BindingKeys = new[] { "*.*.*.dateKey" }
            };
            
            subscriberProvider.Get(config).StartSubscription();

            ////if (args.Any())
            ////{
            ////    Console.WriteLine("Parameters");



            ////    //

            ////    args.ToList().ForEach(Console.WriteLine);
                
            ////}
            ////else
            ////{
            ////    throw new Exception("Parameters missing.");
            ////}
        }
    }
}
