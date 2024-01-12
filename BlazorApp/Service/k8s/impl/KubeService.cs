#nullable enable
using System;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class KubeService : IKubeService
{
    private string _contextName { get; set; }


    private Kubernetes? _client { get; set; }


    public void ChangeContext(string ctxName)
    {
        //重新连接k8s
        _contextName = ctxName;
        _client?.Dispose();
        _client = null;
        //重连
        Client();
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
            : KubernetesClientConfiguration.BuildConfigFromConfigFile(currentContext: _contextName);
        _contextName = config.CurrentContext;
        _client      = new Kubernetes(config);
        Console.WriteLine($"KubeService initialized.{config.CurrentContext}");
        return _client;
    }

    public string CurrentContext()
    {
        return _contextName;
    }
}
