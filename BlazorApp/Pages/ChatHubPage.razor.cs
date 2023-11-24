using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages;

public partial class ChatHubPage : ComponentBase
{
    private ResourceCache<V1Pod> _itemList = ResourceCacheHelper<V1Pod>.Instance.Build();

    private async Task OnResourceChanged(ResourceCache<V1Pod> data)
    {
        _itemList = data;
        await InvokeAsync(StateHasChanged);
    }
}