using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class ReplicaSetService : CommonAction<V1ReplicaSet>, IReplicaSetService
    {
        private readonly IBaseService _baseService;

        public ReplicaSetService(IBaseService baseService)
        {
            _baseService = baseService;
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
            var resp = await _baseService.Client().AppsV1.PatchNamespacedReplicaSetScaleAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }
    }
}
