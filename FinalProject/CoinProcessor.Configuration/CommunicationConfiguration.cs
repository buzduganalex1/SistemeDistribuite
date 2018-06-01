namespace CoinProcessor.Configuration
{
    public abstract class CommunicationConfiguration : ICommunicationConfiguration
    {
        public string Name { get; set; }

        public string HostName { get; set; } = "localhost";

        public string ExchangeName { get; set; } = "topic_logs";

        public string ExchangeType { get; set; } = "topic";

        public string[] BindingKeys { get; set; } = { "#" };
    }
}