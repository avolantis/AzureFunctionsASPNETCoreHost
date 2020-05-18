using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Host
{
    // TODO: implement me
    public class FunctionsHostLifetime: IHostLifetime
    {
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}