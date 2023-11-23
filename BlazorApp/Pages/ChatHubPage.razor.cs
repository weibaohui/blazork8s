using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages;

public partial class ChatHubPage : ComponentBase
{
    private readonly List<V1Pod> Pods = new();
    private          string      MessageReceived { get; set; } //to bind on a label or so...

    private async Task OnChanged(ResourceWatchEntity<V1Pod> data)
    {
        Pods.Add(data.Item);
        MessageReceived += data.Item.Name() + data.Item.Metadata.CreationTimestamp;
        await InvokeAsync(StateHasChanged);
    }
}
