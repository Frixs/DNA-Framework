using Ixs.DNA.Extensions;
using Ixs.DNA.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using static Ixs.DNA.FrameworkDI;

namespace Ixs.DNA
{
    /// <summary>
    ///     The main entry point into the Dna Framework library
    /// </summary>
    /// <remarks>
    /// <para>
    ///     To use Dna.Framework you need to create a new <see cref="FrameworkConstruction"/>
    ///     such as <see cref="DefaultFrameworkConstruction"/> and then add your services
    ///     then finally <see cref="Framework.Build(FrameworkConstruction, bool)"/>. For example:
    /// </para>
    /// <code>
    /// 
    ///     // Create the default framework and build it
    ///     Framework.Construct&lt;DefaultFrameworkConstruction&gt;().Build();
    ///     
    /// </code>
    /// </remarks>
    public static class Framework
    {
        #region Public Properties

        /// <summary>
        ///     The framework construction used in this application.
        ///     NOTE: This should be set by the consuming application at the very start of the program
        /// </summary>
        /// <example>
        /// <code>
        ///     Framework.Construct&lt;DefaultFrameworkConstruction&gt;();
        /// </code>
        /// </example>
        public static FrameworkConstruction Construction { get; private set; }

        /// <summary>
        ///     The dependency injection service provider
        /// </summary>
        public static IServiceProvider Provider => Construction?.Provider;

        #endregion

        #region Extension Methods

        /// <summary>
        ///     Should be called once a Framework Construction is finished and we want to build it and
        ///     start our application
        /// </summary>
        /// <param name="construction">The construction</param>
        /// <param name="logStarted">Specifies if the Dna Framework Started message should be logged</param>
        public static void Build(this FrameworkConstruction construction, bool logStarted = true)
        {
            // Build the service provider
            construction.Build();

            // Log the startup complete
            if (logStarted)
                Logger.LogCriticalSource($"Ixs.DNA Framework started in {FrameworkEnvironment.Configuration}...");
        }

        /// <summary>
        ///     Should be called once a Framework Construction is finished and we want to build it and
        ///     start our application in a hosted environment where the service provider is already built
        ///     such as ASP.Net Core applications
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <param name="logStarted">Specifies if the Dna Framework Started message should be logged</param>
        public static void Build(IServiceProvider provider, bool logStarted = true)
        {
            // Build the service provider
            Construction.Build(provider);

            // Log the startup complete
            if (logStarted)
                Logger.LogCriticalSource($"Ixs.DNA Framework started in {FrameworkEnvironment.Configuration}...");
        }

        /// <summary>
        ///     The initial call to setting up and using the Dna Framework
        /// </summary>
        /// <typeparam name="T">The type of construction to use</typeparam>
        public static FrameworkConstruction Construct<T>()
            where T : FrameworkConstruction, new()
        {
            Construction = new T();

            // Return construction for chaining
            return Construction;
        }


        /// <summary>
        ///     The initial call to setting up and using the Dna Framework.
        /// </summary>
        /// <typeparam name="T">The type of construction to use</typeparam>
        /// <param name="constructionInstance">The instance of the construction to use</param>
        public static FrameworkConstruction Construct<T>(T constructionInstance)
            where T : FrameworkConstruction
        {
            // Set construction
            Construction = constructionInstance;

            // Return construction for chaining
            return Construction;
        }

        #endregion

        #region Shortcut Methods

        /// <summary>
        ///     Shortcut to Framework.Provider.GetService to get an injected service of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of service to get</typeparam>
        /// <returns></returns>
        public static T Service<T>()
        {
            // Use provider to get the service
            return Provider.GetService<T>();
        }

        /// <summary>
        ///     Shortcut to get Environment variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>Value represented as <see langword="string"/> or <see langword="null"/> on failure.</returns>
        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

        /// <summary>
        ///     Shortcut to get Environment variable with parsing the value.
        /// </summary>
        /// <typeparam name="T">Desired type that the values should be parsed to (default <see langword="string"/>).</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <returns>Parsed value in <typeparamref name="T"/> or <see langword="null"/> on failure.</returns>
        /// <remarks>
        ///     For supported types in parsing, see <see cref="StringExtensions.ParseValue{T}"/>.
        /// </remarks>
        public static T? GetEnvironmentVariable<T>(string name)
            where T : struct
        {
            string val = Environment.GetEnvironmentVariable(name);
            return val?.ParseValue<T>();
        }

        /// <summary>
        ///     Shortcut to get Environment variable.
        ///     If the process fails, <see cref="GetConfigurationValue(string)"/> gets fired as backup process to get the desired value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="backupConfigurationValueKey">Section key to navigate to the configuration value.</param>
        /// <returns>Value represented as <see langword="string"/> or <see langword="null"/> on failure.</returns>
        public static string GetEnvironmentVariable(string name, string backupConfigurationValueKey)
        {
            return GetEnvironmentVariable(name) ?? GetConfigurationValue(backupConfigurationValueKey);
        }

        /// <summary>
        ///     Shortcut to get Environment variable with parsing the value.
        ///     If the process fails, <see cref="GetConfigurationValue{T}(string)"/> gets fired as backup process to get the desired value.
        /// </summary>
        /// <typeparam name="T">Desired type that the values should be parsed to (default <see langword="string"/>).</typeparam>
        /// <param name="name">The name of the variable.</param>
        /// <param name="backupConfigurationValueKey">Section key to navigate to the configuration value.</param>
        /// <returns>Parsed value in <typeparamref name="T"/> or <see langword="null"/> on failure.</returns>
        /// <remarks>
        ///     For supported types in parsing, see <see cref="StringExtensions.ParseValue{T}"/>.
        /// </remarks>
        public static T? GetEnvironmentVariable<T>(string name, string backupConfigurationValueKey)
            where T : struct
        {
            return GetEnvironmentVariable<T>(name) ?? GetConfigurationValue<T>(backupConfigurationValueKey);
        }

        /// <summary>
        ///     Shortcut to Construction to get configuration value.
        /// </summary>
        /// <param name="key">Section key to navigate to the configuration value.</param>
        /// <returns>Value represented as <see langword="string"/> or <see langword="null"/> on failure.</returns>
        public static string GetConfigurationValue(string key)
        {
            var csec = Construction.Configuration.GetSection(key);
            return csec?.Value;
        }

        /// <summary>
        ///     Shortcut to Construction to get configuration value with parsing the value.
        /// </summary>
        /// <typeparam name="T">Desired type that the values should be parsed to (default <see langword="string"/>).</typeparam>
        /// <param name="key">Section key to navigate to the configuration value.</param>
        /// <returns>Parsed value in <typeparamref name="T"/> or <see langword="null"/> on failure.</returns>
        /// <remarks>
        ///     For supported types in parsing, see <see cref="StringExtensions.ParseValue{T}"/>.
        /// </remarks>
        public static T? GetConfigurationValue<T>(string key)
            where T : struct
        {
            var csec = Construction.Configuration.GetSection(key);
            return csec?.Value?.ParseValue<T>();
        }

        /// <summary>
        ///     Shortcut to Construction to get configuration value.
        ///     If the process fails, <see cref="GetEnvironmentVariable(string)"/> gets fired as backup process to get the desired value.
        /// </summary>
        /// <param name="key">Section key to navigate to the configuration value.</param>
        /// <param name="backupEnvironmentVariableName">The name of the environment variable.</param>
        /// <returns>Value represented as <see langword="string"/> or <see langword="null"/> on failure.</returns>
        public static string GetConfigurationValue(string key, string backupEnvironmentVariableName)
        {
            return GetConfigurationValue(key) ?? GetEnvironmentVariable(backupEnvironmentVariableName);
        }

        /// <summary>
        ///     Shortcut to Construction to get configuration value with parsing the value.
        ///     If the process fails, <see cref="GetEnvironmentVariable{T}(string)"/> gets fired as backup process to get the desired value.
        /// </summary>
        /// <typeparam name="T">Desired type that the values should be parsed to (default <see langword="string"/>).</typeparam>
        /// <param name="key">Section key to navigate to the configuration value.</param>
        /// <param name="backupEnvironmentVariableName">The name of the environment variable.</param>
        /// <returns>Parsed value in <typeparamref name="T"/> or <see langword="null"/> on failure.</returns>
        /// <remarks>
        ///     For supported types in parsing, see <see cref="StringExtensions.ParseValue{T}"/>.
        /// </remarks>
        public static T? GetConfigurationValue<T>(string key, string backupEnvironmentVariableName)
            where T : struct
        {
            return GetConfigurationValue<T>(key) ?? GetEnvironmentVariable<T>(backupEnvironmentVariableName);
        }

        #endregion
    }
}
