using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheBelt
{
    public class BeltDefinition
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("configuration")]
        public Dictionary<string, object> Configuration { get; set; }
        [JsonProperty("steps")]
        public IEnumerable<Step> Steps { get; set; }
    }

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

    public static class BeltFactory
    {

        public static BeltDefinition FromFile(string jsonFile)
        {
            var definition = JsonConvert.DeserializeObject<BeltDefinition>(File.ReadAllText(jsonFile));
            return definition;
        }

        public static BeltDefinition FromJson(JObject json)
        {
            return JsonConvert.DeserializeObject<BeltDefinition>(json.ToString());
        }
    }
}
