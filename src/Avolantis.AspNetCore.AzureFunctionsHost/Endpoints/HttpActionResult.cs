using System.Threading;
using System.Threading.Tasks;
using Avolantis.AspNetCore.AzureFunctionsHost.Server;
using Microsoft.AspNetCore.Mvc;

namespace Avolantis.AspNetCore.AzureFunctionsHost
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
            var app = FunctionsServer.Instance.Application;
            var appContext = app.CreateContext(actionContext.HttpContext.Features);
            
            await app.ProcessRequestAsync(appContext);
        }
    }
}