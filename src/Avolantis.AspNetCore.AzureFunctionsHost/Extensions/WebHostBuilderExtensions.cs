using System.Collections.Generic;
using System.Linq;
using Avolantis.AspNetCore.AzureFunctionsHost.Server;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Hosting
{
    /// <summary>
    /// This class is a container for extensions methods to the <see cref="IWebHostBuilder"/> interface.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Extension method for configuring Azure Functions as the server for an ASP.NET Core application.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseFunctionsServer(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services =>
            {
                IList<ServiceDescriptor> toRemove = new List<ServiceDescriptor>();

                var serviceDescriptions = services
                    .Where(x => x.ServiceType == typeof(IServer));

                var count = 0;

                // This makes sure there is only one registered IServer using FunctionsServer
                foreach (var serviceDescription in serviceDescriptions)
                {
                    if (serviceDescription.ImplementationType == typeof(FunctionsServer))
                    {
                        count++;

                        // If more then one FunctionsServer has been registered
                        // then remove the extra registrations.
                        if (count > 1) toRemove.Add(serviceDescription);
                    }

                    // If there is an IServer registered that isn't FunctionsServer
                    // then remove it. This is most likely caused by leaving the UseKestrel call.
                    else
                        toRemove.Add(serviceDescription);
                }

                foreach (var serviceDescription in toRemove) services.Remove(serviceDescription);

                if (count == 0) services.AddSingleton<IServer>(FunctionsServer.Instance);
            });
        }
    }
}