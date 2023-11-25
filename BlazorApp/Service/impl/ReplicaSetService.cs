using k8s.Models;

namespace BlazorApp.Service.impl
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