using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IStatefulSetService : ICommonAction<V1StatefulSet>
{
}
