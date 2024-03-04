using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity.Analyze;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PersistentVolumeClaimService(IKubeService kubeService,IEventService eventService)
    : CommonAction<V1PersistentVolumeClaim>, IPersistentVolumeClaimService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedPersistentVolumeClaimAsync(name, ns);
    }

    public async Task<List<Result>> Analyze()
    {
        var items   = List();
        var results = new List<Result>();
        foreach (var item in items.ToList())
        {
            var failures = new List<Failure>();

            if (item.Status.Phase=="Pending")
            {
                var evt =await eventService.GetInvolvingObjectLatestEvent(item.Namespace(), item.Name());
                if (evt == null){continue;}

                if (evt.Reason=="ProvisioningFailed")
                {
                    failures.Add(new Failure()
                    {
                        Text = evt.Message
                    });
                }
            }
            


            if (failures.Count <= 0) continue;
            results.Add(Result.NewResult(item,failures));
        }
        if (results.Count == 0)
        {
            ClusterInspectionResultContainer.Instance.GetPassResources().Add("PVC");
        }
        ClusterInspectionResultContainer.Instance.GetAllResourcesCount().Add("PVC", items.ToList().Count);

        return results;
    }

}
