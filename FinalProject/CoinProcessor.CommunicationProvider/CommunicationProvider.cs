
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CoinProcessor.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoinProcessor.CommunicationProvider
{
    public class CommunicationProvider : ICommunicationProvider
    {
        public void Publish(ICommunicationConfiguration config, List<object> dataList)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.HostName
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(config.ExchangeName, config.ExchangeType);

                    foreach (var data in dataList)
                    {
                        var token = data as JToken;

                        var key = new CoinModel().Key;

                        var coin = token.ToObject<EtherModel>();

                        if (coin.ActiveAdresses > 0)
                        {
                            key = coin.Key;
                        }
                        else
                        {
                            var bitcoin = token.ToObject<BitcoinModel>();

                            key = bitcoin.Key;
                        }

                        var objectToBeSerialized = new Wrapper<object>(coin)
                        {
                            sendingDateTime = DateTime.Now
                        };

                        var message = JsonConvert.SerializeObject(objectToBeSerialized);

                        var body = System.Text.Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(config.ExchangeName, key, null, body);

                        ////Thread.Sleep(TimeSpan.FromSeconds(1).Milliseconds);
                    }


                    Console.WriteLine($" [x]  sent message");
                }
            }
        }

        public void Publish(ICommunicationConfiguration config, string message, string key)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.HostName
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(config.ExchangeName, config.ExchangeType);

                    //send data with key

                    var body = System.Text.Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(config.ExchangeName, key, null, body);

                    Console.WriteLine($" [x] sent message");
                }
            }
        }

        public void Subscribe(ICommunicationConfiguration config)
        {
            var factory = new ConnectionFactory { HostName = config.HostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName,
                        type: config.ExchangeType);

                    var queueName = channel.QueueDeclare().QueueName;

                    foreach (var bindingKey in config.BindingKeys)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: config.ExchangeName,
                            routingKey: bindingKey);
                    }

                    Console.WriteLine($" Subscriber {config.Name} waiting for message. ");

                    var consumer = new EventingBasicConsumer(channel);

                    var counter = 1;
                    
                    consumer.Received += (model, ea) =>
                    {
                        var nowTime = DateTime.Now;

                        double totalTime = 0;

                        if (counter < 10000)
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            var routingKey = ea.RoutingKey;

                            var test = JsonConvert.DeserializeObject(message);

                            JToken token = test as JToken;

                            var sendingDateTime = token["sendingDateTime"].ToObject<DateTime>();

                            var deserializedObject = token["Message"];

                            Console.WriteLine($" {counter++}. {deserializedObject} at {sendingDateTime}");

                            totalTime += (nowTime - sendingDateTime).TotalMilliseconds;

                            Console.WriteLine(totalTime);
                        }
                    };

                    channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);

                    Console.ReadLine();
                }
            }
        }

        public void Intercept(ICommunicationConfiguration config, Action<string, string, string[]> forwardMessage)
        {
            var factory = new ConnectionFactory { HostName = config.HostName };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName,
                        type: config.ExchangeType);

                    var queueName = channel.QueueDeclare("test",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null).QueueName;

                    foreach (var bindingKey in config.BindingKeys)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: config.ExchangeName,
                            routingKey: bindingKey);
                    }

                    Console.WriteLine($" Broker {config.Name} started. ");

                    var consumer = new EventingBasicConsumer(channel);

                    var counter = 1;

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;

                        forwardMessage(message, routingKey, config.BindingKeys);

                        Console.WriteLine($" {counter++} '{message}'");
                    };

                    channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}