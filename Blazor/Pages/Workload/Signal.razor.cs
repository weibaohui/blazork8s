using System;
using System.Threading.Tasks;
using Blazor.Service;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Pages.Workload
{
    public partial class Signal : ComponentBase, IDisposable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        private HubConnection hubConnection;


        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:4000/chathub")
                .AddNewtonsoftJsonProtocol()
                .WithAutomaticReconnect()
                .Build();
            hubConnection.On<WatchEventType, V1Pod>("PodWatch", (type, pod) =>
            {
                var encodedMsg = $"PodWatch {type}:  {pod.Metadata.Name}";
                Console.WriteLine(encodedMsg);
                PodService.UpdateSharePods(type, pod);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        public void Dispose()
        {
            _ = hubConnection?.DisposeAsync();
        }
    }
}
