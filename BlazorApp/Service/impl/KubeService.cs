#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using k8s;

namespace BlazorApp.Service.impl;

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


    public async Task<List<string>> ListNs()
    {
        var namespaces = await Client().CoreV1.ListNamespaceAsync();
        return namespaces.Items.Select(r => r.Metadata.Name).ToList();
    }

    public async Task<List<string>> ListPodByNs(string? ns = "kube-system")
    {
        var list = await Client().CoreV1.ListNamespacedPodAsync(ns);
        return list.Items.Select(r => r.Metadata.Name).ToList();
    }
}
