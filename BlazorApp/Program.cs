using AntDesign.ProLayout;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddSingleton<IKubeService, KubeService>();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IPodService, PodService>();
builder.Services.AddScoped<IDeploymentService, DeploymentService>();
builder.Services.AddScoped<IReplicaSetService, ReplicaSetService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INamespaceService, NamespaceService>();
builder.Services.AddScoped<IPageDrawerService, PageDrawerService>();
builder.Services.AddScoped<IWatchService, WatchService>();
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IKubectlService, KubectlService>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
