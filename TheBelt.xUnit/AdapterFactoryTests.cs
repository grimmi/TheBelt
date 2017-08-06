using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TheBelt.xUnit
{
    public class AdapterFactoryTests
    {
        private class EmptyAdapter : BaseAdapter
        {
            public override ResultType ResultType => ResultType.Unknown;

            public override Task<string> GetResult()
            {
                return Task.FromResult("");
            }

            public override Task Start()
            {
                return Task.FromResult("");
            }
        }

        [Fact]
        public void FactoryShouldCreateCorrectType()
        {
            var adapter = AdapterFactory.CreateAdapter<EmptyAdapter>(new Configuration());

            Assert.IsType<EmptyAdapter>(adapter);
        }
    }
}
