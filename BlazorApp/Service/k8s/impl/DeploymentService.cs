using System;
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

            Console.WriteLine($"UpdateReplicas {replicas} {deploy.Name()} {deploy.Namespace()}");
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
    }
}
