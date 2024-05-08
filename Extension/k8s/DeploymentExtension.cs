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

    public static bool IsProcessing(this V1Deployment deploy)
    {
        if (deploy.Status?.Conditions == null) return false;

        if (deploy.Status.Conditions.Any(x => x.Status == "True" && x.Type == "Progressing"))
            if (deploy.Status.Replicas != deploy.Status.UpdatedReplicas)
                return true;

        ;
        return false;
    }
}
