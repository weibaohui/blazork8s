using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ui
{
    public class Program 
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("11111");
            

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            Console.WriteLine("yyyyy");

            
            
            await builder.Build().RunAsync();
            
            Console.WriteLine("xxxx");
            
        }

    }
}
