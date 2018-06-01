using System;
using System.Linq;
using CoinProcessor.DataProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoinProvider.Publisher.Test
{
    [TestClass]
    public class DataProviderTest
    {
        private ICoidDataProvider dataProvider;
        
        [TestInitialize]
        public void Initialize()
        {
            dataProvider = new CoinDataProvider();
        }

        [TestMethod]
        public void ShouldReturnData()
        {
            var result = dataProvider.GetCoinData();

            Assert.IsTrue(result.Any());
            
            Console.WriteLine(result.Count());
        }
    }
}
