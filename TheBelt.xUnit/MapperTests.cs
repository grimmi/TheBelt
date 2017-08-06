using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TheBelt.xUnit
{
    public class MapperTests
    {
        [Fact]
        public void MapperShouldFillAllNonOptionalValues()
        {
            var adapter = new DownloadAdapter();
            var mapper = new Mapper<DownloadAdapter>();
            var config = new Configuration();
            config["url"] = "http://www.google.com";
            mapper.SetValues(adapter, config);

            Assert.Equal(adapter.Url, "http://www.google.com");
        }
    }
}
