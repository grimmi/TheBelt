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
            var configuration = new Configuration();
            configuration.SetConfigValue("downloadadapter.in.url", "http://www.golem.de");
            var dlAdapter = AdapterFactory.CreateAdapter<DownloadAdapter>(configuration) as BaseAdapter;
            var zipAdapter = AdapterFactory.CreateAdapter<ZipAdapter>(configuration) as BaseAdapter;
            var unzipAdapter = AdapterFactory.CreateAdapter<UnzipAdapter>(configuration) as BaseAdapter;

            var belt = new Belt(new[] { dlAdapter, zipAdapter, unzipAdapter }, configuration);

            await belt.Run();
        }
    }
}