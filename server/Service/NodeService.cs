using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using server.Model;
using server.Utils;

namespace server.Service
{
    public class NodeService
    {
        private readonly        ILogger<NodeService> _logger =ServiceHelper.Services.GetService<ILogger<NodeService>>();
        private static readonly Lazy<NodeService>    Lazy    = new Lazy<NodeService>(() => new NodeService());
        public static           NodeService          Instance => Lazy.Value;

        private List<Node> nodeList = new();

        public void AddNode(V1Node node)
        {
            var exist = nodeList.Any(r => r.Name == node.Metadata.Name);
            if (!exist)
            {
                //不存在
                var item = new Node {Name = node.Name(), OriginNode = node};
                nodeList.Add(item);
                foreach (var kv in node.Status.Capacity)
                {
                    _logger.LogInformation(kv.ToString());
                }

            }
            else
            {
                _logger.LogInformation("新增转更新");
                ModifyNode(node);
            }
        }

        public void ModifyNode(V1Node node)
        {
            _logger.LogInformation("更新1");
            nodeList.First(r => r.Name == node.Metadata.Name).OriginNode = node;
        }

        public void RemoveNode(V1Node node)
        {
            var i = nodeList.FindIndex(r => r.Name == node.Metadata.Name);
            nodeList.RemoveAt(i);
        }

        public IEnumerable<Node> GetList()
        {
            // var result = from node in nodeList
            //     select new Node {Name = node.Metadata.Name};
            // return result;
            return nodeList;
        }
        public IEnumerable<V1Node> GetOriginNodesList()
        {
            var result = from node in nodeList
                select node.OriginNode;
            return result.AsEnumerable();
        }
    }
}
