using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class IngressClassService : CommonAction<V1IngressClass>, IIngressClassService
{
    private readonly IBaseService                _baseService;

    public IngressClassService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteIngressClassAsync(name);
    }

    public async Task SetDefault(V1IngressClass item)
    {
        var list = await _baseService.Client().ListIngressClassAsync();
        foreach (var pc in list.Items.Where(x => x.Name() != item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, false);
        }
        foreach (var pc in list.Items.Where(x => x.Name() == item.Name()))
        {
            await ChangeGlobalDefaultTo(pc, true);
        }
    }

    public async Task ChangeGlobalDefaultTo(V1IngressClass item, bool status)
    {
            var patchStr = """
                    {
                        "metadata": {
                            "annotations": {
                                 "ingressclass.kubernetes.io/is-default-class": "${default}"
                             }
                        }
                    }
                    """
                    .Replace("${default}", status.ToString().ToLower())
                ;
         var x=   await _baseService.Client().PatchIngressClassAsync(
                new V1Patch(patchStr, V1Patch.PatchType.MergePatch)
                , item.Name(), item.Namespace());
    }
}
