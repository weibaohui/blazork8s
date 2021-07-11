using System;
using System.Threading.Tasks;
using System.Web;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using server.Service;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KubeApiController:ControllerBase
    {
        private readonly ILogger<KubeApiController> _logger;

        public KubeApiController(ILogger<KubeApiController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 通用获取资源的方法
        /// </summary>
        /// <param name="api">资源访问地址 /api/v1/namespaces</param>
        /// <returns>JSON格式</returns>
        [HttpGet]
        [Route("{*api}")]
        public Task<string> GetResourceJson(string api)
        {
            //默认增加/根路径
            api=HttpUtility.UrlDecode(api);
            return Kubectl.Instance.GetResourceJson($"/{api}");
        }
    }
}
