using System;
using System.Threading.Tasks;
using BlazorApp.Utils;
using FluentScheduler;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

/// <summary>
/// 集群巡检，每10秒执行一次
/// </summary>
/// <param name="kubeService"></param>
/// <param name="podService"></param>
/// <param name="deploymentService"></param>
/// <param name="logger"></param>
public class ClusterInspectionService(
    IKubeService                      kubeService,
    IPodService                       podService,
    IDeploymentService                deploymentService,
    IStatefulSetService               statefulSetService,
    IReplicaSetService                replicaSetService,
    INodeService                      nodeService,
    ICronJobService  cronJobService,
    ILogger<ClusterInspectionService> logger
)

{
    private async void Inspection()
    {
        logger.LogInformation("cluster inspection start at {Time}", DateTime.Now);
        var results = ClusterInspectionResultContainer.Instance.GetResults();
        results.Clear();
        ClusterInspectionResultContainer.Instance.GetPassResources().Clear();
        results.AddRange(await podService.Analyze());
        results.AddRange(await deploymentService.Analyze());
        results.AddRange(await replicaSetService.Analyze());
        results.AddRange(await statefulSetService.Analyze());
        results.AddRange(await nodeService.Analyze());
        results.AddRange(await cronJobService.Analyze());
        ClusterInspectionResultContainer.Instance.LastInspection = DateTime.Now.ToUniversalTime();
        logger.LogInformation("cluster inspection ended at {Time}", DateTime.Now);
    }

    public Task StartAsync()
    {
        //每10秒执行一次指标更新
        JobManager.Initialize();
        JobManager.AddJob(Inspection, (s) => s.ToRunEvery(10).Seconds());
        return Task.CompletedTask;
    }
}
