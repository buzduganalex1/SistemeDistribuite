using System.Collections.Generic;

namespace CoinProcessor.Middleware
{
    public static class SystemManager
    {
        public static List<Broker.Broker> Brokers = new List<Broker.Broker>();

        public static List<Publisher.Publisher> Publishers = new List<Publisher.Publisher>();

        public static List<Subscriber.Subscriber> Subscribers = new List<Subscriber.Subscriber>();
    }
}