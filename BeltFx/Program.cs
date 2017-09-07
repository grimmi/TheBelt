using Squirrel;
using System;
using System.Threading.Tasks;
using TheBelt;

namespace BeltFx
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var mgr = new UpdateManager(@"c:\temp\releases"))
            {
                mgr.UpdateApp().Wait();
            }

            BeltSetup.Init();
            Run().Wait();
            BeltSetup.Teardown();
            Console.ReadKey();
        }

        private static async Task Run()
        {
            var definition = BeltDefinition.FromFile("./parallelconfig.json");
            var belt = definition.CreateBelt();

            await belt.Run();
        }
    }
}
