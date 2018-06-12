using System.Collections.Generic;
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
                ExchangeName = EnpointConfigurationEnum.BrokerInput.ToString(),
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

            tasks.Add(new Task(() =>
            {
                var initialBroker = brokerProvider.Get(initialBrokerConfig);

                initialBroker.Initiate();
            }));


            tasks.Add(new Task(() =>
            {
                var initialBroker = brokerProvider.Get(initialBrokerConfig);

                initialBroker.Initiate(amountBiggerThan10BrokerConfig, new PriceBiggetThan10BrokerHandler());
            }));

            tasks.Add(new Task(() =>
            {
                var amountBiggerThan10Broker = brokerProvider.Get(amountBiggerThan10BrokerConfig);

                amountBiggerThan10Broker.Initiate(yearConfig, new YearBiggerThan2017BrokerHandler());
            }));

            tasks.Add(new Task(() =>
            {
                var amountBiggerThan10Broker = brokerProvider.Get(yearConfig);

                amountBiggerThan10Broker.Initiate();
            }));

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
