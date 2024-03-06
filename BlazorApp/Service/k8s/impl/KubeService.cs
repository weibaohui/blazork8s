#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Utils.Prometheus;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
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
        // ListWatchHelper.Instance.Dispose();
        //
        // _client?.Dispose();
        // _client = null;
        // var map = ResourceCacheContainer.Instance.GetMap();
        // map.Clear();

        //重连
        // Client();
        //重新 list watch
        // var watchService = ListWatchHelper.Instance.Create(this, _ctx);
        // watchService.StartAsync();
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

    private async Task<string> GetStringAsync(string requestUri)
    {
        var baseUrl = Client().BaseUri.ToString();
        requestUri = requestUri.StartsWith("/") ? requestUri.Remove(0, 1).Trim() : requestUri;
        return await Client().HttpClient.GetStringAsync($"{baseUrl}{requestUri}");
    }

    private async Task<Stream> GetStreamAsync(string requestUri)
    {
        var baseUrl = Client().BaseUri.ToString();
        requestUri = requestUri.StartsWith("/") ? requestUri.Remove(0, 1).Trim() : requestUri;
        return await Client().HttpClient.GetStreamAsync($"{baseUrl}{requestUri}");
    }

    public async Task<ServerInfo> GetServerVersion()
    {
        var json = await GetStringAsync("/version");
        return KubernetesJson.Deserialize<ServerInfo>(json);
    }

    public async Task<string> GetReadyz()
    {
        return await GetStringAsync("/readyz?verbose");
    }

    public async Task<string> GetLivez()
    {
        return await GetStringAsync("/livez?verbose");
    }

    public async Task<List<IMetric>> GetMetrics()
    {
        var             metricString = await GetStringAsync("/metrics");
        var             byteArray    = Encoding.UTF8.GetBytes(metricString);
        await using var ms           = new MemoryStream(byteArray);
        var             metric       = await PrometheusMetricsParser.ParseAsync(ms);
        return metric;
    }

    /// <summary>
    /// 获取节点kubelet配置
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private async Task<string> GetNodesConfig(string nodeName)
    {
        return await GetStringAsync($"/api/v1/nodes/{nodeName}/proxy/configz");
    }
}
