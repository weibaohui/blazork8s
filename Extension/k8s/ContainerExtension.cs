using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class ContainerExtension
    {
         
       /// <summary>
       /// 获取容器运行状态
       /// </summary>
       /// <param name="ss"></param>
       /// <param name="ContainerName"></param>
       /// <returns></returns>
        public static string ReadySummary(this IList<V1ContainerStatus> ss,string ContainerName)
        {
            var cc = ss.First(w => w.Name == ContainerName);
            if (cc.State.Waiting != null)
            {
                return cc.State.Waiting.Reason;
            }
            if (cc.State.Terminated != null)
            {
                return cc.State.Terminated.Reason;
            }
            if (cc.Ready)
            {
                return "Ready";
            }
            return string.Empty;
        }
        public static V1ContainerStatus GetByName(this IList<V1ContainerStatus> ss, string ContainerName)
        {
            return ss.First(w => w.Name == ContainerName);
        }
        public static (string ReadySummary,string Msg) ReadySummaryWithMsg(this IList<V1ContainerStatus> ss, string ContainerName)
        {
            var cc = ss.First(w => w.Name == ContainerName);
            if (cc.State.Waiting != null)
            {
                return (cc.State.Waiting.Reason,cc.State.Waiting.Message);
            }
            if (cc.State.Terminated != null)
            {
                return (cc.State.Terminated.Reason, cc.State.Terminated.Message);
            }
            if (cc.Ready)
            {
                return ("Ready", "");
            }
            return ("", "");
        }
    }
}
