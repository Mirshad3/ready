using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace localshop.Core.Common
{
    public static class StringHelper
    {
        public static string ToUnsigned(this string value)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = value.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string GenerateSlug(this string value)
        {
            string str = value.ToUnsigned().RemoveAccent().ToLower();
            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim
            str = str.Substring(0, str.Length <= 100 ? str.Length : 100).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        private static string RemoveAccent(this string value)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(value);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}