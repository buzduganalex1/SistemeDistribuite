using System;
using System.Collections.Generic;
using CoinProcessor.Configuration;

namespace CoinProcessor.CommunicationProvider
{
    public interface ICommunicationProvider
    {
        void Publish(ICommunicationConfiguration config, List<object> dataList, int numberOfMessages = 0);
        
        void Publish(ICommunicationConfiguration config, string message, string key);

        void Subscribe(ICommunicationConfiguration config);

        void Intercept(ICommunicationConfiguration config, Action<string,string, string[]> forwardMessage);
    }
}