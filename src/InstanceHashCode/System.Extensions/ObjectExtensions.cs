using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using InstanceHashCode;

namespace System.Extensions
{
    /// <summary>
    /// Extension methods for System.Object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the Sha256 hash code based from the instance
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns>The SHA256 hash code string</returns>
        public static string GetSha256HashCode(this object instance)
        {
            var builder = GetHashCodeBuilder(instance);                
            return builder.BuildSha256Hash(instance);
        }

        /// <summary>
        /// Gets the MD5 hash code based from the instance
        /// </summary>
        /// <param name="instance"The instance></param>
        /// <returns>The MD5 hash code string</returns>
        public static string GetMd5HashCode(this object instance)
        {
            var builder = GetHashCodeBuilder(instance);
            return builder.BuildMd5Hash(instance);
        }

        private static IHashCodeBuilder GetHashCodeBuilder(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentException("instance");
            }

            Type instanceType = instance.GetType();

            if (Helpers.IsPrimitiveType(instanceType))
            {
                return PrimitiveTypeHashCodeBuilder.Instance;
            }

            return HashCodeBuilderFactory.Default.CreateInstance(instanceType);
        }
    }
}
