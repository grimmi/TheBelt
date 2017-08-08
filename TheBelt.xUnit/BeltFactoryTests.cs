using Newtonsoft.Json.Linq;
using System.Linq;
using Xunit;

namespace TheBelt.xUnit
{
    public class BeltFactoryTests
    {
        [Fact]
        public void ValidJsonShouldDeserializeIntoBeltDefinition()
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

            var definition = BeltFactory.FromJson(def);

            Assert.Equal("testbelt", definition.Name);
            Assert.Equal("http://www.google.com", definition.Configuration["id01.in.url"]);
            Assert.Equal(1, definition.Steps.Count());
            Assert.Equal(0, definition.Steps.First().Sequence);
        }
    }
}
