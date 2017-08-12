using System;
using System.Threading.Tasks;
using TheBelt;

namespace BeltCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            BeltSetup.Init();
            Run().Wait();
            BeltSetup.Teardown();
            Console.ReadKey();
        }

        private static async Task Run()
        {
            var definition = BeltDefinition.FromFile("./bin/Debug/netcoreapp1.1/parallelconfig.json");
            var belt = definition.CreateBelt();

            await belt.Run();
        }
    }
}