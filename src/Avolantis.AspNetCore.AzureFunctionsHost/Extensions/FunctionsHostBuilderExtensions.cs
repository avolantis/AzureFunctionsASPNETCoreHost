#pragma warning disable 618 // Type or member obsolete
using System;
using System.Collections.Generic;
using System.Linq;
using Avolantis.AspNetCore.AzureFunctionsHost.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            var loggerProviders = ResolveAll<ILoggerProvider>(builder);
            var context = (HostBuilderContext)Resolve<HostBuilderContext>(builder).ImplementationInstance;

            var hostBuilder = Host
                .CreateDefaultBuilder()
                .ConfigureWebHostFunctionsDefaults(
                    webHostBuilder =>
                    {
                        webHostBuilder.UseEnvironment(context.HostingEnvironment.EnvironmentName);
                        webHostBuilder.UseConfiguration(context.Configuration); // Do we need this?
                        webHostBuilder.ConfigureLogging((context, loggingBuilder) =>
                        {
                            loggingBuilder.ClearProviders();
                            loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                            foreach (var serviceDescriptor in loggerProviders)
                            {
                                loggingBuilder.Services.AddSingleton(_ => serviceDescriptor.ImplementationFactory(_));
                            }
                            loggingBuilder.SetMinimumLevel(LogLevel.Trace); // Do we need this?
                        });
                        webHostBuilder.UseStartup<T>();
                    }
                );

            FunctionsServer.Instance = new FunctionsServer();
            hostBuilder.Build().Start();
        }

        public static ServiceDescriptor Resolve<T>(this IFunctionsHostBuilder builder) =>
            Resolve(builder, typeof(T));

        public static ServiceDescriptor Resolve(this IFunctionsHostBuilder builder, Type type) =>
            builder
                .Services
                .First(descriptor => descriptor.ServiceType == type);

        public static IEnumerable<ServiceDescriptor> ResolveAll<T>(this IFunctionsHostBuilder builder) =>
            builder
                .Services
                .Where(descriptor => descriptor.ServiceType == typeof(T));
    }
}