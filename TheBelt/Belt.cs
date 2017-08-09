using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo("TheBelt.xUnit")]
namespace TheBelt
{
    public class Belt
    {
        internal IEnumerable<BaseAdapter> Adapters { get; }
        internal Configuration Configuration { get; }
        internal IEnumerable<ArgumentMapping> Mappings { get; }

        private string currentStep = "";
        public string CurrentStep
        {
            get { return currentStep; }
            private set { currentStep = value; Log.Information("current step: {@currentStep}", currentStep); }
        }

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
                CurrentStep = "setting values on adapter";
                mapper.SetValues(adapter, Configuration, Mappings);
                Log.Debug("Adapter: [{@adapter}]", adapter);
                Log.Information("starting {$adapter}...", adapter);
                var sw = Stopwatch.StartNew();
                var adapterTask = adapter.Start();
                CurrentStep = "running adapter";
                await adapterTask;
                var result = await adapter.GetResult();
                Log.Debug("Adapter: [{@adapter}] | Result: [{@result}] | Runtime: {@runtime}", adapter, result, sw.Elapsed);
                Log.Information("{$adapter} finished; result: {$result}; runtime: {$runtime}", adapter, result, sw.Elapsed);
                CurrentStep = "processing result";
                adapter.FillConfigurationWithOutputs(Configuration);
            }
        }

        public override string ToString()
        {
            return $"[{string.Join("->", Adapters.Select(a => a.GetType()))}]";
        }
    }
}
