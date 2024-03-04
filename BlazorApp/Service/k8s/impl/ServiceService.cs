using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ServiceService(IKubeService kubeService, IEndpointsService endpointsService)
    : CommonAction<V1Service>, IServiceService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedServiceAsync(name, ns);
    }

    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();

        //通过EndPoints反向看Service，1是subset为空，说明Service Label可能有问题
        //2是notReadyAddress，说明有问题
        var eps = endpointsService.List();
        foreach (var ep in eps.ToList())
        {
            var failures = new List<Failure>();

            if (ep.Subsets is not { Count: > 0 })
            {
                //没有subset,说明可能是Selector label问题
                var svc = this.GetByName(ep.Namespace(), ep.Name());
                if (svc == null)
                {
                    //排除使用endpoint做选举的情况
                    //TODO 还有其他情况吗
                    if (ep.Annotations().Any(x => x.Key == "control-plane.alpha.kubernetes.io/leader"))
                    {
                        continue;
                    }

                    failures.Add(new Failure()
                    {
                        Text = $"Service {ep.Namespace()}/{ep.Name()} does not exist,but Endpoints exists",
                    });
                }
                else
                {
                    //Selector label 有问题
                    foreach (var (k, v) in svc.Spec.Selector)
                    {
                        failures.Add(new Failure()
                        {
                            Text = $"Service {ep.Namespace()}/{ep.Name()} does not match Pod by label {k}={v}",
                        });
                    }
                }
            }
            else
            {
                //检查subset 下的notReadyAddresses
                foreach (var subset in ep.Subsets)
                {
                    if (subset.NotReadyAddresses is not { Count: > 0 }) continue;
                    foreach (var addr in subset.NotReadyAddresses)
                    {

                        failures.Add(new Failure()
                        {
                            Text = $"Service Endpoints {ep.Namespace()}/{ep.Name()} is not ready at {addr.Ip},target to {addr.TargetRef.Kind} {addr.TargetRef.NamespaceProperty}/{addr.TargetRef.Name}",
                        });
                    }
                }
            }


            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(ep, failures));
        }
        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("Endpoints");
        }
        ClusterInspectionResultContainer.Instance.AddResourcesCount("Endpoints", items.ToList().Count);

        return results;
    }
}
