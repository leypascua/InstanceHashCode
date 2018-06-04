using System;
using System.Extensions;

namespace InstanceHashCode
{
    public class PrimitiveTypeHashCodeBuilder : IHashCodeBuilder
    {
        public static readonly IHashCodeBuilder Instance = new PrimitiveTypeHashCodeBuilder();

        private PrimitiveTypeHashCodeBuilder() { }

        public string BuildSha256Hash(object instance)
        {
            if (instance == null) return StringExtensions.EmptySha256;

            Type instanceType = instance.GetType();
            EnforcePrimitiveType(instanceType);            

            return instance.ToString().ToSha256Hash();
        }

        public string BuildMd5Hash(object instance)
        {
            if (instance == null) return StringExtensions.EmptyMd5;

            Type instanceType = instance.GetType();
            EnforcePrimitiveType(instanceType);

            return instance.ToString().ToMd5Hash();
        }

        private static void EnforcePrimitiveType(Type instanceType)
        {
            if (!Helpers.IsPrimitiveType(instanceType))
            {
                throw new NotSupportedException($"The type '{instanceType.AssemblyQualifiedName}' is not supported by '{typeof(PrimitiveTypeHashCodeBuilder).FullName}'");
            }
        }
    }
}
