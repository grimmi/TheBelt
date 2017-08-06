using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace TheBelt
{
    public static class AdapterFactory
    {
        public static T CreateAdapter<T>(Configuration config) where T : BaseAdapter
        {
            var adapterInstance = (T)Activator.CreateInstance(typeof(T));

            var mapper = new Mapper<T>();
            mapper.SetValues(adapterInstance, config);
            return adapterInstance;
        }
    }
}
