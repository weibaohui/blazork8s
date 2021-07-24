using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extensions.k8s
{
    public static class PodExtension
    {
        /// <summary>
        /// 计算每个节点包含多少个POD
        /// </summary>
        /// <param name="pods"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<string, int>> CountsOfPod(this V1PodList pods)
        {
            var tuples = pods.Items
                .GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));
            return tuples;
        }
        /// <summary>
        /// 计算当前节点包含多少个POD
        /// </summary>
        /// <param name="pods"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static int CountsOfPodInNode(this V1PodList pods, string nodeName)
        {
            return pods.Items
                .Count(w => w.Spec.NodeName == nodeName);
        }
        /// <summary>
        /// 返回当前节点包含的POD
        /// </summary>
        /// <param name="pods"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IList<V1Pod> PodListInNode(this V1PodList pods, string nodeName)
        {
            return pods.Items.Where(w => w.Spec.NodeName == nodeName).ToList();
        }

    }
}
