using System.Threading.Tasks;

namespace BlazorApp.Service;

public interface IRockAiService
{
    Task<string> Chat(string txtValue);
}
