using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ingress;

public partial class IngressIndex : TableBase<V1Ingress>
{
    [Inject] private IIngressService IngressService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Ingress> data)
    {
        ItemList = data;
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnItemNameClick(V1Ingress item)
    {
        await PageDrawerHelper<V1Ingress>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<IngressDetailView, V1Ingress, bool>(item);
    }

    private static string GetRulePathDisplay(V1IngressRule rule, V1HTTPIngressPath path, IList<V1IngressTLS> specTls)
    {
        var pathDisplay = "";
        if (specTls is { Count: > 0 })
            pathDisplay += "https://";
        else
            pathDisplay += "http://";

        if (rule?.Host != null && !string.IsNullOrWhiteSpace(rule?.Host))
            pathDisplay += $"{rule.Host}";
        else
            pathDisplay += "*";

        if (path?.Path != null) pathDisplay += $"{path.Path}";


        return pathDisplay;
    }

    private static string GetRulePathBackend(V1HTTPIngressPath path)
    {
        var pathDisplay = "";

        if (path?.Backend?.Service?.Name != null && !string.IsNullOrWhiteSpace(path.Backend?.Service?.Name))
            pathDisplay += $"{path.Backend?.Service?.Name}";

        if (path?.Backend?.Service?.Port?.Number != null) pathDisplay += $":{path.Backend?.Service?.Port?.Number}";

        return pathDisplay;
    }

    private static V1ObjectReference GetRulePathBackend(V1HTTPIngressPath path, string ns)
    {
        var reference = new V1ObjectReference
        {
            ApiVersion = "v1",
            Kind = "Service",
            Name = path?.Backend?.Service?.Name,
            NamespaceProperty = ns
        };
        return reference;
    }
}
