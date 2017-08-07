using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheBelt
{
    public class Belt
    {
        private IEnumerable<BaseAdapter> Adapters { get; }
        private Configuration Configuration { get; }
        private IEnumerable<ArgumentMapping> Mappings { get; }
        public Belt(IEnumerable<BaseAdapter> adapters, Configuration config, IEnumerable<ArgumentMapping> mappings)
        {
            Adapters = adapters;
            Configuration = config;
            Mappings = mappings;
        }

        public async Task Run()
        {
            var mapper = new Mapper();
            foreach(var adapter in Adapters)
            {
                mapper.SetValues(adapter, Configuration, Mappings);
                var adapterTask = adapter.Start();
                await adapterTask;
                var result = await adapter.GetResult();
                Console.WriteLine(result);
                adapter.FillConfigurationWithOutputs(Configuration);
            }
        }
    }
}
