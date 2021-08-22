using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class K8sController : ControllerBase
    {
        private readonly ILogger<K8sController> _logger;

        public K8sController(ILogger<K8sController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void PodList()
        {
            var client = new HttpsClient().Builder().Build();

            var httpResponseMessage =
                client.GetAsync("https://kubernetes.docker.internal:6443/api/v1/namespaces/default/pods").GetAwaiter()
                    .GetResult();
            var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _logger.LogError(result);
        }
    }
}
