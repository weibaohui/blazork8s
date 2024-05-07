using System.Linq;
using k8s.Models;

namespace Extension.k8s;

public static class DeploymentExtension
{
    public static bool IsReady(this V1Deployment deploy)
    {
        if (deploy.Status?.Conditions == null) return false;

        return deploy.Status.Conditions.All(x => x.Status == "True");
    }
}
