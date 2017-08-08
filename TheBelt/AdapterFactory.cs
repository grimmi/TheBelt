using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheBelt
{
    public static class AdapterFactory
    {
        private static Dictionary<string, Type> adapterTypeMap { get; } = new Dictionary<string, Type>();
        private static bool initDone = false;

        public static void Init(IEnumerable<Assembly> pluginAssemblies = null)
        {
            if (initDone && pluginAssemblies == null)
            {
                return;
            }
            pluginAssemblies = pluginAssemblies ?? Enumerable.Empty<Assembly>();

            var thisAssembly = typeof(AdapterFactory).GetTypeInfo().Assembly;

            var assemblies = pluginAssemblies.Union(new[] { thisAssembly });
            var baseType = typeof(BaseAdapter).GetTypeInfo();
            foreach(var assembly in assemblies)
            {
                var adapterTypes = assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t));
                foreach(var adapterType in adapterTypes)
                {
                    adapterTypeMap[adapterType.Name.ToLower()] = adapterType;
                }
            }
        }

        public static T CreateAdapter<T>(string adapterId = "") where T : BaseAdapter
        {
            Init(null);
            var adapterInstance = (T)Activator.CreateInstance(typeof(T));

            if(!string.IsNullOrWhiteSpace(adapterId))
            {
                adapterInstance.Id = adapterId;
            }
            return adapterInstance;
        }

        public static BaseAdapter CreateAdapter(string adapterType)
        {
            Init(null);
            Type type = null;
            if(!adapterTypeMap.TryGetValue(adapterType.ToLower(), out type))
            {
                throw new ArgumentException($"unknown adapter '{adapterType}!");
            }

            return (BaseAdapter)Activator.CreateInstance(type);
        }
    }
}
