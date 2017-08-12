using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;

namespace TheBelt
{
    public class BeltDefinition
    {
        [JsonProperty("name")]
        internal string Name { get; set; }
        [JsonProperty("configuration")]
        internal Dictionary<string, object> Configuration { get; set; }
        [JsonProperty("steps")]
        internal IEnumerable<Step> Steps { get; set; }

        public static BeltDefinition FromFile(string jsonFile)
        {
            Log.Information("Loading belt definition from {@File}", jsonFile);
            var definition = JsonConvert.DeserializeObject<BeltDefinition>(File.ReadAllText(jsonFile));
            return definition;
        }

        public static BeltDefinition FromJson(JObject json)
        {
            Log.Information("Loading belt definition from {@Json}", json);
            return JsonConvert.DeserializeObject<BeltDefinition>(json.ToString());
        }

        public Belt CreateBelt()
        {
            var adapters = Steps.OrderBy(step => step.Sequence).SelectMany(MakeAdapter);
            var mappings = Steps.SelectMany(step => step.Mappings);
            var config = Configuration.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            var beltConfig = new Configuration(config);
            var belt = new Belt(adapters, beltConfig, mappings);
            Log.Information("created belt: {belt}", belt);
            return belt;
        }

        private IEnumerable<BaseAdapter> MakeAdapter(Step step)
        {
            foreach(var adapterDescription in step.Adapters)
            {
                var adapter = AdapterFactory.CreateAdapter(adapterDescription.AdapterType);
                adapter.Id = adapterDescription.Id;
                adapter.Sequence = step.Sequence;
                yield return adapter;
            }
        }
    }
}
