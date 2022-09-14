using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace Ixs.DNA.Logging.File
{
    /// <summary>
    ///     A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        /// <summary>
        ///     A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> mFileLocks = new ConcurrentDictionary<string, object>();

        /// <summary>
        ///     The lock to lock the list of locks
        /// </summary>
        protected static object mFileLockLock = new object();

        #endregion

        #region Protected Members

        /// <summary>
        ///     The category for this logger
        /// </summary>
        protected readonly string mCategoryName;

        /// <summary>
        ///     The file path to write to
        /// </summary>
        protected readonly string mLogPath;

        /// <summary>
        ///     The configuration to use
        /// </summary>
        protected FileLoggerConfiguration mConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="categoryName">The category for this logger</param>
        /// <param name="logPath">The path of the folder to log to (relative (not rooted) to dir of executable or absolute path)</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLogger(string categoryName, string logPath, FileLoggerConfiguration configuration)
        {
            // Set members
            mCategoryName = categoryName;
            mConfiguration = configuration;
            mLogPath = Path.IsPathRooted(logPath) 
                ? logPath // already absolute path (rooted)
                : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, logPath); // get absolute
        }

        #endregion

        /// <summary>
        ///     File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        ///     Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel >= mConfiguration.LogLevel;
        }

        /// <summary>
        ///     Logs the message to file
        /// </summary>
        /// <typeparam name="TState">The type of the details of the message</typeparam>
        /// <param name="logLevel">The log level</param>
        /// <param name="eventId">The Id</param>
        /// <param name="state">The details of the message</param>
        /// <param name="exception">Any exception to log</param>
        /// <param name="formatter">The formatter for converting the state and exception to a message string</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // If we should NOT log...
            if (!IsEnabled(logLevel))
                return;

            // Get wanted DTO
            DateTimeOffset dto = mConfiguration.UseUtcTime ? DateTimeOffset.UtcNow : DateTimeOffset.Now;

            // Get current time
            string currentTimeString = dto.ToString("yyyy-MM-dd HH:mm:ss");
            string currentYearMonthString = dto.ToString("yyyy-MM");
            // Prepend log level
            string logLevelString = mConfiguration.OutputLogLevel ? $"[{logLevel.ToString().ToUpper()}] " : "";
            // Prepend the time to the log if desired
            string timeLogString = mConfiguration.OutputLogTime ? $"[{currentTimeString}] " : "";
            // Get the formatted message string
            string message = formatter(state, exception);
            // Write the message
            string output = $"{timeLogString}{logLevelString}{message}{Environment.NewLine}";
            // Normalize log path
            string normalizedLogPath = mLogPath.ToUpper();

            // Get dir info based on configuration
            string fileDirPath = mConfiguration.SingleLogFile
                ? Path.GetDirectoryName(mLogPath) ?? string.Empty // single file
                : Path.Combine(mLogPath, currentYearMonthString); // datetime file hierarchy
            
            // Get file info based on configuration
            string filePath = mConfiguration.SingleLogFile
                ? mLogPath // single file
                : Path.Combine(fileDirPath, $"{dto:yyyy-MM-dd}.log"); // datetime file hierarchy
            
            object fileLock;
            // Double safety even though the FileLocks should be thread safe
            lock (mFileLockLock)
            {
                // Get the file lock based on the absolute path
                fileLock = mFileLocks.GetOrAdd(normalizedLogPath, path => new object());
            }
            // Lock the file
            lock (fileLock)
            {
                // Ensure folder exists
                if (!Directory.Exists(fileDirPath))
                    Directory.CreateDirectory(fileDirPath);

                // Open the file
                using (var fileStream = new StreamWriter(System.IO.File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), encoding: mConfiguration.FileEncoding))
                {
                    // Go to end
                    fileStream.BaseStream.Seek(0, SeekOrigin.End);

                    // NOTE: Ignore logToTop in configuration as not efficient for files on OS

                    // Write the message to the file
                    fileStream.Write(output);
                }
            }
        }
    }
}
