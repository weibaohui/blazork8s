#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Utils.Prometheus;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class KubeService : IKubeService
{
    private string? ContextName { get; set; }
    private Kubernetes? _client { get; set; }

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
        _client = new Kubernetes(config);
        Console.WriteLine($"KubeService initialized.{config.CurrentContext}");
        return _client;
    }

    public string CurrentContext()
    {
        return ContextName ?? "";
    }

    public async Task<string> GetStringAsync(string requestUri)
    {
        var baseUrl = Client().BaseUri.ToString();
        requestUri = requestUri.StartsWith('/') ? requestUri.Remove(0, 1).Trim() : requestUri;
        if (KubernetesClientConfiguration.IsInCluster())
        {
            return await GetStringInClusterAsync(requestUri);
        }

        return await Client().HttpClient.GetStringAsync($"{baseUrl}{requestUri}");
    }


    private async Task<string> GetStringInClusterAsync(string requestUri)
    {
        var serviceAccountTokenPath = "/var/run/secrets/kubernetes.io/serviceaccount/token";
        var caCertPath = "/var/run/secrets/kubernetes.io/serviceaccount/ca.crt";

        var token = await File.ReadAllTextAsync(serviceAccountTokenPath);
        var handler = new HttpClientHandler();

        // Set the CA certificate
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        handler.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate2(caCertPath));

        var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var baseUrl = Client().BaseUri.ToString();
        requestUri = requestUri.StartsWith('/') ? requestUri.Remove(0, 1).Trim() : requestUri;


        var response = await client.GetAsync($"{baseUrl}{requestUri}");

        var content = "";
        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(content);
        }
        // else
        // {
        //     Console.WriteLine($"Error: {response.StatusCode}");
        // }

        return content;
    }


    private async Task<Stream> GetStreamAsync(string requestUri)
    {
        var baseUrl = Client().BaseUri.ToString();
        requestUri = requestUri.StartsWith('/') ? requestUri.Remove(0, 1).Trim() : requestUri;
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
        var metricString = await GetStringAsync("/metrics");
        return await ConvertStringToMetrics(metricString);
    }

    public async Task<List<IMetric>> ConvertStringToMetrics(string metricString)
    {
        var byteArray = Encoding.UTF8.GetBytes(metricString);
        await using var ms = new MemoryStream(byteArray);
        var metric = await PrometheusMetricsParser.ParseAsync(ms);
        return metric;
    }

    /// <summary>
    /// Service Level Indicators，服务水平指标
    /// kubernetes_healthcheck 相关指标
    /// SLIs 是衡量服务水平目标（SLOs）和服务水平协议（SLAs）中定义的性能标准的具体指标。
    /// SLIs 关注于服务的特定方面，如可用性、延迟、吞吐量等，它们为评估服务的性能提供了定量的基础。
    /// 在 Kubernetes API 服务器的上下文中，SLIs 可以包括响应时间、错误率、API 调用的成功率等指标。
    /// 通过监控这些 SLIs，集群运维人员可以评估 API 服务器是否满足既定的服务水平目标（SLOs），从而确保用户获得一致且可靠的服务体验。
    /// </summary>
    /// <returns></returns>
    public async Task<List<IMetric>> GetMetricsSlis()
    {
        var metricString = await GetStringAsync("/metrics/slis");
        var byteArray = Encoding.UTF8.GetBytes(metricString);
        await using var ms = new MemoryStream(byteArray);
        var metric = await PrometheusMetricsParser.ParseAsync(ms);
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
