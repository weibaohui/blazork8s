using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using Blazor.MetaEvent;
using Blazor.Service;
using Blazor.Service.impl;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Blazor
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
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<INodeService, NodeService>();
            builder.Services.AddScoped<IPodService, PodService>();
            builder.Services.AddScoped<IDeploymentService, DeploymentService>();
            builder.Services.AddScoped<IReplicaSetService, ReplicaSetService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<INamespaceService, NamespaceService>();

            //webapi
            builder.Services.AddHttpApi<INodeApi>()
                .ConfigureNewtonsoftJson(o =>
                {
                    o.JsonSerializeOptions.NullValueHandling   = NullValueHandling.Ignore;
                    o.JsonDeserializeOptions.NullValueHandling = NullValueHandling.Ignore;
                });
            builder.Services.AddMemoryCache();
            await builder.Build().RunAsync();
        }
    }
}