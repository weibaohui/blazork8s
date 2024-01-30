using System;
using System.Threading.Tasks;

namespace BlazorApp.Service.AI;

public interface IAiService
{
     bool Enabled()
    {
        return false;
    }

    Task<string> ExplainError(string text);

    Task<string> ExplainSecurity(string text);


    Task<string> AIChat(string text);

    void SetChatEventHandler(EventHandler<string> handler);
    string Selected()
    {
        return string.Empty;
    }

    /// <summary>
    /// AIName
    /// </summary>
    /// <returns></returns>
    string Name();

}
