using Avolantis.AspNetCore.AzureFunctionsHost.UsageExample;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Avolantis.AspNetCore.AzureFunctionsHost.UsageExample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
            => builder.UseStartup<ExampleWebApi.Startup>();
    }
}