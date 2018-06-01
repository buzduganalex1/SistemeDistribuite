using System;
using System.Threading;
using CoinProcessor.CommunicationProvider;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Broker
{
    public class Broker
    {
        private readonly ICommunicationConfiguration config;

        public readonly ICommunicationProvider communicationProvider; 

        public Broker(ICommunicationConfiguration config)
        {
            this.config = config;

            communicationProvider = new CommunicationProvider.CommunicationProvider();
        }

        public void Forward()
        {
            void Action(string message, string key, string[] bindings)
            {
                var subConfig = new SubscriberConfiguration
                {
                    ExchangeName = "brokerOutput",
                    BindingKeys =  bindings
                };

                communicationProvider.Publish(subConfig, message, key);
            }

            communicationProvider.Intercept(config, Action);

            ////var random = new Random();

            ////if (random.Next(10) % 2 == 0)
            ////{
            ////    Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);

            ////    throw new Exception();
            ////}
        }
    }
}