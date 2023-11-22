using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Chat;

public class ChatHub : Hub, IChatHub
{
    public async Task SendMessage(string message)
    {
        Console.WriteLine("收到" + message);
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
