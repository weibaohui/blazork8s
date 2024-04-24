using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class ReplicaSetService(IKubeService kubeService)
        : CommonAction<V1ReplicaSet>, IReplicaSetService
    {
        public new async Task<object> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNamespacedReplicaSetAsync(name, ns);
        }

        public async Task UpdateReplicas(V1ReplicaSet item, int? replicas)
        {
            if (item == null) return;

            if (replicas < 0)
            {
                replicas = 0;
            }

            var patchStr = """
                    {
                        "spec": {
                            "replicas":  ${replicas}
                        }
                    }
                    """
                    .Replace("${replicas}", replicas.ToString())
                ;
            var resp = await kubeService.Client().PatchNamespacedReplicaSetScaleAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }


        public Task<List<Result>> Analyze()
        {
            var items = List();
            var results = new List<Result>();
            foreach (var item in items.ToList())
            {
                var failures = new List<Failure>();

                //create failed
                if (item.Status.Replicas == 0)
                {
                    if (item.Status.Conditions is { Count: > 0 })
                    {
                        foreach (var status in item.Status.Conditions)
                        {
                            if (status.Type == "ReplicaFailure" && status.Reason == "FailedCreate")
                            {
                                failures.Add(new Failure
                                {
                                    Text = status.Message
                                });
                            }
                        }
                    }
                }

                if (failures.Count <= 0) continue;
                results.Add(Result.NewResult(item, failures));
            }

            if (results.Count == 0)
            {
                ClusterInspectionResultContainer.Instance.GetPassResources().Add("ReplicaSet");
            }

            ClusterInspectionResultContainer.Instance.AddResourcesCount("ReplicaSet", items.ToList().Count);

            return Task.FromResult(results);
        }
    }
}
