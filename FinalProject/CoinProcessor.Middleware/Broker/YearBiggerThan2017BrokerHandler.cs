using System;
using CoinProcessor.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinProcessor.Middleware.Broker
{
    public class YearBiggerThan2017BrokerHandler : BrokerHandler
    {
        public override string BrokerHandlerKey => "dateKey";

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

            var date = coinModelWrapper["date"].ToObject<DateTime>();

            return date.Year > 2017;
        }
    }
}
