using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using k8s;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
