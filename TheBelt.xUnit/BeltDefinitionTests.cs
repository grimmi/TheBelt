using Newtonsoft.Json.Linq;
using System.Linq;
using Xunit;

namespace TheBelt.xUnit
{
    public class BeltDefinitionTests
    {
        private JObject GetSimpleJson()
        {
            var def = new JObject();
            def.Add("name", "testbelt");

            var config = new JObject();
            config.Add("id01.in.url", "http://www.google.com");

            var step = new JObject();
            step.Add("sequence", 0);
            step.Add("id", "id01");
            step.Add("adapter", "downloadadapter");
            var steps = new[] { step };

            def.Add("configuration", config);
            def.Add("steps", JToken.FromObject(steps));

            return def;
        }

        [Fact]
        public void ValidJsonShouldDeserializeIntoBeltDefinition()
        {
            var def = GetSimpleJson();
            var definition = BeltDefinition.FromJson(def);

            Assert.Equal("testbelt", definition.Name);
            Assert.Equal("http://www.google.com", definition.Configuration["id01.in.url"]);
            Assert.Equal(1, definition.Steps.Count());
            Assert.Equal(0, definition.Steps.First().Sequence);
        }

        [Fact]
        public void BeltDefinitionCreatesCompleteBelt()
        {
            var definition = BeltDefinition.FromJson(GetSimpleJson());

            var belt = definition.CreateBelt();

            Assert.Equal("http://www.google.com", belt.Configuration.GetConfigValue("id01.in.url"));
            Assert.Equal("id01", belt.Adapters.Single().Id);
        }
    }
}
