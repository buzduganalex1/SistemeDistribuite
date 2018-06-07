using System.Collections.Generic;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Publisher
{
    public class PublisherProvider : IPublisherProvider
    {
        public Publisher GetPublisher(ICommunicationConfiguration config)
        {
            return new Publisher(config);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            throw new System.NotImplementedException();
        }
    }
}