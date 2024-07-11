using System.Collections.Generic;
using k8s.Models;

namespace Extension.k8s;

public static class StatefulSetExtension
{
    public static bool IsReady(this IList<V1StatefulSetCondition> conditions)
    {
        foreach (var condition in conditions)
        {
            if (condition is { Type: "Ready", Status: "True" })
            {
                return true;
            }
        }

        return false;
    }
}
