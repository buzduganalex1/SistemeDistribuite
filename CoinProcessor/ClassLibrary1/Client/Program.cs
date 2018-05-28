using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Publisher.Domain;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static HttpClient httpClient;

        static void Main(string[] args)
        {
            httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:52122/api/values") };

            var request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress);

            var result = GetResponse(request).Result;

            var text = GetResponseContent(result.Content).Result;

            var list = JsonConvert.DeserializeObject<List<object>>(text);

            var tasks = new List<Task<int>>();

            for (int i = 0; i < 3; i++)
            {
                tasks.Add(Publish(list, 100000, i));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static async Task<HttpResponseMessage> GetResponse(HttpRequestMessage request)
        {
            return await httpClient.SendAsync(request);
        }

        private static async Task<string> GetResponseContent(HttpContent content)
        {
            return await content.ReadAsStringAsync();
        }

        private static async Task<int> Publish(List<object> publicationFeed, int messagesCount, int taskNumber)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

                    int counter = messagesCount;

                    var rnd = new Random();

                    while (counter > 0)
                    {
                        int r = rnd.Next(publicationFeed.Count);

                        var type = publicationFeed[r].GetType();

                        var message = publicationFeed[r];

                        var coinModelJson = JsonConvert.SerializeObject(message);

                        var body = System.Text.Encoding.UTF8.GetBytes(coinModelJson);

                        var key = GetKey(message);

                        channel.BasicPublish(exchange: "topic_logs",
                                             routingKey: key,
                                             basicProperties: null,
                                             body: body);

                        Console.WriteLine($" [x] Task {taskNumber} sent message with key: {key}");

                        counter--;
                    }
                }
            }

            return 0;
        }

        private static string GetKey(object model)
        {
            var test = model as JToken;

            var type = test.ToObject<EtherModel>();

            if(type.AverageDificulty > 0)
            {
                return type.Key;
            }

            return test.ToObject<BitcoinModel>().Key;
        }
    }
}
