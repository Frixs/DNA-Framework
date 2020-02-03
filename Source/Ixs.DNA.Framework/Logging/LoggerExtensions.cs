using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Ixs.DNA
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/> loggers
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs a verbose trace message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogTraceSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Trace, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Trace), exception, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a debug message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogDebugSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Debug, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Debug), exception, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs an informative message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogInformationSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Information, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Information), exception, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a warning message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogWarningSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Warning, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Warning), exception, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs an error message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogErrorSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Error, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Error), exception, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a critical (fatal) message, including the source of the log
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="eventId">The event ID</param>
        /// <param name="exception">The exception</param>
        /// <param name="origin">The callers member/function name</param>
        /// <param name="filePath">The source code file path</param>
        /// <param name="lineNumber">The line number in the code file of the caller</param>
        /// <param name="args">The additional arguments</param>
        public static void LogCriticalSource(
            this ILogger logger,
            string message,
            EventId eventId = new EventId(),
            Exception exception = null,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args
            ) => logger?.Log(LogLevel.Critical, eventId, args.Prepend(origin, filePath, lineNumber, message, LogLevel.Critical), exception, LoggerSourceFormatter.Format);
    }
}
