using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheBelt
{
    public class Step
    {
        [JsonProperty("sequence")]
        public int Sequence { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("adapter")]
        public string AdapterType { get; set; }
        [JsonProperty("mappings")]
        public IEnumerable<ArgumentMapping> Mappings { get; set; }
    }
}
