using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class DaemonSetService : CommonAction<V1DaemonSet>, IDaemonSetService
    {
        private readonly IBaseService                _baseService;

        public DaemonSetService(IBaseService baseService)
        {
            _baseService = baseService;
        }


    }
}
