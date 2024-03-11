using System;
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
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPageDrawerService, PageDrawerService>();
builder.Services.AddHostedService<HostedService>();
builder.Services.AddHostedService<PortForwardService>();
builder.Services.AddHostedService<MetricsQueueWatchService>();

builder.Services.AddSingleton<IKubeService, KubeService>();
builder.Services.AddSingleton<IMetricsService,MetricsService>();
builder.Services.AddSingleton<ClusterInspectionService>();


builder.Services.AddSingleton<IConfigService, ConfigService>();
builder.Services.AddSingleton<INodeService, NodeService>();
builder.Services.AddSingleton<IPodService, PodService>();
builder.Services.AddSingleton<IDeploymentService, DeploymentService>();
builder.Services.AddSingleton<IDaemonSetService, DaemonSetService>();
builder.Services.AddSingleton<IReplicaSetService, ReplicaSetService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<INamespaceService, NamespaceService>();
builder.Services.AddSingleton<IKubectlService, KubectlService>();
builder.Services.AddSingleton<IXunFeiAiService, XunFeiAiService>();
builder.Services.AddSingleton<IOpenAiService, OpenAiService>();
builder.Services.AddSingleton<IRockAiService, RockAiService>();
builder.Services.AddSingleton<IQwenAiService, QwenAiService>();
builder.Services.AddSingleton<IAiService, AiService>();
builder.Services.AddSingleton<ITranslateService, TranslateService>();
builder.Services.AddSingleton<IDocService, DocService>();
builder.Services.AddSingleton<IJobService,JobService>();
builder.Services.AddSingleton<ILeaseService,LeaseService>();
builder.Services.AddSingleton<IServiceService,ServiceService>();
builder.Services.AddSingleton<IServiceAccountService,ServiceAccountService>();
builder.Services.AddSingleton<IClusterRoleService,ClusterRoleService>();
builder.Services.AddSingleton<IClusterRoleBindingService,ClusterRoleBindingService>();
builder.Services.AddSingleton<IRoleService,RoleService>();
builder.Services.AddSingleton<IRoleBindingService,RoleBindingService>();
builder.Services.AddSingleton<IIngressService,IngressService>();
builder.Services.AddSingleton<IPersistentVolumeService,PersistentVolumeService>();
builder.Services.AddSingleton<IPersistentVolumeClaimService,PersistentVolumeClaimService>();
builder.Services.AddSingleton<IStorageClassService,StorageClassService>();
builder.Services.AddSingleton<INetworkPolicyService,NetworkPolicyService>();
builder.Services.AddSingleton<IIngressClassService,IngressClassService>();
builder.Services.AddSingleton<IEndpointSliceService,EndpointSliceService>();
builder.Services.AddSingleton<IEndpointsService,EndpointsService>();
builder.Services.AddSingleton<ISecretService,SecretService>();
builder.Services.AddSingleton<IPriorityClassService,PriorityClassService>();
builder.Services.AddSingleton<IPodDisruptionBudgetService,PodDisruptionBudgetService>();
builder.Services.AddSingleton<IValidatingWebhookConfigurationService,ValidatingWebhookConfigurationService>();
builder.Services.AddSingleton<IMutatingWebhookConfigurationService,MutatingWebhookConfigurationService>();
builder.Services.AddSingleton<ILimitRangeService,LimitRangeService>();
builder.Services.AddSingleton<IHorizontalPodAutoscalerService,HorizontalPodAutoscalerService>();
builder.Services.AddSingleton<IResourceQuotaService,ResourceQuotaService>();
builder.Services.AddSingleton<IConfigMapService,ConfigMapService>();
builder.Services.AddSingleton<ICronJobService,CronJobService>();
builder.Services.AddSingleton<IStatefulSetService,StatefulSetService>();
builder.Services.AddSingleton<ICustomResourceDefinitionService,CustomResourceDefinitionService>();
builder.Services.AddSingleton<IReplicationControllerService,ReplicationControllerService>();
builder.Services.AddSingleton<IResourceCrudService, ResourceCrudService>();

builder.Services.AddSingleton
    <ISqlSugarClient>(s =>
{
    //Scoped用SqlSugarClient
    SqlSugarClient sqlSugar = new SqlSugarClient (new ConnectionConfig()
        {
            DbType                = SqlSugar.DbType.Sqlite,
            ConnectionString      = "DataSource=docs.db",
            IsAutoCloseConnection = true,
            LanguageType = LanguageType.Chinese,
        },
        db =>
        {
            //每次上下文都会执行


            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //获取原生SQL推荐 5.1.4.63  性能OK
                Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
            };
        });
    return sqlSugar;
});


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
