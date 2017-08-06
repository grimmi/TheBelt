using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace TheBelt.xUnit
{
    public class DownloadAdapterTests
    {
        [Fact]
        public async Task DownloadGoogleComShouldWork()
        {
            var dlAdapter = new DownloadAdapter();
            dlAdapter.Url = "http://www.google.com";
            var adapterTask = dlAdapter.Start();

            await adapterTask;

            Assert.True(dlAdapter.Finished);

            var result = await dlAdapter.GetResult();

            Assert.True(File.Exists(result));
        }
    }
}
