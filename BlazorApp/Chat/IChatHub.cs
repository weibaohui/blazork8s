using System.Threading.Tasks;

namespace BlazorApp.Chat;

public interface IChatHub
{
    public Task SendMessage(string message);
}
