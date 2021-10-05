using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Blazor.Pages
{
    public partial class ChatHub:ComponentBase,IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private HubConnection hubConnection;
        private List<string>  messages = new List<string>();
        private string        userInput;
        private string        messageInput;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("http://localhost:4000/chathub"))
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                messages.Add(encodedMsg);
                StateHasChanged();
            });

            hubConnection.On<WatchEventType, V1Pod>("PodWatch",(type, pod)=>{
                var encodedMsg = $"PodWatch {type}:  {pod.Metadata.Name}";
                messages.Add(encodedMsg);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        async Task Send() =>
            await hubConnection.SendAsync("SendMessage", userInput, messageInput);

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;


        public void Dispose()
        {
            _ = hubConnection?.DisposeAsync();
        }
    }
}
