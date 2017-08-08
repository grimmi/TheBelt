using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static BeltDefinition FromFile(string jsonFile)
        {
            var definition = JsonConvert.DeserializeObject<BeltDefinition>(File.ReadAllText(jsonFile));
            return definition;
        }

        public static BeltDefinition FromJson(JObject json)
        {
            return JsonConvert.DeserializeObject<BeltDefinition>(json.ToString());
        }

        public Belt CreateBelt()
        {
            var adapters = Steps.Select(MakeAdapter);
            var mappings = Steps.SelectMany(step => step.Mappings);
            var config = Configuration.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            var beltConfig = new Configuration(config);
            return new Belt(adapters, beltConfig, mappings);
        }

        private BaseAdapter MakeAdapter(Step step)
        {
            var adapter = AdapterFactory.CreateAdapter(step.AdapterType);
            adapter.Id = step.Id;
            return adapter;
        }
    }
}
