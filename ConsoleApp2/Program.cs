using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    class Program
    {
        static void Main()
        {
            var publications = new PublicationGenerator().Generate(new PublicationConfiguration(), 10);

            WriteInFile(JsonConvert.SerializeObject(publications), true);

            ////publications.ForEach(x => WriteInFile(DisplayConfiguration(x), true));

            var subcriptions = new SubscriptionGenerator().Generate(new Configuration(), 10);


            WriteInFile(JsonConvert.SerializeObject(subcriptions));

            //subcriptions.ForEach(x => WriteInFile(DisplayConfiguration(x)));
        }

        static string DisplayConfiguration(object pub)
        {
            return JsonConvert.SerializeObject(pub);
        }

        public static void WriteInFile(string message, bool isPublisher = false)
        {
            var path = isPublisher ? "C:\\Users\\alexandru.buzdugan\\Desktop\\FIles\\Publishers.json" : "C:\\Users\\alexandru.buzdugan\\Desktop\\FIles\\Subscribers.json";

            if (!File.Exists(path))
            {
                var createText = message + Environment.NewLine;

                File.WriteAllText(path, createText);
            }

            var appendText =message + Environment.NewLine;

            File.AppendAllText(path, appendText);

            var readText = File.ReadAllText(path);

            Console.WriteLine(readText);
        }
    }
}
