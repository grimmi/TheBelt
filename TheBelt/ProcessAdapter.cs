using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TheBelt
{
    public class ProcessAdapter : BaseAdapter
    {
        public override ResultType ResultType => ResultType.Value;

        [Input(false)]
        public string Path { get; set; }
        [Input(true)]
        public string Arguments { get; set; }

        private Process process;

        public override Task<string> GetResult()
        {
            if(!Finished)
            {
                throw new InvalidOperationException("this operation is not finished!");
            }

            return Task.FromResult(process.ExitCode.ToString());
        }

        public override Task Start()
        {
            return Task.Run(() =>
            {
                process = new Process();
                process.StartInfo.FileName = Path;
                process.StartInfo.Arguments = Arguments;
                process.Start();
                process.WaitForExit();
            });
        }
    }
}
