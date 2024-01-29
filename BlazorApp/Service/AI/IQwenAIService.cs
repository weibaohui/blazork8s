using System;
using System.Threading.Tasks;

namespace BlazorApp.Service.AI;

/// <summary>
/// 阿里通义千问
/// </summary>
public interface IQwenAiService
{
    //解释错误
    public Task<string> ExplainError(string text);

    //解释安全问题
    public Task<string> ExplainSecurity(string text);

    bool         Enabled();
    Task<string> AIChat(string txtValue);

    void SetChatEventHandler(EventHandler<string> eventHandler);

}
