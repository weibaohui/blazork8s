using System;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Chat;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        Console.WriteLine("收到" + message);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public Task SendWatchEvent(string message)
    {
        throw new NotImplementedException();
    }
}
