using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class HorizontalPodAutoscalerService : CommonAction<V1HorizontalPodAutoscaler>, IHorizontalPodAutoscalerService
{
    private readonly IKubeService                _baseService;

    public HorizontalPodAutoscalerService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().AutoscalingV2.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }

    public new async Task<object> V1Delete(string ns, string name)
    {
        return await _baseService.Client().AutoscalingV1.DeleteNamespacedHorizontalPodAutoscalerAsync(name, ns);
    }
}
