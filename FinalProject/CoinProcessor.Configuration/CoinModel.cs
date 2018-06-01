using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CoinProcessor.Configuration
{
    [DataContract]
    public class CoinModel : CoinModelKey
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        [JsonProperty(PropertyName = "txVolume(USD)")]
        public double Volume { get; set; }

        [JsonProperty(PropertyName = "txCount")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "marketcap(USD)")]
        public long MarketCap { get; set; }

        [JsonProperty(PropertyName = "price(USD)")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "exchangeVolume(USD)")]
        public long ExchangeVolume { get; set; }

        [JsonProperty(PropertyName = "generatedCoins")]
        public double GeneratedCoins { get; set; }

        [JsonProperty(PropertyName = "fees")]
        public double Fees { get; set; }

        public override string ToString()
        {
            return $"{date} {Volume} {Count} {MarketCap} {Price} {ExchangeVolume} {GeneratedCoins} {Fees}";
        }
    }
}