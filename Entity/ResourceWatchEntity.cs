using System;
using k8s;
using k8s.Models;

namespace Entity;

public class ResourceWatchEntity<T> where T : IKubernetesObject<V1ObjectMeta>
{
    public string         Message { get; set; }
    public WatchEventType Type    { get; set; }
    public T              Item    { get; set; }
}
