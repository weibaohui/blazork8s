using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace Extension.k8s
{
    public static class Extension
    {
        public static bool IsReady(this IList<V1NodeCondition> conditions)
        {
            foreach (var condition in conditions)
            {
                if (condition.Type == "Ready" && condition.Status == "True")
                {
                    return true;
                }
            }

            return false;
        }

        public static string? NotReadyReason(this IList<V1NodeCondition> conditions)
        {
            return conditions.FirstOrDefault(w => w.Type == "Ready")?.Reason?.ToString();
        }

        /// <summary>
        /// 获取节点角色
        /// </summary>
        /// <param name="meta"></param>
        /// <returns></returns>
        public static IList<string> NodeRoles(this V1ObjectMeta meta)
        {
            return meta.Labels
                .Where(w => w.Key.StartsWith("node-role.kubernetes.io/"))
                .Select(d => d.Key.Replace("node-role.kubernetes.io/", ""))
                .ToList();
        }
    }
}
