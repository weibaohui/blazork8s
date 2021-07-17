using System.Collections.Generic;
using k8s.Models;

namespace Extension.k8s
{
    public static class ReadyExtension
    {
        public static bool IsReady(this IList<V1NodeCondition> conditions)
        {
            foreach (var condition in conditions)
            {
                if (condition.Type == "Ready" && condition.Status=="True" )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
