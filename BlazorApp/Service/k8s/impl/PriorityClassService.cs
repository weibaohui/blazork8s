using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PriorityClassService : CommonAction<V1PriorityClass>, IPriorityClassService
{
    private readonly IKubeService _kubeService;

    public PriorityClassService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }

    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeletePriorityClassAsync(name);
    }

    public async Task SetDefault(V1PriorityClass item)
    {
        var list = await _kubeService.Client().ListPriorityClassAsync();
        foreach (var pc in list.Items.Where(x => x.Name() != item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, false);
        }
        foreach (var pc in list.Items.Where(x => x.Name() == item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, true);
        }


    }

    public async Task ChangeGlobalDefaultTo(V1PriorityClass item, bool status)
    {
            var patchStr = """
                    {
                        
                            "globalDefault":  ${default}
                         
                    }
                    """
                    .Replace("${default}", status.ToString().ToLower())
                ;
            await _kubeService.Client().PatchPriorityClassAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
        }
}
