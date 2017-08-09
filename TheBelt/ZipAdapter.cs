using Serilog;
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

        [Input(false, "input", "file or directory to zip")]
        public string Input { get; set; }

        [Input(true, "output", "the ziparchive")]
        [Output("output", "the ziparchive")]
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
                    Log.Information("creating zip archive from {@Input}...", Input);
                    using (var stream = new FileStream(OutputPath, FileMode.OpenOrCreate))
                    using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(Input, Path.GetFileName(Input));
                    }
                    Log.Information("created zip archive: {@OutputPath}", OutputPath);
                }
                Finished = true;
            });
        }
    }
}
