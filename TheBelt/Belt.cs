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
        public Belt(IEnumerable<BaseAdapter> adapters, Configuration config)
        {
            Adapters = adapters;
            Configuration = config;
        }

        public async Task Run()
        {
            var dlToZip = new ArgumentMapping { From = "downloadapter.out.output", To = "zipadapter.in.input" };
            var zipToUnzip = new ArgumentMapping { From = "zipadapter.out.output", To = "unzipadapter.in.archive" };
            var mappings = new[] { dlToZip, zipToUnzip };
            var mapper = new Mapper();
            foreach(var adapter in Adapters)
            {
                mapper.SetValues(adapter, Configuration, mappings);
                var adapterTask = adapter.Start();
                await adapterTask;
                var result = await adapter.GetResult();
                Console.WriteLine(result);
                adapter.FillConfigurationWithOutputs(Configuration);
            }
        }
    }
}
