using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ui.Service;
using ui.Service.impl;
using WebApiClientCore.Serialization.JsonConverters;

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
