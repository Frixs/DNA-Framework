using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Ixs.DNA
{
    /// <summary>
    /// Extension methods for the <see cref="FileLogger"/>
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="logPath">The path of the folder to log to</param>
        /// <param name="configuration">The configuration</param>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string logPath, FileLoggerConfiguration configuration = null)
        {
            // Create default configuration if not provided
            if (configuration == null)
                configuration = new FileLoggerConfiguration();

            // Add file log provider to builder
            builder.AddProvider(new FileLoggerProvider(logPath, configuration));

            // Return the builder
            return builder;
        }

        /// <summary>
        /// Injects a file logger into the framework construction
        /// </summary>
        /// <param name="construction">The construction</param>
        /// <param name="logPath">The path of the folder to log to</param>
        /// <param name="logLevel">Log level start for logging</param>
        /// <param name="outputLogTime">Should time be logged?</param>
        /// <param name="outputLogLevel">Should log level be logged?</param>
        /// <param name="fileEncoding">File encoding spec.</param>
        /// <param name="useUtcTime">Use UTC (<see langword="true"/>) instead of the current local time (<see langword="false"/>).</param>
        /// <param name="singleLogFile">
        ///     <para>
        ///         <see langword="true"/>: Logger logs only into single file. Parameter <paramref name="logPath"/> specified the FILE path.
        ///     </para>
        ///     <para>
        ///         <see langword="false"/>: Logger creates log files by itself in hierarchical structure based on current datetime and the logger saves all these files into DIR (path) specified in <paramref name="logPath"/>.
        ///     </para>
        /// </param>
        public static FrameworkConstruction AddFileLogger(this FrameworkConstruction construction,
            string logPath = "log",
            LogLevel logLevel = LogLevel.Information,
            bool outputLogTime = true,
            bool outputLogLevel = true,
            Encoding fileEncoding = null,
            bool useUtcTime = false,
            bool singleLogFile = false
            )
        {
            // Make use of AddLogging extension
            construction.Services.AddLogging(options =>
            {
                // Create configuration object
                var configuration = new FileLoggerConfiguration
                {
                    LogLevel = logLevel,
                    OutputLogTime = outputLogTime,
                    OutputLogLevel = outputLogLevel,
                    UseUtcTime = useUtcTime,
                    SingleLogFile = singleLogFile,
                };
                if (fileEncoding != null) 
                    configuration.FileEncoding = fileEncoding;

                // Add file logger
                options.AddFile(logPath, configuration);
            });

            // Chain the construction
            return construction;
        }

    }
}
