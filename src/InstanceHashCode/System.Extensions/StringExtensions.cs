using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace System.Extensions
{
    public static class StringExtensions
    {
        internal static readonly string EmptySha256 = string.Empty.ToSha256Hash();
        internal static readonly string EmptyMd5 = string.Empty.ToMd5Hash();

        public static string ToMd5Hash(this string input)
        {
            input = input.ToLowerInvariant();

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString().ToLowerInvariant();
            }
        }

        public static string ToSha256Hash(this string input)
        {
            var sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(input));

                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string TrimToNull(this string arg)
        {
            var input = (arg ?? string.Empty).Trim();

            return input.Length > 0 ?
                input : null;
        }

        public static string TrimToMaxLength(this string input, int maxLength)
        {
            string text = (input ?? string.Empty).Trim();
            if (text.Length <= maxLength) return text;

            return text.Substring(0, maxLength);
        }
    }
}
