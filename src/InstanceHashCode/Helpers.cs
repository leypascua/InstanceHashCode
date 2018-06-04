using System;
using System.Collections.Generic;
using System.Linq;
using System.Extensions;

namespace InstanceHashCode
{
    public static class Helpers
    {
        public static string BuildSha256HashFrom(IEnumerable<string> inputs)
        {
            return BuildSha256Hash(inputs.ToArray());
        }

        public static string BuildSha256Hash(params string[] inputs)
        {
            var cleanInputs = inputs
                .Where(str => !string.IsNullOrEmpty(str))
                .ToArray();

            return string.Join(string.Empty, cleanInputs)
                .ToSha256Hash();
        }

        public static string BuildMd5Hash(params string[] inputs)
        {
            var cleanInputs = inputs
                .Where(str => !string.IsNullOrEmpty(str))
                .ToArray();

            return string.Join(string.Empty, cleanInputs)
                .ToMd5Hash();
        }

        public static bool IsPrimitiveType(Type t)
        {
            return t.IsValueType || (t == typeof(string));
        }
    }
}
