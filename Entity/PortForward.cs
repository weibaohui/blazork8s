using System;
using k8s;
using k8s.Models;

namespace Entity;

/// <summary>
/// Listen on port 8888 on all addresses, forwarding to 5000 in the pod
///  kubectl port-forward --address 0.0.0.0 pod/mypod 8888:5000
/// </summary>
public class PortForward : IKubernetesObject<V1ObjectMeta>

{
    public string       ApiVersion { get; set; }
    public string       Kind       { get; set; }
    public V1ObjectMeta Metadata   { get; set; }


    public int    LocalPort { get; set; }
    public string KubePort  { get; set; }
    public string KubeName  { get; set; }

    public string           Status          { get; set; }
    public DateTime? StatusTimestamp { get; set; }

    /// <summary>
    /// deployment svc pod
    /// </summary>
    public PortForwardType Type { get; set; }


    public static int RandomPort()
    {
        return new Random().Next(30000, 65500);
    }
}

public enum PortForwardType
{
    Deployment,
    Svc,
    Pod,
}
