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

        private readonly List<Node> _nodeList = new();

        public void AddNode(V1Node node)
        {
            var exist = _nodeList.Any(r => r.Name == node.Metadata.Name);
            if (!exist)
            {
                //不存在
                var item = new Node {Name = node.Name(), OriginNode = node};

                foreach (var kv in node.Status.Capacity)
                {
                    if (kv.Key=="cpu"||kv.Key=="memory"||kv.Key=="pods")
                    {
                        item.Capacity.Add(kv.Key,kv.Value.ToString());
                    }
                }
                foreach (var kv in node.Status.Allocatable)
                {
                    if (kv.Key=="cpu"||kv.Key=="memory"||kv.Key=="pods")
                    {
                        item.Allocatable.Add(kv.Key,kv.Value.ToString());
                    }
                }

                _nodeList.Add(item);
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
            _nodeList.First(r => r.Name == node.Metadata.Name).OriginNode = node;
        }

        public void RemoveNode(V1Node node)
        {
            var i = _nodeList.FindIndex(r => r.Name == node.Metadata.Name);
            _nodeList.RemoveAt(i);
        }

        public IEnumerable<Node> GetList()
        {
            // var result = from node in nodeList
            //     select new Node {Name = node.Metadata.Name};
            // return result;
            return _nodeList;
        }
        public IEnumerable<V1Node> GetOriginNodesList()
        {
            var result = from node in _nodeList
                select node.OriginNode;
            return result.AsEnumerable();
        }
    }
}
