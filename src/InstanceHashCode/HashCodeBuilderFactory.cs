using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace InstanceHashCode
{
    /// <summary>
    /// An abstraction for creating IHashCodeBuilder instances.
    /// </summary>
    public interface IHashCodeBuilderFactory
    {
        /// <summary>
        /// Creates an instance of IHashCodeBuilder based on targetType
        /// </summary>
        /// <param name="targetType">The targetType</param>
        /// <returns>an IHashCodeBuilder instance</returns>
        IHashCodeBuilder CreateInstance(Type targetType);

        /// <summary>
        /// Adds an IHashCodeBuilder instance to the factory
        /// </summary>
        /// <param name="targetType">The targetType</param>
        /// <param name="builderInstance">The builderInstance</param>
        void Register(Type targetType, IHashCodeBuilder builderInstance);
    }

    public class HashCodeBuilderFactory : IHashCodeBuilderFactory
    {        
        private readonly ConcurrentDictionary<Type, IHashCodeBuilder> _builderCache = new ConcurrentDictionary<Type, IHashCodeBuilder>();

        public readonly static IHashCodeBuilderFactory Default = new HashCodeBuilderFactory();

        public IHashCodeBuilder CreateInstance(Type targetType)
        {
            if (Helpers.IsPrimitiveType(targetType)) return PrimitiveTypeHashCodeBuilder.Instance;

            return GetBuilder(targetType);
        }

        public void Register(Type instanceType, IHashCodeBuilder builderInstance)
        {
            if (!_builderCache.ContainsKey(instanceType))
            {
                _builderCache.TryAdd(instanceType, builderInstance);
            }
        }

        private IHashCodeBuilder GetBuilder(Type instanceType)
        {
            if (!_builderCache.ContainsKey(instanceType))
            {
                IEnumerable<PropertyInfo> targetProperties = HashCodeParameterAttribute.GetDemarcatedProperties(instanceType);
                var builder = new ReflectionHashCodeBuilder(targetProperties, this);
                this.Register(instanceType, builder);
            }

            return _builderCache[instanceType];
        }
    }
}
