using k8s;
using k8s.Models;

namespace Extension.k8s;

public static class MetaExtension<T> where T : IKubernetesObject<V1ObjectMeta>
{
    // public static int? RestartCount(this IList<T> items)
    // {
    //     var sum = pod.Status?.ContainerStatuses?.Sum(x => x.RestartCount);
    //     return sum;
    // }
}