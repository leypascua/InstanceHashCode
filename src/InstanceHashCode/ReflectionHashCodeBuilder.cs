using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace InstanceHashCode
{
    public class ReflectionHashCodeBuilder : IHashCodeBuilder
    {        
        private readonly IHashCodeBuilderFactory _builderFactory;
        private readonly IEnumerable<PropertyInfo> TargetProperties;

        public ReflectionHashCodeBuilder(IEnumerable<PropertyInfo> targetProperties, IHashCodeBuilderFactory builderFactory)
        {
            TargetProperties = targetProperties;
            _builderFactory = builderFactory;
        }

        public virtual string BuildMd5Hash(object instance)
        {
            string[] result = GetParameters(instance, TargetProperties, _builderFactory);
            return Helpers.BuildMd5Hash(result);
        }

        public virtual string BuildSha256Hash(object instance)
        {
            string[] result = GetParameters(instance, TargetProperties, _builderFactory);
            return Helpers.BuildSha256Hash(result);
        }

        public static string[] GetParameters(object instance, IEnumerable<PropertyInfo> properties, IHashCodeBuilderFactory builderFactory)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            var parameterValues = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(instance, null);
                if (propValue == null) continue;

                Type propertyType = property.PropertyType;
                if (propValue is System.Collections.ICollection)
                {
                    parameterValues.AddRange(GetParametersFromCollection(propValue as System.Collections.ICollection, builderFactory));
                    continue;
                }

                parameterValues.Add(propValue.ToString());
            }

            return parameterValues
                .Where(val => !string.IsNullOrEmpty(val))
                .ToArray();
        }

        private static IEnumerable<string> GetParametersFromCollection(System.Collections.ICollection collection, IHashCodeBuilderFactory builderFactory)
        {
            var values = new List<string>();

            if (collection == null) return values;

            foreach (object element in collection)
            {
                if (element == null) continue;                

                if (element is System.Collections.ICollection)
                {
                    values.AddRange(GetParametersFromCollection(element as System.Collections.ICollection, builderFactory));
                    continue;
                }

                Type instanceType = element.GetType();
                var builder = builderFactory.CreateInstance(element.GetType()) as ReflectionHashCodeBuilder;
                
                if (builder != null)
                {
                    values.AddRange(GetParameters(element, builder.TargetProperties, builderFactory));
                }
            }

            return values;
        }
    }
}
