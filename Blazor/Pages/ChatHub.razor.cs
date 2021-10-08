using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Service;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Pages
{
    public partial class ChatHub:ComponentBase,IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IPodService PodService { get; set; }
        private HubConnection hubConnection;
        private List<string>  messages = new List<string>();
        private string        userInput;
        private string        messageInput;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("http://localhost:4000/chathub"))
                .AddNewtonsoftJsonProtocol()
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
                PodService.UpdateSharePods(type,pod);
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
