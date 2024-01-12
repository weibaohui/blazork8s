using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterConfig;

public partial class ClusterConfig : ComponentBase
{
    [Inject]
    public IKubeService KubeService { get; set; }

    private Task OnChangeBtnClick(string ctxName)
    {
        KubeService.ChangeContext(ctxName);
        return Task.CompletedTask;
    }
}
