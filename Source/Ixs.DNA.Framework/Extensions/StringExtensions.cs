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
        public static bool IsURL(this string content)
        {
            Uri uriResult;
            return Uri.TryCreate(content, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
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
    }
}
