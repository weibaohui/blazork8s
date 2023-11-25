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
    }
}