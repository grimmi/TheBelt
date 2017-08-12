using Newtonsoft.Json;

namespace TheBelt
{
    public class AdapterDescription
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        [JsonProperty("adapter")]
        public string AdapterType { get; set; } = "";
    }
}
