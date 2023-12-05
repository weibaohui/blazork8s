using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IHorizontalPodAutoscalerService : ICommonAction<V1HorizontalPodAutoscaler>
{
}
