using AntDesign.ProLayout;
using BlazorApp.Chat;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddAntDesign();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IKubeService, KubeService>();
builder.Services.AddSingleton<IBaseService, BaseService>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IPodService, PodService>();
builder.Services.AddScoped<IDeploymentService, DeploymentService>();
builder.Services.AddScoped<IReplicaSetService, ReplicaSetService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INamespaceService, NamespaceService>();
builder.Services.AddScoped<IPageDrawerService, PageDrawerService>();
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IKubectlService, KubectlService>();
builder.Services.AddScoped<IRockAiService, RockAiService>();
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

builder.Services.AddHostedService<ListWatchService>();


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
app.MapHub<ChatHub>("/chathub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
