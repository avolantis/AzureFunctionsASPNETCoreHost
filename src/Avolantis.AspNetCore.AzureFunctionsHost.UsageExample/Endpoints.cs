using System.Threading;
using Avolantis.AspNetCore.AzureFunctionsHost.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Avolantis.AspNetCore.AzureFunctionsHost.UsageExample
{
    // ReSharper disable once UnusedType.Global
    public class Endpoints : AspNetCoreProxyEndpoints
    {
        [FunctionName("AspNetCoreHttpProxy")]
        public override IActionResult HttpProxy(
            [HttpTrigger(AuthorizationLevel.Function,
                "get", "post", "put", "delete", "patch", "options",
                Route = "{*x:regex(^(?!admin|debug|monitoring).*$)}")]
            HttpRequest request,
            CancellationToken token
        ) => base.HttpProxy(request, token);
    }
}