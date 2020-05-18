using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Endpoints
{
    public abstract class AspNetCoreProxyEndpoints
    {
        // ReSharper disable once MemberCanBeProtected.Global
        // ReSharper disable once UnusedMemberHierarchy.Global
        public virtual IActionResult HttpProxy(
            [HttpTrigger(AuthorizationLevel.Function,
                "get", "post", "put", "delete", "patch", "options",
                Route = "{*x:regex(^(?!admin|debug|monitoring).*$)}")]
            // ReSharper disable once UnusedParameter.Global
            HttpRequest request,
            CancellationToken token
        ) => new HttpActionResult(token);
    }
}