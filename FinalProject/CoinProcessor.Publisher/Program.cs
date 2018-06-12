using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Publisher;

namespace CoinProcessor.Publisher
{
    public class Program
    {
        public static void Main()
        {
            var publishers = GetPublishers(3);

            foreach (var publisher in publishers)
            {
                publisher.Start();
            }
            
            Task.WaitAll(publishers.ToArray());
        }

        private static IList<Task> GetPublishers(int number)
        {
            var tasks = new List<Task>();

            var publisherProvider = new PublisherProvider();

            for (var i = 1; i <= number; i++)
            {
                var taskId = i;

                tasks.Add(new Task(() =>
                {
                    var config = new PublisherConfiguration
                    {
                        Name = $"Publisher-{taskId}",
                        ExchangeName = EnpointConfigurationEnum.BrokerInput.ToString()
                    };

                    var publisher = publisherProvider.GetPublisher(config);

                    publisher.Publish(40000);
                }));
            }

            return tasks;
        }
    }
}
