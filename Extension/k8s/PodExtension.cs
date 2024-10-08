using System;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s;

public static class PodExtension
{
    /// <summary>
    /// 计算每个节点包含多少个POD
    /// </summary>
    /// <param name="pods"></param>
    /// <returns></returns>
    public static IEnumerable<Tuple<string, int>> CountsOfPod(this IList<V1Pod> pods)
    {
        var tuples = pods
            .GroupBy(s => s.Spec.NodeName)
            .OrderBy(g => g.Key)
            .Select(g => Tuple.Create(g.Key, g.Count()));
        return tuples;
    }

    /// <summary>
    /// 计算POD重启次数
    /// </summary>
    /// <param name="pod"></param>
    /// <returns></returns>
    public static int? RestartCount(this V1Pod pod)
    {
        var sum = pod.Status?.ContainerStatuses?.Sum(x => x.RestartCount);
        return sum;
    }

    /// <summary>
    /// 计算当前节点包含多少个POD
    /// </summary>
    /// <param name="pods"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public static int CountsOfPodInNode(this IList<V1Pod> pods, string nodeName)
    {
        return pods
            .Count(w => w.Spec.NodeName == nodeName);
    }

    /// <summary>
    ///  返回指定名称的节点包含的POD
    /// </summary>
    /// <param name="pods"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public static IList<V1Pod> PodListInNode(this IList<V1Pod> pods, string nodeName)
    {
        return pods.Where(w => w.Spec.NodeName == nodeName).ToList();
    }

    /// <summary>
    /// 返回指定名称的节点包含的POD
    /// </summary>
    /// <param name="pods"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public static IList<V1Pod> FilterByNodeName(this IList<V1Pod> pods, string nodeName)
    {
        return pods.Where(w => w.Spec.NodeName == nodeName).ToList();
    }

    public static int CountPodsByOwner(this IList<V1Pod> pods, string uid)
    {
        return pods
            .Count(x => x.GetController() != null && x.GetController().Uid == uid);
    }

    public static string ReadySummary(this IList<V1Pod> pods)
    {
        var count = pods.Count;
        var readyCount = pods.Count(IsReady);
        return $"{readyCount}/{count}";
    }

    public static bool IsReady(this V1Pod pod)
    {
        if (pod.Status?.Conditions == null)
        {
            return false;
        }

        return pod.Status.Conditions.Any(condition => condition.Type == "Ready" && condition.Status == "True");
    }

    public static bool IsProcessing(this V1Pod pod)
    {
        return pod.Status?.Phase switch
        {
            "Pending" => true,
            "Running" => false,
            "Succeeded" => false,
            "Unknown" => true,
            "Failed" => false,
            _ => false
        };


        // if (pod.Status?.Conditions == null) return false;
        // return pod.Status.Conditions.Any(condition => condition.Status != "True");
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
        if (pod.Metadata?.DeletionTimestamp != null)
        {
            return "Terminating";
        }

        var phase = pod.Status.Phase;

        if (pod.Status.ContainerStatuses == null)
        {
            return phase;
        }

        var phaseList = pod.Status.ContainerStatuses
            .Select(s => s.State)
            .Select(s =>
            {
                if (s.Terminated != null)
                {
                    return s.Terminated.Reason;
                }

                if (s.Waiting != null)
                {
                    return s.Waiting.Reason;
                }

                return string.Empty;
            }).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

        if (phaseList.Count == 0)
        {
            return phase;
        }
        else
        {
            return phaseList.First();
        }
    }

    public static string Status(this V1PodStatus podStatus)
    {
        var phase = podStatus.Phase;

        if (podStatus.ContainerStatuses == null)
        {
            return phase;
        }

        var phaseList = podStatus.ContainerStatuses
            .Select(s => s.State)
            .Select(s =>
            {
                if (s.Terminated != null)
                {
                    return s.Terminated.Reason;
                }

                if (s.Waiting != null)
                {
                    return s.Waiting.Reason;
                }

                return string.Empty;
            }).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

        if (phaseList.Count == 0)
        {
            return phase;
        }
        else
        {
            return phaseList.First();
        }
    }

    /// <summary>
    /// 获得Pod中Ready容器数量
    /// </summary>
    /// <param name="pod"></param>
    /// <returns></returns>
    public static string ReadySummary(this V1Pod pod)
    {
        var count = pod.Spec.Containers.Count;
        var readyCount = pod.Status.ContainerStatuses?
            .Where(w => w.Ready).Count() ?? 0;
        return $"{readyCount}/{count}";
    }

    public static string? StatusX(this V1Pod pod)
    {
        if (pod == null)
        {
            return "null";
        }

        return pod?.Spec?.NodeName;
    }
}
