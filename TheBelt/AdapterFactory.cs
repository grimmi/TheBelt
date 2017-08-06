using System;
using System.Collections.Generic;
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
