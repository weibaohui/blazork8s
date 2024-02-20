using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class HorizontalPodAutoscalerService : CommonAction<V1HorizontalPodAutoscaler>, IHorizontalPodAutoscalerService
{
    private readonly IKubeService                _kubeService;

    public HorizontalPodAutoscalerService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().AutoscalingV2.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }

    public  async Task<object> V1Delete(string ns, string name)
    {
        return await _kubeService.Client().AutoscalingV1.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }
}
