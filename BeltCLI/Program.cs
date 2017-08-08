using System;
using System.Threading.Tasks;
using TheBelt;

namespace BeltCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
            Console.ReadKey();
        }

        private static async Task Run()
        {

            var dlId = Guid.NewGuid();
            var configuration = new Configuration();
            configuration.SetConfigValue($"{dlId}.in.url", "http://www.golem.de");
            var dlAdapter = AdapterFactory.CreateAdapter<DownloadAdapter>(dlId.ToString()) as BaseAdapter;
            var zipId = Guid.NewGuid();
            var zipAdapter = AdapterFactory.CreateAdapter<ZipAdapter>(zipId.ToString()) as BaseAdapter;
            var unzipId = Guid.NewGuid();
            var unzipAdapter = AdapterFactory.CreateAdapter<UnzipAdapter>(unzipId.ToString()) as BaseAdapter;

            var dlToZip = new ArgumentMapping { From = $"{dlId}.out.output", To = $"{zipId}.in.input" };
            var zipToUnzip = new ArgumentMapping { From = $"{zipId}.out.output", To = $"{unzipId}.in.archive" };
            var mappings = new[] { dlToZip, zipToUnzip };

            var belt = new Belt(new[] { dlAdapter, zipAdapter, unzipAdapter }, configuration, mappings);

            await belt.Run();
        }
    }
}