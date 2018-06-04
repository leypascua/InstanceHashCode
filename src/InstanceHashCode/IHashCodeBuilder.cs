namespace InstanceHashCode
{
    /// <summary>
    /// An abstraction for building hash codes given an object instance.
    /// </summary>
    public interface IHashCodeBuilder
    {
        /// <summary>
        /// Builds a SHA256 hash based from the combined property 
        /// values of the instance.
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns>a string</returns>
        string BuildSha256Hash(object instance);

        /// <summary>
        /// Builds a MD5 hash based from the combined property 
        /// values of the instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        string BuildMd5Hash(object instance);
    }
}
