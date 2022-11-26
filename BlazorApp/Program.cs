using System;
using AntDesign.ProLayout;
using BlazorApp.Data;
using BlazorApp.Service;
using k8s;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IKubeService,KubeService>();

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

// // Load from the default kubeconfig on the machine.
// var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
// // Use the config object to create a client.
// var client     = new Kubernetes(config);
// var namespaces = client.CoreV1.ListNamespace();
// foreach (var ns in namespaces.Items)
// {
//     Console.WriteLine(ns.Metadata.Name);
//     var list = client.CoreV1.ListNamespacedPod(ns.Metadata.Name);
//     foreach (var item in list.Items)
//     {
//         Console.WriteLine(item.Metadata.Name);
//     }
// }

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
