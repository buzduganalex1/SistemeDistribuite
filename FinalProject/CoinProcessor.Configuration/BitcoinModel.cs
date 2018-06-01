using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CoinProcessor.Configuration
{
    [KnownType(typeof(BitcoinModel))]
    public class BitcoinModel : CoinModel
    {
        public override string Key => "key.bitcoin";
    }
}
