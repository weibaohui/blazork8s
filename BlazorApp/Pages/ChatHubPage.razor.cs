using System;
using System.Threading.Tasks;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages;

public partial class ChatHubPage : ComponentBase
{
    [Inject]
    protected NavigationManager MyUriHelper { get; set; } //to get current Uri

    private string MessageToSend   { get; set; } //to bind on textbox
    private string MessageReceived { get; set; } //to bind on a label or so...

    private HubConnection _hubConnection;

    protected override async Task OnInitializedAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(MyUriHelper.ToAbsoluteUri("/chathub"))
            .Build();

        //receive event
        _hubConnection.On<ResourceWatchEntity<V1Pod>>("ReceiveMessage", async (data) =>
        {
            Console.WriteLine("page收到消息" + data.Message);
            Console.WriteLine("page收到消息" + data.Item.Metadata.Name);
            MessageReceived += data.Item.Metadata.Name;
            await InvokeAsync(StateHasChanged);
        });

        await _hubConnection.StartAsync();
    }

    //send message
    //call Send method from button click
    private async Task Send()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.SendAsync("SendMessage", MessageToSend);
        }

        StateHasChanged();
    }
}
