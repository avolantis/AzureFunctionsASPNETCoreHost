using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Avolantis.AspNetCore.FunctionsServer
{
    public class HttpActionResult : IActionResult
    {
        private readonly CancellationToken _cancellationToken;

        public HttpActionResult(CancellationToken cancellationToken = new CancellationToken())
        {
            _cancellationToken = cancellationToken;
        }

        public async Task ExecuteResultAsync(ActionContext actionContext)
        {
            var app = Server.FunctionsServer.Instance.Application;
            var host = Server.FunctionsServer.Instance.Host;
            var scope = host.Services.CreateScope();

            // TODO: feature translation
            // https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.AspNetCoreServer/AbstractAspNetCoreFunction.cs#L451
            ((IServiceProvidersFeature) actionContext.HttpContext.Features[typeof(IServiceProvidersFeature)])
                .RequestServices = scope.ServiceProvider;

            var appContext = app.CreateContext(actionContext.HttpContext.Features);
            try
            {
                await app.ProcessRequestAsync(appContext);
            }
            catch (Exception ex)
            {
                app.DisposeContext(appContext, ex);
                throw;
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}