using System;
using System.Collections.Generic;
using System.Text;
using CoinProcessor.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoinProcessor.CommunicationProvider
{
    public class CommunicationProvider : ICommunicationProvider
    {
        public void Publish(ICommunicationConfiguration config, List<object> dataList, int numberOfMessages)
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

                    if (numberOfMessages > 0)
                    {
                        while (numberOfMessages > 0)
                        {
                            var random = new Random();

                            var data = dataList[random.Next(dataList.Count)];

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

                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(config.ExchangeName, key, null, body);

                            Console.WriteLine($"{key} \n {numberOfMessages} \n");

                            numberOfMessages--;
                        }
                    }
                    else
                    {
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

                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(config.ExchangeName, key, null, body);

                            Console.WriteLine($"{key} \n {message} \n");

                            ////Thread.Sleep(TimeSpan.FromSeconds(1).Milliseconds);
                        }
                    }         
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

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(config.ExchangeName, key, null, body);
                }
            }
        }

        public void Subscribe(ICommunicationConfiguration config)
        {
            var logger = new Logger.Logger();

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

                    var numberOfMessagesReceived = 1;

                    double totalMilliseconds = 0;

                    consumer.Received += (model, ea) =>
                    {
                        var receivedTime = DateTime.Now;

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;

                        var deserializeObject = JsonConvert.DeserializeObject(message);

                        var token = deserializeObject as JToken;

                        var sendingDateTime = token["sendingDateTime"].ToObject<DateTime>();

                        totalMilliseconds += (receivedTime - sendingDateTime).TotalMilliseconds;

                        var totalAmountOfTime =
                            TimeSpan.FromMilliseconds(totalMilliseconds / numberOfMessagesReceived).TotalSeconds;

                        logger.Log($"{numberOfMessagesReceived++}. SendingTime: {sendingDateTime} ReceivedAt: {receivedTime} \n TotalTime: {totalAmountOfTime}");

                        Console.WriteLine(
                            $"{numberOfMessagesReceived++}. {sendingDateTime} {routingKey} \n {message} \n {totalAmountOfTime}");
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

                    string queueName;

                    if (config.ExchangeName == EnpointConfigurationEnum.BrokerInput.ToString())
                    {
                        queueName = channel.QueueDeclare("inputQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null).QueueName;
                    }
                    else
                    {
                        queueName = channel.QueueDeclare(config.ExchangeName.ToUpper(),
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null).QueueName;
                    }
                    
                    foreach (var bindingKey in config.BindingKeys)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: config.ExchangeName,
                            routingKey: bindingKey);
                    }
                    
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var exchange = ea.Exchange;
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;

                        forwardMessage(message, routingKey, config.BindingKeys);
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