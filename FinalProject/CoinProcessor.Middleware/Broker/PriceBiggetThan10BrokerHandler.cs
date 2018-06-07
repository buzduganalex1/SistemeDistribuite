using CoinProcessor.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinProcessor.Middleware.Broker
{
    public class PriceBiggetThan10BrokerHandler : BrokerHandler
    {
        public override string BrokerHandlerKey => "biggerThan10";

        public override bool IsConditionMet(string message)
        {
            var coinModel = JsonConvert.DeserializeObject(message);

            var token = coinModel as JToken;

            if (token == null)
            {
                return false;
            }

            var wrapper = token.ToObject<Wrapper<object>>();

            var coinModelWrapper = wrapper.Message as JToken;

            var price = coinModelWrapper["price(USD)"].ToObject<double>();

            return price > 100;
        }
    }
}