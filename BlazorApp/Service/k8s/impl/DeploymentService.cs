using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DeploymentService(IKubeService kubeService, IDocService docService)
        : CommonAction<V1Deployment>, IDeploymentService
    {
        public new async Task<object> Delete(string ns, string name)
        {
            return await kubeService.Client().DeleteNamespacedDeploymentAsync(name, ns);
        }


        public async Task UpdateReplicas(V1Deployment item, int? replicas)
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
            var resp = await kubeService.Client().PatchNamespacedDeploymentScaleAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }

        public async Task Restart(V1Deployment item)
        {
            if (item == null) return;


            var patchStr = """
                    {
                           "spec": {
                             "template": {
                               "metadata": {
                                 "annotations": {
                                   "kubectl.kubernetes.io/restartedAt": "${now}",
                                   "kubectl.kubernetes.io/origin": "BlazorK8s"
                                 }
                               }
                             }
                           }
                    }
                    """
                    .Replace("${now}", DateTime.Now.ToLocalTime().ToString(CultureInfo.CurrentCulture))
                ;
            var resp = await kubeService.Client().PatchNamespacedDeploymentAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }


        public async Task<List<Result>> Analyze()
        {
            var items    = List();
            var results = new List<Result>();
            foreach (var item in items)
            {
                var failures = new List<Failure>();

                if (item.Status.Replicas != item.Spec.Replicas)
                {
                    var doc = await docService.GetExplainByField("deployment.spec.replicas");
                    failures.Add(new Failure
                    {
                        Text =
                            $"Deployment {item.Namespace()}/{item.Name()} should have {item.Spec.Replicas} but {item.Status.Replicas} available",
                        KubernetesDoc = doc?.Explain
                    });
                }


                if (failures.Count <= 0) continue;
                results.Add(Result.NewResult(item,failures));
            }

            return results;
        }
    }
}
