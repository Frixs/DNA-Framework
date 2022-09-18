using Microsoft.Extensions.Hosting;
using System;

namespace Ixs.DNA.AspNet
{
    /// <summary>
    ///     Extensions for <see cref="IHostBuilder"/>
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        ///     Adds the Dna Framework construct to the ASP.Net Core application
        /// </summary>
        /// <param name="builder">The host builder</param>
        /// <param name="configure">Custom action to configure the Dna Framework</param>
        /// <returns>Return the builder for further chaining.</returns>
        public static IHostBuilder UseDnaFramework(this IHostBuilder builder, Action<FrameworkConstruction> configure = null)
        {
            builder.ConfigureServices((context, services) =>
            {
                // Construct a hosted Dna Framework
                Framework.Construct<HostedFrameworkConstruction>();

                // Setup this service collection to
                // be used by DnaFramework 
                services.AddDnaFramework()
                    // Add configuration
                    .AddConfiguration(context.Configuration)
                    // Add default services
                    .AddDefaultServices();

                // Fire off construction configuration
                configure?.Invoke(Framework.Construction);

                // NOTE: Framework will do .Build() from the Startup.cs Configure call
                //       app.UseDnaFramework()
            });

            // Return builder for chaining
            return builder;
        }
    }
}
