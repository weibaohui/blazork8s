using k8s.Models;

namespace Extension.k8s;

public static class ReplicaSetExtension
{
    public static bool IsReady(this V1ReplicaSet rs)
    {
        if (rs.Status?.Replicas == 0) return false;

        return rs.Status?.Replicas == rs.Status?.ReadyReplicas;
    }
}
