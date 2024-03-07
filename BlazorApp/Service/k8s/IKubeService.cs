using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
using k8s;

namespace BlazorApp.Service.k8s;

public interface IKubeService
{
    public Kubernetes Client();
    public string     CurrentContext();

    public Task<ServerInfo>    GetServerVersion();
    public Task<string>        GetReadyz();
    public Task<string>        GetLivez();
    public Task<List<IMetric>> GetMetrics();
    public Task<List<IMetric>> GetMetricsSlis();
    public Task<string>        GetStringAsync(string         requestUri);
    public Task<List<IMetric>> ConvertStringToMetrics(string metricString);
}
