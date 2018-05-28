using System;
using System.Runtime.Serialization;

namespace Publisher.Domain
{
    [KnownType(typeof(BitcoinModel))]
    public class BitcoinModel : CoinModel
    {
        public override string Key => "key.bitcoin";
    }
}
