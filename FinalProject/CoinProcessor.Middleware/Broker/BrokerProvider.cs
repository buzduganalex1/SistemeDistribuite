using System;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Broker
{
    public class BrokerProvider : IBrokerProvider
    {
        public Broker Get(ICommunicationConfiguration configuration)
        {
            var broker = new Broker(configuration);

            SystemManager.Brokers.Add(broker);

            return broker;
        }
    }
}