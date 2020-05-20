﻿using Avolantis.AspNetCore.FunctionsServer.UsageExample;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Avolantis.AspNetCore.FunctionsServer.UsageExample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
            => builder.UseStartup<ExampleWebApi.Startup>();
    }
}