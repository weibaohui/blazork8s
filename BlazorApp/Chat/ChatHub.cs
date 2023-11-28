#nullable enable
using System;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Chat;

public class ChatHub : Hub
{
    public Task PodLogHeartBeat(V1Pod pod, string containerName)
    {
        var message = $"{pod.Namespace()}/{pod.Name()}/{containerName}";
        Console.WriteLine(message);
        return Task.CompletedTask;
    }

    public Task SendWatchEvent(string message)
    {
        throw new NotImplementedException();
    }
}
