using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TheBelt
{
    public class Step
    {
        [JsonProperty("sequence")]
        public int Sequence { get; set; } = -1;
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        [JsonProperty("adapter")]
        public string AdapterType { get; set; } = "";
        [JsonProperty("mappings")]
        public IEnumerable<ArgumentMapping> Mappings { get; set; } = Enumerable.Empty<ArgumentMapping>();
    }
}
