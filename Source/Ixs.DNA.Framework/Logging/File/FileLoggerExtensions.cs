using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
        /// <param name="path">The path where to write to</param>
        /// <param name="configuration">The configuration</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path, FileLoggerConfiguration configuration = null)
        {
            // Create default configuration if not provided
            if (configuration == null)
                configuration = new FileLoggerConfiguration();

            // Add file log provider to builder
            builder.AddProvider(new FileLoggerProvider(path, configuration));
            
            // Return the builder
            return builder;
        }

        /// <summary>
        /// Injects a file logger into the framework construction
        /// </summary>
        /// <param name="construction">The construction</param>
        /// <param name="logPath">The path of the file to log to</param>
        /// <param name="trimSize">Says what file size triggers the trimming to half of the logs in a file. Unit = Byte</param>
        /// <returns></returns>
        public static FrameworkConstruction AddFileLogger(this FrameworkConstruction construction, string logPath = "log.txt", LogLevel logLevel = LogLevel.Information, int trimSize = 0)
        {
            // Make use of AddLogging extension
            construction.Services.AddLogging(options =>
            {
                // Add file logger
                options.AddFile(logPath, new FileLoggerConfiguration 
                { 
                    LogLevel = logLevel,
                    LogTime = true,
                    TrimByteSize = trimSize,
                });
            });
            
            // Chain the construction
            return construction;
        }

    }
}
