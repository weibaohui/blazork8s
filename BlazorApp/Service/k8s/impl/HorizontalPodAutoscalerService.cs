using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class HorizontalPodAutoscalerService(
    IKubeService                  kubeService,
    IDeploymentService            deploymentService,
    IReplicaSetService            replicaSetService,
    IStatefulSetService           statefulSetService,
    IReplicationControllerService replicationControllerService)
    : CommonAction<V1HorizontalPodAutoscaler>, IHorizontalPodAutoscalerService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().AutoscalingV2.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }

    public async Task<object> V1Delete(string ns, string name)
    {
        return await kubeService.Client().AutoscalingV1.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }

    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();
        foreach (var item in items)
        {
            var failures = new List<Failure>();

            var targetRef = item.Spec?.ScaleTargetRef;
            if (targetRef == null)
            {
                failures.Add(new Failure()
                {
                    Text = $"HPA {item.Namespace()}/{item.Name()} spec.scaleTargetRef is empty"
                });
            }
            else
            {
                //检查对象是否存在
                switch (targetRef.Kind)
                {
                    case "Deployment":
                        var t = deploymentService.GetByName(item.Namespace(), targetRef.Name);
                        if (t == null)
                        {
                            failures.Add(new Failure()
                            {
                                Text =
                                    $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} is not exists"
                            });
                        }
                        else
                        {
                            var anyContainerSetResourced = t.Spec?.Template?.Spec?.Containers.Any(x =>
                                x.Resources?.Requests != null || x.Resources?.Limits != null);
                            if (anyContainerSetResourced is not true)
                            {
                                failures.Add(new Failure()
                                {
                                    Text =
                                        $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} all containers are not set resource request/limit"
                                });
                            }
                        }

                        break;
                    case "StatefulSet":

                        var s = statefulSetService.GetByName(item.Namespace(), targetRef.Name);
                        if (s == null)
                        {
                            failures.Add(new Failure()
                            {
                                Text =
                                    $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} is not exists"
                            });
                        }
                        else
                        {
                            var anyContainerSetResourced = s.Spec?.Template?.Spec?.Containers.Any(x =>
                                x.Resources?.Requests != null || x.Resources?.Limits != null);
                            if (anyContainerSetResourced is not true)
                            {
                                failures.Add(new Failure()
                                {
                                    Text =
                                        $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} all containers are not set resource request/limit"
                                });
                            }
                        }

                        break;
                    case "ReplicaSet":
                        var r = replicaSetService.GetByName(item.Namespace(), targetRef.Name);
                        if (r == null)
                        {
                            failures.Add(new Failure()
                            {
                                Text =
                                    $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} is not exists"
                            });
                        }
                        else
                        {
                            var anyContainerSetResourced = r.Spec?.Template?.Spec?.Containers.Any(x =>
                                x.Resources?.Requests != null || x.Resources?.Limits != null);
                            if (anyContainerSetResourced is not true)
                            {
                                failures.Add(new Failure()
                                {
                                    Text =
                                        $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} all containers are not set resource request/limit"
                                });
                            }
                        }

                        break;
                    case "ReplicationController":
                        var rc = replicationControllerService.GetByName(item.Namespace(), targetRef.Name);
                        if (rc == null)
                        {
                            failures.Add(new Failure()
                            {
                                Text =
                                    $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} is not exists"
                            });
                        }
                        else
                        {
                            var anyContainerSetResourced = rc.Spec?.Template?.Spec?.Containers.Any(x =>
                                x.Resources?.Requests != null || x.Resources?.Limits != null);
                            if (anyContainerSetResourced is not true)
                            {
                                failures.Add(new Failure()
                                {
                                    Text =
                                        $"HPA {item.Namespace()}/{item.Name()} scale target {targetRef.Kind} {item.Namespace()}/{targetRef.Name} all containers are not set resource request/limit"
                                });
                            }
                        }

                        break;
                    default:
                        failures.Add(new Failure()
                        {
                            Text = $"HPA {item.Namespace()}/{item.Name()} spec.scaleTargetRef.kind is invalid"
                        });
                        break;
                }
            }

            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item, failures));
        }

        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("HPA");
        }

        return results;
    }
}
