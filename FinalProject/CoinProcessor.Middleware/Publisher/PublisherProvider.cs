using System.Collections.Generic;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Publisher
{
    public class PublisherProvider : IPublisherProvider
    {
        public Publisher GetPublisher(ICommunicationConfiguration config)
        {
            var publisher = new Publisher(config);

            SystemManager.Publishers.Add(publisher);

            return publisher;
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            throw new System.NotImplementedException();
        }
    }
}