#nullable enable
using System;
using System.Net.Http;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class KubeService : IKubeService
{
    private readonly HttpClient _http;

    private string      _name    { get; set; }
    private string      _version { get; set; }
    private Kubernetes? _client  { get; set; }

    public KubeService(HttpClient http)
    {
        _http    = http;
        _name    = "AutoName";
        _version = "AutoVersion";
    }


    public string Name()
    {
        return _name;
    }

    public string Version()
    {
        return _version;
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
            : KubernetesClientConfiguration.BuildConfigFromConfigFile();

        _client = new Kubernetes(config);
        Console.WriteLine("KubeService initialized.");
        return _client;
    }

}
