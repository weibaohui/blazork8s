using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class StorageClassService(IKubeService kubeService) : CommonAction<V1StorageClass>, IStorageClassService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteStorageClassAsync(name);
    }

    public async Task SetDefault(V1StorageClass item)
    {
        var list = await kubeService.Client().ListStorageClassAsync();
        foreach (var pc in list.Items.Where(x => x.Name() != item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, false);
        }
        foreach (var pc in list.Items.Where(x => x.Name() == item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, true);
        }
    }

    public async Task ChangeGlobalDefaultTo(V1StorageClass item, bool status)
    {
        var patchStr = """
                {
                    "metadata": {
                        "annotations": {
                             "storageclass.kubernetes.io/is-default-class": "${default}"
                         }
                    }
                }
                """
                .Replace("${default}", status.ToString().ToLower())
            ;
        var x =   await kubeService.Client().PatchStorageClassAsync(
            new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
            , item.Name(), item.Namespace());
    }
}
