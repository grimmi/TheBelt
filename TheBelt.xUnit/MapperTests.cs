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
            var mapper = new Mapper();
            var config = new Configuration();
            config[$"{adapter.Id.ToString().ToLower()}.in.url"] = "http://www.google.com";
            mapper.SetValues(adapter, config);

            Assert.Equal(adapter.Url, "http://www.google.com");
        }
    }
}
