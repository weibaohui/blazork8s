using System.Threading.Tasks;

namespace BlazorApp.Service;

public interface IOpenAiService
{
    //使用OPENAI解释
    public Task<string> Explain(string text);
    bool         Enabled();
}
