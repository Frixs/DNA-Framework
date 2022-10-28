using Microsoft.Extensions.DependencyInjection;

namespace Ixs.DNA.Hosting.Extensions
{
    /// <summary>
    ///     Extension methods for the <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Used in a hosted environment when using an existing set of services and configuration, such as 
        ///     in an ASP.Net Core environment
        /// </summary>
        /// <remarks>
        ///     Special for <see cref="HostedFrameworkConstruction"/> because it does not create service collection...
        ///     ...naturally for specific reason. <see cref="DefaultFrameworkConstruction"/> can be simply use for non-hosted
        ///     environment (e.g. MVVM WPF app). We don't have any other builder that we need to use (generally), so we can build it
        ///     by ourselves. Hosted app needs more care. We need to wrap it all around the hosted builder, we cannot just simply
        ///     build our <see cref="Framework"/>. So, we need to take the service collection (configuration) from the builder
        ///     and once the builder builds, provider is available for us, so we can use to build our framework using 
        ///     <see cref="ApplicationBuilderExtensions.UseDnaFramework"/> - usually in Startup.Configure method
        ///     which is standard pattern for hosted projects.
        ///     <para>
        ///         Interesting reading about history of host builders:
        ///         https://andrewlock.net/exploring-dotnet-6-part-2-comparing-webapplicationbuilder-to-the-generic-host/
        ///     </para>
        /// </remarks>
        /// <param name="services">The services to use</param>
        /// <returns></returns>
        public static FrameworkConstruction AddDnaFramework(this IServiceCollection services)
        {
            // Add the services into the Dna Framework
            Framework.Construction.UseHostedServices(services);

            // Return construction for chaining
            return Framework.Construction;
        }
    }
}