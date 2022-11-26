using System;
using System.Collections.Generic;
using System.Linq;
using k8s;

namespace BlazorApp.Service;

public class KubeService : IKubeService
{
    private string     name    { get; set; }
    private string     version { get; set; }
    private Kubernetes _client { get; set; }

    public KubeService()
    {
        this.name    = "AutoName";
        this.version = "AutoVersion";
        // var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
        // _client = new Kubernetes(config);
        Console.WriteLine(_client == null);
        Console.WriteLine("KubeService initialized.");
    }


    public string Name()
    {
        return name;
    }

    public string Version()
    {
        return version;
    }


    private Kubernetes Client()
    {
        return _client;
    }

    public List<string> ListNs()
    {
// Load from the default kubeconfig on the machine.
        var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
// Use the config object to create a client.
        Console.WriteLine(config.CurrentContext);
        var client     = new Kubernetes(config);
        var namespaces = client.CoreV1.ListNamespace();
        Console.WriteLine(namespaces.Items.Count);
        foreach (var ns in namespaces.Items)
        {
            Console.WriteLine(ns.Metadata.Name);
            var list = client.CoreV1.ListNamespacedPod(ns.Metadata.Name);
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
            }
        }

        // return namespaces.Items.Select(r => r.Metadata.Name).ToList();
        return new List<string>() { "xx", "Xxx" };
    }

    public List<string> ListPodByNs(string? ns = "default")
    {
        var list = Client().CoreV1.ListNamespacedPod(ns);
        return list.Items.Select(r => r.Metadata.Name).ToList();
    }
}
