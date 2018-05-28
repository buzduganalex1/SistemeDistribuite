using System;
using System.Runtime.Serialization;

namespace Publisher.Domain
{
    public abstract class CoinModelKey
    {
        public virtual string Key { get { return "key.coin"; } }
    }
}