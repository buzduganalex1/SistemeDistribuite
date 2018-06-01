using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Broker;

namespace CoinProcessor.Broker
{
    public class Program
    {
        public static void Main()
        {
            var tasks = new List<Task>();

            for (int i = 0;  i < 1;  i++)
            {
                int taskId = i;

                tasks.Add(new Task(() =>
                {
                    var brokerProvider = new BrokerProvider();

                    var config = new BrokerConfiguration()
                    {
                        ExchangeName = "brokerInput",
                        Name = taskId.ToString()
                    };

                    var broker = brokerProvider.Get(config);

                    try
                    {
                        broker.Forward();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Broker {taskId} failed.");
                    }
                }));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
