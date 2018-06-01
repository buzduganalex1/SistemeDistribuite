using System.Linq;
using CoinProcessor.CommunicationProvider;
using CoinProcessor.Configuration;
using CoinProcessor.DataProvider;

namespace CoinProcessor.Middleware.Publisher
{
    public class Publisher
    {
        private readonly ICoidDataProvider dataProvider;

        private readonly ICommunicationProvider communicationProvider;

        public readonly ICommunicationConfiguration config;

        public Publisher(ICommunicationConfiguration config)
        {
            dataProvider = new CoinDataProvider();
           
            communicationProvider = new CommunicationProvider.CommunicationProvider();

            this.config = config;
        }

        public void Publish()
        {
            var data = dataProvider.GetCoinData();

            communicationProvider.Publish(this.config, data.ToList());
        }
    }
}