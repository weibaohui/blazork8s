using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Service;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {

        public IHubContext<ChatHub, IChatClient> _strongChatHubContext { get; }

        public ChatController(IHubContext<ChatHub, IChatClient> chatHubContext)
        {
            _strongChatHubContext = chatHubContext;
        }

        [HttpPost]
        public async Task SendMessage(string user, string message)
        {
            await _strongChatHubContext.Clients.All.ReceiveMessage(user, message);
        }
    }


}
