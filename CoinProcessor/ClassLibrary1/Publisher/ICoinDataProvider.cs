using Publisher.Domain;
using System.Collections.Generic;

namespace Publisher
{
    public interface ICoinDataProvider
    {
        IEnumerable<object> GetCoinData();
    }
}