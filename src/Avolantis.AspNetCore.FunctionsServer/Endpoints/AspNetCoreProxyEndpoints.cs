using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avolantis.AspNetCore.FunctionsServer.Endpoints
{
    public abstract class AspNetCoreProxyEndpoints
    {
        // ReSharper disable once MemberCanBeProtected.Global
        // ReSharper disable once UnusedMemberHierarchy.Global
        public virtual IActionResult HttpProxy(
            // ReSharper disable once UnusedParameter.Global
            HttpRequest request,
            CancellationToken token
        ) => new HttpActionResult(token);
    }
}