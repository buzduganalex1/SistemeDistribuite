using CoinProcessor.CommunicationProvider;
using CoinProcessor.Configuration;

namespace CoinProcessor.Middleware.Subscriber
{
    public class Subscriber
    {
        public readonly ICommunicationConfiguration configuration;

        private readonly ICommunicationProvider communicationProvider;

        public Subscriber(ICommunicationConfiguration configuration)
        {
            this.configuration = configuration;

            this.communicationProvider = new CommunicationProvider.CommunicationProvider();
        }

        public void StartSubscription()
        {
            this.communicationProvider.Subscribe(configuration);
        }
    }
}