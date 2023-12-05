using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface ICronJobService : ICommonAction<V1CronJob>
{
}
