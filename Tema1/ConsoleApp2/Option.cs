using Newtonsoft.Json;

namespace ConsoleApp2
{
    public class Option
    {
        [JsonIgnore]
        public string Field { get; set; }

        [JsonIgnore]
        public string Op { get; set; }

        [JsonIgnore]
        public string Value { get; set; }

        [JsonProperty("Result")]
        public string Display => Field + "," + Op + "," + Value;
    }
}