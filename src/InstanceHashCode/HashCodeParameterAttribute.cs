using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace InstanceHashCode
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HashCodeParameterAttribute : Attribute
    {
        public static IEnumerable<PropertyInfo> GetDemarcatedProperties(Type instanceType)
        {
            var allProperties = new List<PropertyInfo>();
            var targetedProperties = new List<PropertyInfo>();

            foreach (var prop in instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
            {
                // we don't support indexable properties for now.
                if (prop.GetIndexParameters().Any()) continue;

                allProperties.Add(prop);
                HashCodeParameterAttribute attr = prop
                    .GetCustomAttributes(typeof(HashCodeParameterAttribute), true)
                    .Select(a => a as HashCodeParameterAttribute)
                    .FirstOrDefault();

                if (attr != null)
                {
                    targetedProperties.Add(prop);
                }
            }

            return targetedProperties.Any() ?
                targetedProperties : allProperties;
        }
    }
}
