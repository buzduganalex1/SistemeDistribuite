using System;
using System.Collections.Generic;
using System.Text;
using CoinProcessor.Configuration;
using CoinProcessor.Middleware;
using CoinProcessor.Middleware.Publisher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoinProvider.Publisher.Test
{
    [TestClass]
    public class PublisherProviderTest
    {
        private IPublisherProvider publisherProvider;

        [TestInitialize]
        public void Initialize()
        {
            this.publisherProvider = new PublisherProvider();
        }

        [TestMethod]
        public void ShouldReturnAPublisher()
        {
            var result = publisherProvider.GetPublisher(new PublisherConfiguration());

            Assert.AreEqual("localhost", result.config.HostName);

            Assert.AreEqual("topic_logs", result.config.ExchangeName);

            Assert.AreEqual("topic", result.config.ExchangeType);
        }
    }
}
