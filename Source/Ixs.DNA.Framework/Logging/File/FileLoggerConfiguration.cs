using Microsoft.Extensions.Logging;
using System.Text;

namespace Ixs.DNA
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The level of log that should be processed
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; } = true;

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool OutputLogLevel { get; set; } = true;

        /// <summary>
        /// Says what file size triggers the trimming to half of the logs in a file
        /// 0 = default = no clean
        /// Unit: Byte
        /// </summary>
        public int TrimByteSize { get; set; } = 0;

        /// <summary>
        /// Encoding to use to file manipulation
        /// Default: UTF8
        /// </summary>
        public Encoding FileEncoding { get; set; } = Encoding.UTF8;

        #endregion
    }
}
