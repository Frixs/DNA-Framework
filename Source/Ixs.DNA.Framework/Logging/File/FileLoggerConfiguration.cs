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
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool OutputLogTime { get; set; }

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool OutputLogLevel { get; set; }

        /// <summary>
        /// Encoding to use to file manipulation
        /// </summary>
        /// <remarks>Default <see cref="Encoding.UTF8"/></remarks>
        public Encoding FileEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Use UTC (<see langword="true"/>) instead of the current local time (<see langword="false"/>).
        /// </summary>
        public bool UseUtcTime { get; set; }

        /// <summary>
        /// Identifies if logger should log only into single log file (<see langword="true"/>) or create hierarchical structure based on datetime (<see langword="false"/>).
        /// </summary>
        public bool SingleLogFile { get; set; }

        #endregion
    }
}
