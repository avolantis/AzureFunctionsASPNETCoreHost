using System.Threading;
using Microsoft.Extensions.Hosting;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Host
{
    // TODO: implement me
    public class FunctionsApplicationLifetime: IHostApplicationLifetime
    {
        public void StopApplication()
        {
            throw new System.NotImplementedException();
        }

        public CancellationToken ApplicationStarted { get; }
        public CancellationToken ApplicationStopped { get; }
        public CancellationToken ApplicationStopping { get; }
    }
}