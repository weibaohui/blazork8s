using System;
using System.Net.Http;
using BlazorApp.Service;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Counter : ComponentBase
    {
        [Inject]
        private IKubeService KubeService { get; set; }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount += 1;

            Console.WriteLine( new HttpClient().GetStringAsync("http://www.baidu.com").Result);
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
// Use the config object to create a client.
            Console.WriteLine(config.CurrentContext);
            var client     = new Kubernetes(config);
            try
            {
                var namespaces = client.CoreV1.ListNode();
                Console.WriteLine(namespaces.Items.Count);
                foreach (var ns in namespaces.Items)
                {
                    Console.WriteLine(ns.Metadata.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
    }
}
