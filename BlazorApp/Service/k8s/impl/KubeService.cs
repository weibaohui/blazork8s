#nullable enable
using System;
using BlazorApp.Utils;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class KubeService : IKubeService
{
    private string?     ContextName { get; set; }
    private Kubernetes? _client     { get; set; }

    public void ChangeContext(string ctxName)
    {
        //重新连接k8s
        ContextName = ctxName;
        //停止list watch

        _client?.Dispose();
        _client = null;
        var map = ResourceCacheContainer.Instance.GetMap();
        map.Clear();

        //重连
        Client();
        //重新 list watch
    }

    public Kubernetes Client()
    {
        if (_client != null)
        {
            return _client;
        }


        KubernetesClientConfiguration config;
        // Load from in-cluster configuration:
        config = KubernetesClientConfiguration.IsInCluster()
            ? KubernetesClientConfiguration.InClusterConfig()
            : KubernetesClientConfiguration.BuildConfigFromConfigFile(currentContext: ContextName);
        ContextName = config.CurrentContext;
        _client     = new Kubernetes(config);
        Console.WriteLine($"KubeService initialized.{config.CurrentContext}");
        return _client;
    }

    public string CurrentContext()
    {
        return ContextName ?? "";
    }
}
