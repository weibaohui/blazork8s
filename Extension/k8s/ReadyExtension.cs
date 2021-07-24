using System;
using System.Collections.Generic;
using System.Linq;
using k8s.Models;
using Microsoft.VisualBasic;

namespace Extensions.k8s
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

        public static IList<string> NodeRoles(this V1ObjectMeta meta)
        {
            // "items[0].metadata.labels["node-role.kubernetes.io/master"]"
            // items[0].metadata.labels["node-role.kubernetes.io/control-plane"]
            IList<string> roles=new List<string>();
            if (meta.Labels.ContainsKey("node-role.kubernetes.io/master"))
            {
                roles.Add("Master");
            }
            if (meta.Labels.ContainsKey("node-role.kubernetes.io/control-plane"))
            {
                roles.Add("ControlPlane");
            }

            return roles;
        }
    }
}
