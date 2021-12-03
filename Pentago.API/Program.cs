using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pentago.Engine;

namespace Pentago.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var board = new Board();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}