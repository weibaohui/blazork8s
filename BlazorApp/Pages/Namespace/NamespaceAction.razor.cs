using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Namespace;

public partial class NamespaceAction : PageBase
{
    [Parameter] public V1Namespace Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;


    private async Task OnResourceClick(V1Namespace item)
    {
        var options = PageDrawerService.DefaultOptions($"{L["ResourceQuota"]}:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<NamespaceResourceQuota, V1Namespace, bool>(options, item);
    }

    private async Task OnLimitRangeClick(V1Namespace item)
    {
        var options = PageDrawerService.DefaultOptions($"{L["LimitRange"]}:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<NamespaceLimitRange, V1Namespace, bool>(options, item);
    }
}
