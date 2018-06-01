using System.Collections.Generic;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Publisher
{
    public interface IPublisherProvider
    {
        Publisher GetPublisher(ICommunicationConfiguration config);

        IEnumerable<Publisher> GetPublishers();
    }
}