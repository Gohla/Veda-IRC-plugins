using System;
using System.Text.RegularExpressions;
using Veda.Interface;

namespace Veda.Plugins.Str
{
    [Plugin(Name = "String", Description = "Provides several string manipulation and information commands.")]
    public static class StringPlugin
    {
        [Command(Description = "Indicates whether the specified string is empty.")]
        public static bool IsEmpty(IContext context, String str)
        {
            return String.IsNullOrEmpty(str);
        }

        [Command(Description = "Indicates whether a specified string is empty or consists only of white-space characters.")]
        public static bool IsEmptyOrWhitespace(IContext context, String str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        [Command(Description = "Returns a value indicating whether the specified value occurs within this string.")]
        public static bool Contains(IContext context, String str, String value)
        {
            return str.Contains(value);
        }

        [Command(Description = "Determines whether the beginning of this string matches the value.")]
        public static bool StartsWith(IContext context, String str, String value)
        {
            return str.StartsWith(value);
        }

        [Command(Description = "Determines whether the end of this string matches the value.")]
        public static bool EndsWith(IContext context, String str, String value)
        {
            return str.EndsWith(value);
        }

        [Command(Description = "Concatenates given strings.")]
        public static String Concat(IContext context, String str1, String str2)
        {
            return String.Concat(str1, str2);
        }

        [Command(Description = "Concatenates given strings.")]
        public static String Concat(IContext context, String str1, String str2, String str3)
        {
            return String.Concat(str1, str2, str3);
        }

        [Command(Description = "Concatenates given strings.")]
        public static String Concat(IContext context, String str1, String str2, String str3, String str4)
        {
            return String.Concat(str1, str2, str3, str4);
        }

        [Command(Description = "Concatenates given strings.")]
        public static String Concat(IContext context, String str1, String str2, String str3, String str4, String str5)
        {
            return String.Concat(str1, str2, str3, str4, str5);
        }

        [Command(Description = "Joins given strings with a separator.")]
        public static String Join(IContext context, String separator, String str1, String str2)
        {
            return String.Join(separator, str1, str2);
        }

        [Command(Description = "Joins given strings with a separator.")]
        public static String Join(IContext context, String separator, String str1, String str2, String str3)
        {
            return String.Join(separator, str1, str2, str3);
        }

        [Command(Description = "Joins given strings with a separator.")]
        public static String Join(IContext context, String separator, String str1, String str2, String str3, 
            String str4)
        {
            return String.Join(separator, str1, str2, str3, str4);
        }

        [Command(Description = "Joins given strings with a separator.")]
        public static String Join(IContext context, String separator, String str1, String str2, String str3, 
            String str4, String str5)
        {
            return String.Join(separator, str1, str2, str3, str4, str5);
        }

        [Command(Description = "Formats given strings. See http://msdn.microsoft.com/en-us/library/system.string.format.aspx for more info.")]
        public static String Format(IContext context, String format, String str1)
        {
            return String.Format(format, str1);
        }

        [Command(Description = "Formats given strings. See http://msdn.microsoft.com/en-us/library/system.string.format.aspx for more info.")]
        public static String Format(IContext context, String format, String str1, String str2)
        {
            return String.Format(format, str1, str2);
        }

        [Command(Description = "Formats given strings. See http://msdn.microsoft.com/en-us/library/system.string.format.aspx for more info.")]
        public static String Format(IContext context, String format, String str1, String str2, String str3)
        {
            return String.Format(format, str1, str2, str3);
        }

        [Command(Description = "Formats given strings. See http://msdn.microsoft.com/en-us/library/system.string.format.aspx for more info.")]
        public static String Format(IContext context, String format, String str1, String str2, String str3, String str4)
        {
            return String.Format(format, str1, str2, str3, str4);
        }

        [Command(Description = "Formats given strings. See http://msdn.microsoft.com/en-us/library/system.string.format.aspx for more info.")]
        public static String Format(IContext context, String format, String str1, String str2, String str3, String str4, 
            String str5)
        {
            return String.Format(format, str1, str2, str3, str4, str5);
        }

        [Command(Description = "Replaces all occurrences of value in the string with replacement string.")]
        public static String Replace(IContext context, String str, String value, String replacement)
        {
            return str.Replace(value, replacement);
        }

        [Command(Description = "Retrieves a substring from the string. The substring starts at given index and has given length.")]
        public static String Substring(IContext context, String str, int startIndex, int length)
        {
            return str.Substring(startIndex, length);
        }

        [Command(Description = "Removes all leading and trailing white-space characters.")]
        public static String Trim(IContext context, String str)
        {
            return str.Trim();
        }

        [Command(Name = "regex match", Description = "Indicates whether the regex pattern finds a match in the string. See http://msdn.microsoft.com/en-us/library/hs600312.aspx for more info.")]
        public static bool RegexMatch(IContext context, String str, String pattern)
        {
            return Regex.IsMatch(str, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
        }

        [Command(Name = "regex replace", Description = "Replaces all strings that match regex pattern with a replacement string. http://msdn.microsoft.com/en-us/library/hs600312.aspx for more info.")]
        public static String RegexReplace(IContext context, String str, String pattern, String replacement)
        {
            return Regex.Replace(str, pattern, replacement, RegexOptions.None, TimeSpan.FromMilliseconds(100));
        }
    }
}
