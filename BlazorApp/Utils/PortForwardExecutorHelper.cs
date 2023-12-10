#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PortForwardExecutorHelper
{
    private static readonly ILogger<PortForwardExecutorHelper> Logger =
        LoggingHelper<PortForwardExecutorHelper>.Logger();

    private static Dictionary<string, PodLogExecutor> map = new();

    public static PortForwardExecutorHelper Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly PortForwardExecutorHelper Instance = new PortForwardExecutorHelper();
    }


    public async Task ForwardDeployment(string ns, string deployName, string deployPort, int localPort)
    {
        var pf = new PortForward
        {
            Type      = PortForwardType.Deployment,
            LocalPort = localPort,
            KubePort  = deployPort,
            Metadata = new V1ObjectMeta
            {
                Name              = deployName,
                NamespaceProperty = ns,
                CreationTimestamp = DateTime.Now,
            }
        };

        PortForwardExecutor pfe = new PortForwardExecutor(pf);
        await pfe.Start();
    }
    
    public async Task ForwardPod(string ns, string podName, string deployPort, int localPort)
    {
        var pf = new PortForward
        {
            Type      = PortForwardType.Pod,
            LocalPort = localPort,
            KubePort  = deployPort,
            Metadata = new V1ObjectMeta
            {
                Name              = podName,
                NamespaceProperty = ns,
                CreationTimestamp = DateTime.Now,
            }
        };

        PortForwardExecutor pfe = new PortForwardExecutor(pf);
        await pfe.Start();
    }
    public async Task ForwardSvc(string ns, string svcName, string deployPort, int localPort)
    {
        var pf = new PortForward
        {
            Type      = PortForwardType.Svc,
            LocalPort = localPort,
            KubePort  = deployPort,
            Metadata = new V1ObjectMeta
            {
                Name              = svcName,
                NamespaceProperty = ns,
                CreationTimestamp = DateTime.Now,
            }
        };

        PortForwardExecutor pfe = new PortForwardExecutor(pf);
        await pfe.Start();
    }
}