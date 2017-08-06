using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace TheBelt
{
    public class ZipAdapter : BaseAdapter
    {
        public override ResultType ResultType => ResultType.File;

        [Input(false)]
        public string Input { get; set; }

        [Input(true)]
        [Output("the output file / directory")]
        public string OutputPath { get; set; }

        public override Task<string> GetResult()
        {
            if(!Finished)
            {
                throw new InvalidOperationException("operation is not finished!");
            }
            return Task.FromResult(OutputPath);
        }

        public override Task Start()
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(OutputPath))
                {
                    OutputPath = Path.GetTempFileName();
                }
                if (Directory.Exists(Input))
                {
                    ZipFile.CreateFromDirectory(Input, OutputPath);
                }
                else if(File.Exists(Input))
                {
                    using (var stream = new FileStream(OutputPath, FileMode.OpenOrCreate))
                    using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(Input, Path.GetFileName(Input));
                    }
                }
                Finished = true;
            });
        }
    }
}
