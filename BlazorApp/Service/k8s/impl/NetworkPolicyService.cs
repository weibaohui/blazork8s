using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class NetworkPolicyService(IKubeService kubeService,IPodService podService) : CommonAction<V1NetworkPolicy>, INetworkPolicyService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedNetworkPolicyAsync(name, ns);
    }
    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();
        foreach (var item in items.ToList())
        {
            var failures = new List<Failure>();

            var labels = item.Spec?.PodSelector?.MatchLabels;
            if (labels == null)
            {
                failures.Add(new Failure()
                {
                    Text = $"NetworkPolicy {item.Namespace()}/{item.Name()} spec.podSelector is empty,Allow Or Deny all pods"

                });
            }
            else
            {
                var pods = await podService.FilterPodByLabels(item.Namespace(), labels);
                if (pods is not { Count: > 0 })
                {
                    failures.Add(new Failure()
                    {
                        Text = $"NetworkPolicy {item.Namespace()}/{item.Name()} is not applied to any Pods"
                    });
                }
            }

            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item,failures));
        }
        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("NetworkPolicy");
        }

        ClusterInspectionResultContainer.Instance.GetAllResourcesCount().Add("NetworkPolicy", items.ToList().Count);

        return results;
    }

}
