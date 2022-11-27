using k8s;

namespace BlazorApp.Service.impl
{
    public class BaseService : IBaseService
    {


        private readonly IKubeService _kubeService;
        public BaseService(IKubeService kubeService)
        {
            _kubeService   = kubeService;
        }


        public Kubernetes Client()
        {
            return _kubeService.Client();
        }












    }
}
