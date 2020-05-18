using System;
using System.Threading;
using System.Threading.Tasks;
using Avolantis.AspNetCore.AzureFunctionsHost.Host;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Server
{
    public class FunctionsServer : IServer
    {
        public ApplicationWrapper Application;

        public void Dispose()
        {
            // TODO: Is the host needed to be disposed?
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            Application = new ApplicationWrapper<TContext>(application);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public static FunctionsServer Instance { get; internal set; }
        public IFeatureCollection Features { get; } = new FunctionsServerFeatureCollection();
        public static string ContentRoot { get; } = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
    }
}