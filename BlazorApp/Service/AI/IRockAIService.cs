using System.Threading.Tasks;

namespace BlazorApp.Service.AI;

public interface IRockAiService
{
    Task<string> Chat(string txtValue);
}
