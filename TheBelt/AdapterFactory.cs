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
        public static T CreateAdapter<T>(Configuration config, Guid adapterId = default(Guid)) where T : BaseAdapter
        {
            var adapterInstance = (T)Activator.CreateInstance(typeof(T));

            if(adapterId == default(Guid))
            {
                adapterId = Guid.NewGuid();
            }
            adapterInstance.Id = adapterId;

            return adapterInstance;
        }
    }
}
