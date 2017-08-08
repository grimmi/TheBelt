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
            var definition = BeltDefinition.FromFile("./bin/Debug/netcoreapp1.1/config.json");
            var belt = definition.CreateBelt();

            await belt.Run();
        }
    }
}