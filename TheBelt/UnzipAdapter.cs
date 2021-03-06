﻿using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace TheBelt
{
    public class UnzipAdapter : BaseAdapter
    {
        public override ResultType ResultType => ResultType.Directory;

        [Input(false, "archive", "archive to unzip")]
        public string Archive { get; set; }
        [Input(true, "outputdirectory", "directory into which to unzip")]
        [Output("outputdirectory", "directory into which the archive is extracted")]
        public string OutputDirectory { get; set; }

        public override Task<string> GetResult()
        {
            if(!Finished)
            {
                throw new InvalidOperationException("this operation is not finished!");
            }

            return Task.FromResult(OutputDirectory);
        }

        public override Task Start()
        {
            return Task.Run(() =>
            {
                OutputDirectory = string.IsNullOrWhiteSpace(OutputDirectory)
                    ? Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
                    : OutputDirectory;

                if(!Directory.Exists(OutputDirectory))
                {
                    Directory.CreateDirectory(OutputDirectory);
                }

                Log.Information("unzipping {@Archive} to {@OutputDirectory}", Archive, OutputDirectory);
                using (var fileStream = new FileStream(Archive, FileMode.Open))
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        var targetPath = Path.Combine(OutputDirectory, entry.FullName);
                        entry.ExtractToFile(targetPath);
                    }
                }
                Finished = true;
            });
        }
    }
}
