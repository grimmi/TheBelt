using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TheBelt
{
    public abstract class BaseAdapter
    {
        public abstract ResultType ResultType { get; }
        public virtual bool Finished { get; protected set; } = false;
        public abstract Task Start();
        public abstract Task<string> GetResult();

        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public virtual void FillConfigurationWithOutputs(Configuration config)
        {
            foreach(var outputProperty in GetOutputProperties())
            {
                var outputValue = outputProperty.GetValue(this)?.ToString() ?? "";
                config[$"{Id}.out.{GetOutputName(outputProperty)}"] = outputValue;
            }
        }

        private IEnumerable<PropertyInfo> GetOutputProperties()
        {
            return GetType().GetTypeInfo().GetProperties()
                .Where(p => p.GetCustomAttribute<OutputAttribute>() != null);
        }

        private string GetOutputName(PropertyInfo property)
        {
            var outputAttribute = property.GetCustomAttribute<OutputAttribute>();
            return outputAttribute.Name;
        }
    }

    public enum ResultType
    {
        Unknown,
        File,
        Directory,
        Url,
        Value
    }
}
