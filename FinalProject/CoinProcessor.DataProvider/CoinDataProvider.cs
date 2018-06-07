using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CoinProcessor.DataProvider
{
    public class CoinDataProvider : ICoidDataProvider
    {
        public IEnumerable<object> GetCoinData()
        {
            var filesToProcess = new string[] { "btc.json", "eth.json" };

            var coins = new List<object>();

            var relativePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\"));

            var localPath = "D:\\Projects\\GitHub\\SistemeDistribuite\\SistemeDistribuite\\FinalProject\\";

            foreach (var file in filesToProcess)
            {
                var path = $@"{localPath}\CoinProcessor.DataProvider\Data\{file}";

                using (var r = new StreamReader(path))
                {
                    var json = r.ReadToEnd();

                    coins.AddRange(JsonConvert.DeserializeObject<List<object>>(json));
                }
            }

            return coins;
        }
    }
}