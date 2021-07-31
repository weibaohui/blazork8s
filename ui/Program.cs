using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddScoped<IBaseService,BaseService>();
            builder.Services.AddScoped<INodeService,NodeService>();
            builder.Services.AddScoped<IPodService,PodService>();

            //webapi
            builder.Services.AddHttpApi<INodeApi>()
                .ConfigureNewtonsoftJson(o =>
                {
                    o.JsonSerializeOptions.NullValueHandling   = NullValueHandling.Ignore;
                    o.JsonDeserializeOptions.NullValueHandling = NullValueHandling.Ignore;
                });;
            await builder.Build().RunAsync();
        }
    }
}
