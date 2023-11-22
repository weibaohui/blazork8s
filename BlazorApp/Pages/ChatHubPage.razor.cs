using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp.Pages;

public partial class ChatHubPage : ComponentBase
{
    [Inject]
    protected NavigationManager MyUriHelper { get; set; } //to get current Uri

    public string _messageToSend  { get; set; } //to bind on textbox
    public string _messageRecived { get; set; } //to bind on a label or so...

    private HubConnection hubConnection;

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(MyUriHelper.ToAbsoluteUri("/chathub"))
            .Build();

        //receive event
        hubConnection.On<string>("ReceiveMessage", (message) =>
        {
            Console.WriteLine("page收到消息" + message);
            _messageRecived += message;
            StateHasChanged();
        });

        await hubConnection.StartAsync();
    }

    //send message
    //call Send method from button click
    public async Task Send()
    {
        if (hubConnection is not null)
        {
            await hubConnection.SendAsync("SendMessage", _messageToSend);
        }

        StateHasChanged();
    }
}
