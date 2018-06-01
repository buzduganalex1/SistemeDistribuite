namespace CoinProcessor.Configuration
{
    public interface ICommunicationConfiguration
    {
        string Name { get; set; }

        string HostName { get; set; }

        string ExchangeName { get; set; }

        string ExchangeType { get; set; }

        string[] BindingKeys { get; set; }
    }
}