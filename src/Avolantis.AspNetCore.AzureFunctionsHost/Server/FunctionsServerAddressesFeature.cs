using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Server
{
    public class FunctionsServerAddressesFeature : IServerAddressesFeature
    {
        public ICollection<string> Addresses { get; } = new List<string>
        {
            Environment.GetEnvironmentVariable("WEBSITE_URL")
        };

        public bool PreferHostingUrls { get; set; } = false;
    }
}