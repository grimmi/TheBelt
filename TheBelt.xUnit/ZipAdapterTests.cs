using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TheBelt.xUnit
{
    public class ZipAdapterTests
    {
        [Fact]
        public async Task ZipAdapterShouldUnzipArchiveToTempPath()
        {
            var adapter = new ZipAdapter()
            {
                Archive = @"c:\temp\testarchive.zip"
            };
            await adapter.Start();

            Assert.True(adapter.Finished);

            Assert.True(Directory.Exists(adapter.OutputDirectory));
            Assert.True(Directory.GetFiles(adapter.OutputDirectory).Length > 0);
            Assert.Equal(await adapter.GetResult(), adapter.OutputDirectory);
        }
    }
}
