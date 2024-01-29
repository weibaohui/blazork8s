using System;
using System.Threading.Tasks;

namespace BlazorApp.Service.AI;

public interface IOpenAiService
{
    //使用OPENAI解释错误
    public Task<string> ExplainError(string text);

    //解释安全问题
    public Task<string> ExplainSecurity(string text);

    bool         Enabled();
    Task<string> Chat(string txtValue);

    public class AIChatData
    {
        public Object data  { get; set; }
        public Object style { get; set; }
    }
}
