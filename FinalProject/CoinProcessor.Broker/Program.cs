using System;
using System.Collections.Generic;
using System.Linq;
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

            var brokerProvider = new BrokerProvider();

            var initialBrokerConfig = new BrokerConfiguration
            {
                ExchangeName = "brokerInput",
                Name = "InputBroker"
            };

            var amountBiggerThan10BrokerConfig = new BrokerConfiguration()
            {
                ExchangeName = "AmountBiggerThan10",
                Name = "AmountBiggerThan10Broker"
            };

            var yearConfig = new BrokerConfiguration()
            {
                ExchangeName = "yearConfig",
                Name = "yearConfigBroker"
            };

            var task1 = new Task(() =>
            {
                var initialBroker = brokerProvider.Get(initialBrokerConfig);
                
                initialBroker.Initiate(amountBiggerThan10BrokerConfig, new PriceBiggetThan10BrokerHandler());
            });

            var task2 = new Task(() =>
            {
                var amountBiggerThan10Broker = brokerProvider.Get(amountBiggerThan10BrokerConfig);

                amountBiggerThan10Broker.Initiate(yearConfig, new YearBiggerThan2017BrokerHandler());
            });

            var task3 = new Task(() =>
            {
                var amountBiggerThan10Broker = brokerProvider.Get(yearConfig);

                amountBiggerThan10Broker.Initiate();
            });

            tasks.Add(task1);

            tasks.Add(task2);

            tasks.Add(task3);

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
            
        }

        ////private static IList<Task> GetBrokerTasks(int number)
        ////{
        ////    var tasks = new List<Task>();

        ////    var brokerProvider = new BrokerProvider();

        ////    for (var i = 1; i <= number; i++)
        ////    {
        ////        var taskId = i;

        ////        tasks.Add(new Task(() =>
        ////        {
        ////            var config = new BrokerConfiguration
        ////            {
        ////                ExchangeName = "brokerInput",
        ////                Name = taskId.ToString()
        ////            };

        ////            var broker = brokerProvider.Get(config);

        ////            try
        ////            {
        ////                broker.Initiate();
        ////            }
        ////            catch (Exception)
        ////            {
        ////                Console.WriteLine($"Broker {taskId} failed.");
        ////            }
        ////        }));
        ////    }

        ////    return tasks;
        ////}
    }
}
