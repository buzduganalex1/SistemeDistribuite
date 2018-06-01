using System.Collections.Generic;
using System.Threading.Tasks;
using CoinProcessor.Configuration;
using CoinProcessor.Middleware.Publisher;

namespace CoinProcessor.Publisher
{
    public class Program
    {
        public static void Main()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 1; i++)
            {
                int taskId = i;

                tasks.Add(new Task(() =>
                {
                    var publisherProvider = new PublisherProvider();

                    var config = new PublisherConfiguration
                    {
                        Name = $"Publisher-{taskId}",
                        ExchangeName = "brokerInput"
                    };

                    var publisher = publisherProvider.GetPublisher(config);

                    publisher.Publish();
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
