using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class ContainerExtension
    {
        /// <summary>
        /// 容器是否Ready
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static bool IsReady(this IList<V1ContainerStatus> ss, string containerName)
        {
            var cc = ss.First(w => w.Name == containerName);
            return cc.Ready;
        }

        public static bool IsReady(this V1ContainerStatus cc)
        {
            return cc.Ready;
        }

        /// <summary>
        /// 获取容器运行状态
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static string ReadySummary(this IList<V1ContainerStatus> ss, string containerName)
        {
            var cc = ss.First(w => w.Name == containerName);
            if (cc.State.Waiting != null)
            {
                return cc.State.Waiting.Reason;
            }

            if (cc.State.Terminated != null)
            {
                return cc.State.Terminated.Reason;
            }

            return cc.Ready ? "Ready" : string.Empty;
        }

        public static string ReadySummary(this V1ContainerStatus cc)
        {
            if (cc.Ready)
            {
                return $"{cc.Name}(Ready,Running),{cc.State.Running.StartedAt?.AgeFromUtc()} ago";
            }

            if (cc.State.Waiting != null)
            {
                return $"{cc.Name}(Waiting,{cc.State.Waiting.Reason}),{cc.State.Waiting.Message}";
            }

            if (cc.State.Terminated != null)
            {
                return $"{cc.Name}(Terminated,{cc.State.Terminated.Reason}),{cc.State.Terminated.Message}";
            }

            return string.Empty;
        }

        public static V1ContainerStatus GetByName(this IList<V1ContainerStatus> ss, string containerName)
        {
            return ss.First(w => w.Name == containerName);
        }

        public static (string ReadySummary, string Msg) ReadySummaryWithMsg(this IList<V1ContainerStatus>? ss,
            string ContainerName)
        {
            if (ss == null)
            {
                return ("", "");
            }

            var cc = ss.First(w => w.Name == ContainerName);
            if (cc.State.Waiting != null)
            {
                return (cc.State.Waiting.Reason, cc.State.Waiting.Message);
            }

            if (cc.State.Terminated != null)
            {
                return (cc.State.Terminated.Reason, cc.State.Terminated.Message);
            }

            return cc.Ready ? ("Ready", "") : ("", "");
        }
    }
}
