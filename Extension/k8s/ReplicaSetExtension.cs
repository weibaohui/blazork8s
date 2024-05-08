using k8s.Models;

namespace Extension.k8s;

public static class ReplicaSetExtension
{
    public static bool IsReady(this V1ReplicaSet rs)
    {
        if (rs.Spec?.Replicas == 0) return false;

        return rs.Status?.Replicas == rs.Spec?.Replicas;
    }

    public static bool IsProcessing(this V1ReplicaSet rs)
    {
        if (rs.Spec?.Replicas == 0) return false;

        return rs.Status?.Replicas != rs.Spec?.Replicas;
    }
}
