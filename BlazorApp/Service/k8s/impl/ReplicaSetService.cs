using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class ReplicaSetService : CommonAction<V1ReplicaSet>, IReplicaSetService
    {
        private readonly IKubeService _baseService;

        public ReplicaSetService(IKubeService baseService)
        {
            _baseService = baseService;
        }
        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().DeleteNamespacedReplicaSetAsync(name, ns);
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
            var resp = await _baseService.Client().PatchNamespacedReplicaSetScaleAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }
    }
}
