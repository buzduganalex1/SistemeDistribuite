using System.Collections.Generic;

namespace CoinProcessor.DataProvider
{
    public interface ICoidDataProvider
    {
        IEnumerable<object> GetCoinData();
    }
}