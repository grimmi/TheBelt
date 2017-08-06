using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TheBelt
{
    public class Mapper<T> where T: BaseAdapter
    {
        public void SetValues(T adapter, Configuration config)
        {
            var inputProperties = GetInputProperties();
            foreach(var inputProperty in inputProperties)
            {
                try
                {
                    var propertyValue = config.GetConfigValue(inputProperty.Name);
                    var converted = Convert.ChangeType(propertyValue, inputProperty.PropertyType);
                    inputProperty.SetValue(adapter, converted);
                }
                catch(KeyNotFoundException knfEx) when (IsOptional(inputProperty))
                {
                    // ignore if property is optional
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

        private IEnumerable<PropertyInfo> GetInputProperties()
        {
            var properties = typeof(T).GetRuntimeProperties();
            var inputProperties = properties.Where(p => p.GetCustomAttribute<InputAttribute>() != null);
            return inputProperties;
        }
    }
}
