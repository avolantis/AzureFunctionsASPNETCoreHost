using System;
using Avolantis.AspNetCore.FunctionsServer.Server;
using Microsoft.AspNetCore.Hosting;

#if !NETCOREAPP_2_1
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWebHostFunctionsDefaults(this IHostBuilder builder,
            Action<IWebHostBuilder> configure)
        {
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseContentRoot(FunctionsServer.ContentRoot)
                    .UseUrls(Environment.GetEnvironmentVariable("WEBSITE_URL"));

                webBuilder.UseFunctionsServer();

                configure(webBuilder);
            });

            return builder;
        }
    }
}
#endif