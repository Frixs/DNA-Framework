using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ixs.DNA.Extensions
{
    /// <summary>
    ///     Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Simple URL check.
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns><see langword="true"/> = is URL, <see langword="false"/> otherwise</returns>
        public static bool IsUrl(this string content)
        {
            return Uri.TryCreate(content, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        ///     Remove N lines from the beginning of the string.
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="nLines">Number of lines affected</param>
        /// <returns>Modified string</returns>
        public static string RemoveFirstLines(this string content, int nLines)
        {
            var lines = Regex.Split(content, "\r\n|\r|\n").Skip(nLines);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        /// <summary>
        ///     Get N lines from the beginning of the string.
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="nLines">Number of lines affected</param>
        /// <returns>Modified string</returns>
        public static string GetFirstLines(this string content, int nLines)
        {
            var lines = Regex.Split(content, "\r\n|\r|\n");
            var newLines = new string[nLines];

            for (int i = 0; i < nLines && i < lines.Length; i++)
                newLines[i] = lines[i];

            return string.Join(Environment.NewLine, newLines.ToArray());
        }

        /// <summary>
        ///     Parse string value into generically selected one.
        /// </summary>
        /// <typeparam name="T">The generic type to parse the value to.</typeparam>
        /// <param name="input">The input value ready for parsing.</param>
        /// <returns>Parsed value in <typeparamref name="T"/> or <see langword="null"/> on failure.</returns>
        /// <remarks>
        ///     It supports: <see langword="short"/>, <see langword="int"/>, <see langword="long"/>,
        ///     <see langword="float"/>, <see langword="double"/>, <see langword="char"/>, <see langword="bool"/>.
        /// </remarks>
        public static T? ParseValue<T>(this string input)
            where T : struct
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(short))
            {
                if (short.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(long))
            {
                if (long.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(float))
            {
                if (float.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(char))
            {
                if (char.TryParse(input, out var val))
                    return (T)(object)val;
            }
            else if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(input, out var val))
                    return (T)(object)val;
            }

            return null;
        }
    }
}
