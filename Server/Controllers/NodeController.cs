using System.Collections.Generic;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly ILogger<NodeController> _logger;

        public NodeController(ILogger<NodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Node> GetNodes()
        {
            return NodeService.Instance.GetList();
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<V1Node> GetOriginNodes()
        {
            return NodeService.Instance.GetOriginNodesList();
        }
    }
}
