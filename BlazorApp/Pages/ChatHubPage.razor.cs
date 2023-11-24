using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Utils;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages;

public partial class ChatHubPage : ComponentBase
{
    private ResourceCacheHelper<V1Pod> _itemList = ResourceCacheHelper<V1Pod>.Instance();

    private async Task OnResourceChanged(ResourceCacheHelper<V1Pod> data)
    {
        _itemList = data;
        await InvokeAsync(StateHasChanged);
    }
}