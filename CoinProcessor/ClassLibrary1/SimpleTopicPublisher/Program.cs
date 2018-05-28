using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace SimpleTopicPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs2",
                                        type: "topic");

                args = new string[] { "test.coin" };

                var random = new Random();

                while (true)
                {
                    var routingKey = (args.Length > 0) ? args[random.Next(args.Length)] : "anonymous.info";

                    var message = "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "topic_logs2",
                                         routingKey: routingKey,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                }
            }
        }
    }
}
