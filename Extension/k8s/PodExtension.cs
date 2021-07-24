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

        public static bool isReady(this V1Pod pod)
        {
            foreach (var condition in pod.Status.Conditions)
            {
                if (condition.Type == "Ready" && condition.Status == "True")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Pending
        /// Running
        /// Succeeded
        /// Failed
        /// Unknown
        /// </summary>
        /// <param name="pod"></param>
        /// <returns></returns>
        public static string Status(this V1Pod pod)
        {
            var phase = pod.Status.Phase;

            if (pod.Status.ContainerStatuses==null)
            {
                return phase;
            }
            var phaseList = pod.Status.ContainerStatuses
                .Select(s => s.State)
                .Select(s =>
                {
                    if (s.Terminated != null)
                    {
                        return "Terminating";
                    }
                    if (s.Waiting != null)
                    {
                        return s.Waiting.Reason;
                    }
                    return string.Empty;
                }).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

            if (!phaseList.Any())
            {
                return phase;
            }
            else
            {
                return phaseList.First();
            }
        }
    }
}
