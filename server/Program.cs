using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using server.Utils;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            ServiceHelper.Services = host.Services;
            // var watcher = ServiceHelper.Services.GetService<Watcher>();
            // if (watcher != null) watcher.StartWatch();
            host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
