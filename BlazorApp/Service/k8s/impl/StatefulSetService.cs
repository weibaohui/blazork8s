using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using Extension;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StatefulSetService(IKubeService kubeService,
    IServiceService svcService,
    IStorageClassService storageClassService) : CommonAction<V1StatefulSet>, IStatefulSetService
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
        foreach (var item in items.ToList())
        {
            var failures = new List<Failure>();
            //check spec.serviceName
            if (item.Spec.ServiceName.IsNullOrWhiteSpace())
            {
                failures.Add(new Failure
                {
                    Text = $"StatefulSet  {item.Namespace()}/{item.Name()} spec.serviceName  not set",
                    KubernetesDocField = "statefulSet.spec.serviceName",
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
                        KubernetesDocField = "statefulSet.spec.serviceName",
                    });

                }
            }


            //检查存储配置，尤其是storageClassName
            if(item.Spec.VolumeClaimTemplates  is {Count: > 0})
            {
                foreach (var pvcTemplate in item.Spec.VolumeClaimTemplates)
                {
                    if (pvcTemplate.Spec.StorageClassName.IsNullOrWhiteSpace()) continue;
                    var storageClass = storageClassService.GetByName(pvcTemplate.Spec.StorageClassName);
                    if (storageClass == null)
                    {

                        failures.Add(new Failure
                        {
                            Text          = $"StatefulSet  {item.Namespace()}/{item.Name()} use storageClass {pvcTemplate.Spec.StorageClassName} not exist",
                        });
                    }
                }
            }

            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item,failures));
        }

        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("StatefulSet");
        }
        ClusterInspectionResultContainer.Instance.AddResourcesCount("StatefulSet", items.ToList().Count);

        return results;
    }
}
