using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TheBelt
{
    public class Mapper
    {
        public void SetValues(BaseAdapter adapter, Configuration config, IEnumerable<ArgumentMapping> mappings = null)
        {
            mappings = mappings ?? Enumerable.Empty<ArgumentMapping>();
            var inputProperties = GetInputProperties(adapter);
            foreach(var inputProperty in inputProperties)
            {
                var inName = $"{adapter.Id}.in.{inputProperty.Name.ToLower()}";
                try
                {
                    var mapping = mappings.FirstOrDefault(m => m.To.ToLower().Equals(inName));
                    if (mapping != null)
                    {
                        var mappedValue = config.GetConfigValue(mapping.From);
                        Log.Information("setting [{@inName}] to [{@inputValue}] (from mapping)", inName, mappedValue);
                        inputProperty.SetValue(adapter, mappedValue);
                    }
                    else
                    {
                        var propertyValue = config.GetConfigValue(inName);
                        var converted = Convert.ChangeType(propertyValue, inputProperty.PropertyType);
                        Log.Information("setting [{@inName}] to [{@inputValue}] (without mapping)", inName, converted);
                        inputProperty.SetValue(adapter, converted);
                    }
                }
#pragma warning disable 0168
                catch(KeyNotFoundException knfEx) when (IsOptional(inputProperty))
#pragma warning restore 0168
                {                    
                }
                catch(Exception ex) when 
                    (ex is InvalidCastException || ex is FormatException 
                    || ex is OverflowException || ex is ArgumentNullException)
                {
                    // error converting config value to correct type
                    throw;
                }
            }
        }

        private bool IsOptional(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<InputAttribute>();
            return attribute.IsOptional;
        }

        private IEnumerable<PropertyInfo> GetInputProperties(BaseAdapter adapter)
        {
            var properties = adapter.GetType().GetRuntimeProperties();
            var inputProperties = properties.Where(p => p.GetCustomAttribute<InputAttribute>() != null);
            return inputProperties;
        }
    }
}
