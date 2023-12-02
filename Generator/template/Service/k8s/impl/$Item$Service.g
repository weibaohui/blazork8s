using BlazorApp.Utils;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class $Item$Service : CommonAction<$ItemType$>, I$Item$Service
    {
        private readonly IBaseService                _baseService;

        public $Item$Service(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}
