using Newtonsoft.Json;

namespace ConsoleApp2
{
    public class Subscription
    {
        public string Company { get; set; }

        public Option Value { get; set; }

        public Option Drop { get; set; }

        public Option Variation { get; set; }

        public string Date { get; set; }
    }
}