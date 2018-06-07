using System;
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

        public void Initiate(BrokerConfiguration nextBrokerConfiguration = null, BrokerHandler handler = null)
        {
            var nextBrokerConfigurationVar = nextBrokerConfiguration;

            var brokerHandlerVar = handler;

            void Action(string message, string key, string[] bindings)
            {
                if (nextBrokerConfigurationVar != null)
                {
                    nextBrokerConfigurationVar.BindingKeys = bindings;

                    handler.Key = key;
                    
                    var newKey = brokerHandlerVar.Handle(message);

                    communicationProvider.Publish(nextBrokerConfiguration, message, newKey);

                    Console.WriteLine($"Broker {nextBrokerConfigurationVar.Name} sent message with key {newKey}");
                }
                else
                {
                    var subConfig = new SubscriberConfiguration
                    {
                        ExchangeName = "brokerOutput",
                        BindingKeys = bindings
                    };

                    communicationProvider.Publish(subConfig, message, key);
                    
                    Console.WriteLine($"Broker {subConfig.Name} sent message with key {key}");
                }             
            }

            Console.WriteLine($" Broker {config.Name} started. ");

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