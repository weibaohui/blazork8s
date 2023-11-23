using System;
using System.Threading.Tasks;
using AntDesign;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages;

public partial class ItemList<T> : ComponentBase where T : IKubernetesObject<V1ObjectMeta>
{
    [Parameter]
    public string Type { get; set; }

    [Inject]
    protected NavigationManager MyUriHelper { get; set; }

    [Parameter]
    public EventCallback<ResourceWatchEntity<T>> OnChanged { get; set; }

    private HubConnection _hubConnection;

    private async Task UpdateMessage(ResourceWatchEntity<T> data)
    {
        Console.WriteLine("page收到消息" + data.Message);
        Console.WriteLine("page收到消息" + data.Item.Metadata.Name);
        await InvokeAsync(StateHasChanged);
        await OnChanged.InvokeAsync(data);
    }

    protected override async Task OnInitializedAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(MyUriHelper.ToAbsoluteUri("/chathub"))
            .Build();

        //receive event
        _hubConnection.On<ResourceWatchEntity<T>>("ReceiveMessage", UpdateMessage);
        await _hubConnection.StartAsync();
    }
}
