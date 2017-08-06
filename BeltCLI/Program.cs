using System;
using System.Threading.Tasks;
using TheBelt;

namespace BeltCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.ReadKey();
        }

        private static async Task Run()
        {
            //var cmdAdapter = new ProcessAdapter()
            //{
            //    Path = "cmd.exe",
            //    Arguments = "/C ping 127.0.0.1"
            //};
            //Console.WriteLine("vor start");
            //var task = cmdAdapter.Start();
            //Console.WriteLine("nach start");
            //await Task.Delay(1000);
            //Console.WriteLine("nach delay");
            //await task;
            //Console.WriteLine("nach await");
            //var result = await cmdAdapter.GetResult();
            //Console.WriteLine("result: " + result);

            //var config = new Configuration();
            //config.SetConfigValue("xmlfile", @"c:\temp\belttest.xml");
            //config.SetConfigValue("jsonfile", @"c:\temp\jsonfile.json");
            //var adapter = AdapterFactory.CreateAdapter<XmlToJsonAdapter.XmlToJsonAdapter>(config);

            //await adapter.Start();
            //Console.WriteLine("result: " + await adapter.GetResult());

            var config = new Configuration();
            config.SetConfigValue("input", @"c:\temp\belttest.xml");
            config.SetConfigValue("outputpath", @"c:\temp\belt.zip");
            var adapter = AdapterFactory.CreateAdapter<ZipAdapter>(config);

            await adapter.Start();
            Console.WriteLine($"result: {await adapter.GetResult()}");
        }
    }
}