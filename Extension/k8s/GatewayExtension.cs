using System.Collections.Generic;
using System.Linq;
using Entity.Crd.Gateway;

namespace Extension.k8s;

public static class GatewayExtension
{
    public static bool IsReady(this IList<RouteParentStatus> routeStatus)
    {
        foreach (var condition in routeStatus.SelectMany(r => r.Conditions))
        {
            if (condition.Type == "ResolvedRefs" && condition.Status != "True")
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsReady(this GatewayStatus status)
    {
        if (status.Conditions is { Count: > 0 })
        {
            foreach (var condition in status.Conditions)
            {
                if (condition.Type == "Programmed" && condition.Status != "True")
                {
                    return false;
                }
            }
        }


        return true;
    }
}
