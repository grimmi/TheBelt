using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TheBelt
{
    public class Step
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("sequence")]
        public int Sequence { get; set; } = -1;
        public IEnumerable<AdapterDescription> Adapters { get; set; } = Enumerable.Empty<AdapterDescription>();
        [JsonProperty("mappings")]
        public IEnumerable<ArgumentMapping> Mappings { get; set; } = Enumerable.Empty<ArgumentMapping>();
    }
}
