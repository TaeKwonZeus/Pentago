using Microsoft.AspNetCore.Hosting;
using Pentago.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace Pentago.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}