using System.Collections.Generic;
using Avolantis.AspNetCore.FunctionsServer.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#pragma warning disable 618 // Type or member obsolete
using IApplicationLifetime = Microsoft.Extensions.Hosting.IApplicationLifetime;

#pragma warning restore 618

// ReSharper disable once CheckNamespace
namespace Microsoft.Azure.Functions.Extensions.DependencyInjection
{
    public static class FunctionsHostBuilderExtensions
    {
        public static void UseStartup<T>(this IFunctionsHostBuilder builder) where T : class
        {
            // TODO: Wire up lifetime
#pragma warning disable 618 // Type or member obsolete
            var applicationLifetime = Resolve<IApplicationLifetime>(builder);
#pragma warning restore 618
            var hostLifetime = Resolve<IHostLifetime>(builder);

            var context = (HostBuilderContext) Resolve<HostBuilderContext>(builder).ImplementationInstance;
            var hostBuilder = Host
                .CreateDefaultBuilder()
                .ConfigureWebHostFunctionsDefaults(
                    webHostBuilder =>
                    {
                        webHostBuilder.UseEnvironment(context.HostingEnvironment.EnvironmentName);
                        webHostBuilder.UseConfiguration(context.Configuration); // Do we need this?
                        webHostBuilder.UseStartup<T>();
                    }
                );

            FunctionsServer.Instance = new FunctionsServer();
            var host = hostBuilder.Build();
            FunctionsServer.Instance.Host = host;
            host.Start();
        }

        public static ServiceDescriptor Resolve<T>(this IFunctionsHostBuilder builder) =>
            builder.Services.Resolve(typeof(T));

        public static IEnumerable<ServiceDescriptor> ResolveAll<T>(this IFunctionsHostBuilder builder) =>
            builder.Services.ResolveAll(typeof(T));
    }
}