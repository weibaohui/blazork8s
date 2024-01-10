using System;
using System.Globalization;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DeploymentService : CommonAction<V1Deployment>, IDeploymentService
    {
        private readonly IBaseService _baseService;

        public DeploymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().AppsV1.DeleteNamespacedDeploymentAsync(name, ns);
        }


        public async Task UpdateReplicas(V1Deployment deploy, int? replicas)
        {
            if (deploy == null) return;

            if (replicas < 0)
            {
                replicas = 0;
            }

            var patchStr = """
                    {
                        "spec": {
                            "replicas":  ${deploy.Spec.Replicas}
                        }
                    }
                    """
                    .Replace("${deploy.Spec.Replicas}", replicas.ToString())
                ;
            var resp = await _baseService.Client().AppsV1.PatchNamespacedDeploymentScaleAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , deploy.Name(), deploy.Namespace());
        }

        public async Task Restart(V1Deployment deploy)
        {
            if (deploy == null) return;


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
                    .Replace("${now}", DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture))
                ;
            var resp = await _baseService.Client().AppsV1.PatchNamespacedDeploymentAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , deploy.Name(), deploy.Namespace());
        }
    }
}
