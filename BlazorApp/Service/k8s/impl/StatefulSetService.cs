using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Entity.Analyze;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StatefulSetService(IKubeService kubeService, IDocService docService,IServiceService svcService) : CommonAction<V1StatefulSet>, IStatefulSetService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedStatefulSetAsync(name, ns);
    }
    public async Task UpdateReplicas(V1StatefulSet item, int? replicas)
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
        var resp = await kubeService.Client().PatchNamespacedStatefulSetScaleAsync(
            new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
            , item.Name(), item.Namespace());
    }

    public async Task Restart(V1StatefulSet item)
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
        var resp = await kubeService.Client().PatchNamespacedStatefulSetAsync(
            new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
            , item.Name(), item.Namespace());
    }


    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();
        var doc     = await docService.GetExplainByField("statefulset.spec.serviceName");
        foreach (var item in items)
        {
            var failures = new List<Failure>();
            //check spec.serviceName
            if (item.Spec.ServiceName.IsNullOrWhiteSpace())
            {
                failures.Add(new Failure
                {
                    Text = $"StatefulSet  {item.Namespace()}/{item.Name()} spec.serviceName  not set",
                    KubernetesDoc = doc?.Explain
                });
            }
            else
            {
                var svcName = item.Spec.ServiceName;
                //检查是否存在
                var svc = svcService.GetByName(item.Namespace(),svcName);
                if (svc == null)
                {
                    failures.Add(new Failure
                    {
                        Text = $"StatefulSet  {item.Namespace()}/{item.Name()} spec.serviceName {svcName} not exist",
                        KubernetesDoc = doc?.Explain
                    });

                }
            }

            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item,failures));
        }

        return results;
    }
}
