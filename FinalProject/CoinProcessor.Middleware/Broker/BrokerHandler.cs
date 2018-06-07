namespace CoinProcessor.Middleware.Broker
{
    public abstract class BrokerHandler
    {
        public virtual string BrokerHandlerKey { get; set; }

        public virtual string Key { get; set; }

        public string Handle(string message)
        {
            if (this.IsConditionMet(message))
            {
                return $"{Key}.{BrokerHandlerKey}";
            }

            return Key;
        }

        public virtual bool IsConditionMet(string message)
        {
            return true;
        }
    }
}