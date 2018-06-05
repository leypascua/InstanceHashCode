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

        /// <summary>
        /// Compares the SHA 256 hash of two object instances
        /// </summary>
        /// <param name="current">The current instance</param>
        /// <param name="other">The other instance</param>
        /// <returns></returns>
        public static bool Sha256HashEquals<T>(this T current, T other) where T : class
        {            
            if (current != null && other != null)
            {
                return current.GetSha256HashCode() == other.GetSha256HashCode();
            }

            return current == other;
        }

        public static bool Md5HashEquals<T>(this T current, T other) where T : class
        {
            if (current != null && other != null)
            {
                return current.GetMd5HashCode() == other.GetMd5HashCode();
            }

            return current == other;
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
