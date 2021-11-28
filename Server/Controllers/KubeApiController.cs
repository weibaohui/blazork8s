using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KubeApiController : ControllerBase
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
            api = HttpUtility.UrlDecode(api);
            return Kubectl.Instance.GetResourceJson($"/{api}");
        }

        [HttpDelete]
        [Route("{*api}")]
        public Task<bool> DeleteResourceJson(string api)
        {
            //默认增加/根路径
            api = HttpUtility.UrlDecode(api);
            return Kubectl.Instance.DeleteResourceJson($"/{api}");
        }

        [HttpPatch]
        [Route("{*api}")]
        public async Task<bool> PatchResourceJson(string api)
        {
            // curl -X 'PATCH' \
            // 'http://0.0.0.0:4000/KubeApi/apis%2Fapps%2Fv1%2Fnamespaces%2Fdefault%2Fdeployments%2Fhello-minikube' \
            // -H 'accept: application/json' \
            // -H 'Content-Type: application/json' \
            // -d '{"spec":{"template":{"metadata":{"annotations":{"date":"2122"}}}}}'
            //默认增加/根路径
            api = HttpUtility.UrlDecode(api);
            var s = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            return await Kubectl.Instance.PatchResourceJson($"/{api}", s);
        }
    }
}
