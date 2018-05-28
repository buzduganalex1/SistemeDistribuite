﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
                var queueName = channel.QueueDeclare().QueueName;

                args = new string[] { "#" };

                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Usage: {0} [binding_key...]",
                                            Environment.GetCommandLineArgs()[0]);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var bindingKey in args)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "topic_logs",
                                      routingKey: bindingKey);
                }

                Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'",
                                      routingKey,
                                      message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        private static void ReceiveForPublisher1()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void ReceiveForPublisher2()
        {
            ////    var task = Task.Factory.StartNew(() =>
            ////    {
            ////        var factory = new ConnectionFactory() { HostName = "localhost" };
            ////        using (var connection = factory.CreateConnection())
            ////        using (var channel = connection.CreateModel())
            ////        {
            ////            channel.ExchangeDeclare(exchange: "topic_logs2", type: "topic");
            ////            var queueName = channel.QueueDeclare().QueueName;

            ////            args = new string[] { "#" };

            ////            if (args.Length < 1)
            ////            {
            ////                Console.Error.WriteLine("Usage: {0} [binding_key...]",
            ////                                        Environment.GetCommandLineArgs()[0]);
            ////                Console.WriteLine(" Press [enter] to exit.");
            ////                Console.ReadLine();
            ////                Environment.ExitCode = 1;
            ////                return;
            ////            }

            ////            foreach (var bindingKey in args)
            ////            {
            ////                channel.QueueBind(queue: queueName,
            ////                                  exchange: "topic_logs2",
            ////                                  routingKey: bindingKey);
            ////            }

            ////            Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

            ////            var consumer = new EventingBasicConsumer(channel);
            ////            consumer.Received += (model, ea) =>
            ////            {
            ////                var body = ea.Body;
            ////                var message = Encoding.UTF8.GetString(body);
            ////                var routingKey = ea.RoutingKey;
            ////                Console.WriteLine(" [x] Received '{0}':'{1}'",
            ////                                  routingKey);
            ////            };
            ////            channel.BasicConsume(queue: queueName,
            ////                                 autoAck: true,
            ////                                 consumer: consumer);

            ////            Console.WriteLine(" Press [enter] to exit.");
            ////            Console.ReadLine();
            ////        }
            ////    });

            ////    task.Wait();
            ////}
        }
    }
}

