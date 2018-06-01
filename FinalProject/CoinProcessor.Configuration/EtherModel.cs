﻿using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CoinProcessor.Configuration
{
    [KnownType(typeof(EtherModel))]
    public class EtherModel : CoinModel
    {
        [JsonProperty(PropertyName = "activeAddresses")]
        public int ActiveAdresses { get; set; }

        [JsonProperty(PropertyName = "medianTxValue(USD)")]
        public double MedianTax { get; set; }

        [JsonProperty(PropertyName = "medianFee")]
        public double MedianFree { get; set; }

        [JsonProperty(PropertyName = "averageDifficulty")]
        public long AverageDificulty { get; set; }

        [JsonProperty(PropertyName = "paymentCount")]
        public int paymentCount { get; set; }

        public override string Key => "key.ether";
    }
}