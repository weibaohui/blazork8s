using System;
using System.Globalization;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StatefulSetService : CommonAction<V1StatefulSet>, IStatefulSetService
{
    private readonly IBaseService                _baseService;

    public StatefulSetService(IBaseService baseService)
    {
        _baseService = baseService;
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
        var resp = await _baseService.Client().AppsV1.PatchNamespacedStatefulSetScaleAsync(
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
        var resp = await _baseService.Client().AppsV1.PatchNamespacedStatefulSetAsync(
            new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
            , item.Name(), item.Namespace());
    }
}
