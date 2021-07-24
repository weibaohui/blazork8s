using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ui.Service;
using ui.Service.impl;
namespace ui
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBootstrapBlazor();

            //
            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddSingleton<IBaseService,BaseService>();
            builder.Services.AddScoped<IBlogService,BlogService>();
            builder.Services.AddScoped<INodeService,NodeService>();
            builder.Services.AddScoped<IPodService,PodService>();
            builder.Services.AddSingleton<HttpClient>();
            await builder.Build().RunAsync();
        }
    }
}
