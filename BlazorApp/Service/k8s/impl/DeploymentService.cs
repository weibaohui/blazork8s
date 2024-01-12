using System;
using System.Globalization;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DeploymentService : CommonAction<V1Deployment>, IDeploymentService
    {
        private readonly IKubeService _baseService;

        public DeploymentService(IKubeService baseService)
        {
            _baseService = baseService;
        }

        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().DeleteNamespacedDeploymentAsync(name, ns);
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
            var resp = await _baseService.Client().PatchNamespacedDeploymentScaleAsync(
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
            var resp = await _baseService.Client().PatchNamespacedDeploymentAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }
    }
}
