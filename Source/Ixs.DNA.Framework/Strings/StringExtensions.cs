using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ixs.DNA
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or an empty string
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content);
        }

        /// <summary>
        /// Returns true if the string is null or an empty string or just whitespace
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsNullOWhiteSpace(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }

        /// <summary>
        /// Returns formatted string
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="parameters">Format parameters</param>
        /// <returns></returns>
        public static string Format(this string content, params object[] parameters)
        {
            return string.Format(content, parameters);
        }

        /// <summary>
        /// Simple URL check.
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsURL(this string content)
        {
            Uri uriResult;
            return Uri.TryCreate(content, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Check if the string has appropriate form of HEX color.
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="hasHashmark"></param>
        /// <param name="allowAlpha">True to include alpha into the search</param>
        /// <returns></returns>
        public static bool IsColorHEX(this string content, bool hasHashmark = false, bool allowAlpha = false)
        {
            var standard = @"^" + (hasHashmark ? "#" : "") + "[A-Fa-f0-9]{6}$";
            var alpha = @"^" + (hasHashmark ? "#" : "") + "[A-Fa-f0-9]{8}$";

            return Regex.IsMatch(
                content, 
                standard + (allowAlpha ? "|" + alpha : "")
                );
        }

        /// <summary>
        /// Remove N lines from the beginning of the string.
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="nLines">Number of lines affected</param>
        /// <returns></returns>
        public static string RemoveFirstLines(this string content, int nLines)
        {
            var lines = Regex.Split(content, "\r\n|\r|\n").Skip(nLines);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        /// <summary>
        /// Get N lines from the beginning of the string.
        /// </summary>
        /// <param name="content">The string</param>
        /// <param name="nLines">Number of lines affected</param>
        /// <returns></returns>
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
