using Newtonsoft.Json;
using Publisher.Domain;
using System.Collections.Generic;
using System.IO;

namespace Publisher
{
    public class CoinDataProvider : ICoinDataProvider
    {
        public IEnumerable<object> GetCoinData()
        {
            var filesToProcess = new string[] { "btc.json", "eth.json" };

            List<object> coins = new List<object>();

            foreach(var file in filesToProcess)
            {
                var path = $@"D:\Projects\GitHub\Tests\RabbitMq\ClassLibrary1\{file}";

                using (StreamReader r = new StreamReader(path))
                {
                    var json = r.ReadToEnd();

                    coins.AddRange(JsonConvert.DeserializeObject<List<MessageWrapper<object>>>(json));
                }
            }

            return coins;
        }
    }
}
