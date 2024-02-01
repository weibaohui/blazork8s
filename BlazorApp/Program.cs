using AntDesign.ProLayout;
using BlazorApp.Chat;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Service.AI.impl;
using BlazorApp.Service.impl;
using BlazorApp.Service.k8s;
using BlazorApp.Service.k8s.impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IKubeService, KubeService>();
builder.Services.AddSingleton<IMetricsService,MetricsService>();
builder.Services.AddHostedService<HostedService>();
builder.Services.AddHostedService<PortForwardService>();
builder.Services.AddHostedService<MetricsQueueWatchService>();

builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IPodService, PodService>();
builder.Services.AddScoped<IDeploymentService, DeploymentService>();
builder.Services.AddScoped<IDaemonSetService, DaemonSetService>();
builder.Services.AddScoped<IReplicaSetService, ReplicaSetService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INamespaceService, NamespaceService>();
builder.Services.AddScoped<IPageDrawerService, PageDrawerService>();
builder.Services.AddScoped<IKubectlService, KubectlService>();
builder.Services.AddScoped<IXunFeiAiService, XunFeiAiService>();
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IRockAiService, RockAiService>();
builder.Services.AddScoped<IQwenAiService, QwenAiService>();
builder.Services.AddScoped<IAiService, AiService>();
builder.Services.AddScoped<ITranslateService, TranslateService>();

builder.Services.AddScoped<IJobService,JobService>();
builder.Services.AddScoped<IServiceService,ServiceService>();
builder.Services.AddScoped<IServiceAccountService,ServiceAccountService>();
builder.Services.AddScoped<IClusterRoleService,ClusterRoleService>();
builder.Services.AddScoped<IClusterRoleBindingService,ClusterRoleBindingService>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IRoleBindingService,RoleBindingService>();
builder.Services.AddScoped<IIngressService,IngressService>();
builder.Services.AddScoped<IPersistentVolumeService,PersistentVolumeService>();
builder.Services.AddScoped<IPersistentVolumeClaimService,PersistentVolumeClaimService>();
builder.Services.AddScoped<IStorageClassService,StorageClassService>();
builder.Services.AddScoped<INetworkPolicyService,NetworkPolicyService>();
builder.Services.AddScoped<IIngressClassService,IngressClassService>();
builder.Services.AddScoped<IEndpointSliceService,EndpointSliceService>();
builder.Services.AddScoped<IEndpointsService,EndpointsService>();
builder.Services.AddScoped<ISecretService,SecretService>();
builder.Services.AddScoped<IPriorityClassService,PriorityClassService>();
builder.Services.AddScoped<IPodDisruptionBudgetService,PodDisruptionBudgetService>();
builder.Services.AddScoped<IValidatingWebhookConfigurationService,ValidatingWebhookConfigurationService>();
builder.Services.AddScoped<IMutatingWebhookConfigurationService,MutatingWebhookConfigurationService>();
builder.Services.AddScoped<ILimitRangeService,LimitRangeService>();
builder.Services.AddScoped<IHorizontalPodAutoscalerService,HorizontalPodAutoscalerService>();
builder.Services.AddScoped<IResourceQuotaService,ResourceQuotaService>();
builder.Services.AddScoped<IConfigMapService,ConfigMapService>();
builder.Services.AddScoped<ICronJobService,CronJobService>();
builder.Services.AddScoped<IStatefulSetService,StatefulSetService>();
builder.Services.AddScoped<ICustomResourceDefinitionService,CustomResourceDefinitionService>();
builder.Services.AddScoped<IReplicationControllerService,ReplicationControllerService>();

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
