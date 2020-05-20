using System;
using System.Threading;
using System.Threading.Tasks;
using Avolantis.AspNetCore.FunctionsServer.Host;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;

namespace Avolantis.AspNetCore.FunctionsServer.Server
{
    public class FunctionsServer : IServer
    {
        public ApplicationWrapper Application { get; internal set; }
        public IHost Host { get; internal set; }

        public void Dispose()
        {
            Host?.Dispose();
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