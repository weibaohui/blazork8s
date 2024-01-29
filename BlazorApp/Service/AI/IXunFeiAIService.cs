using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Service.AI;

public interface IXunFeiAiService
{
    //解释错误
    public Task<string> ExplainError(string text);

    //解释安全问题
    public Task<string> ExplainSecurity(string text);

    bool         Enabled();
    Task<string> AIChat(string txtValue);

    void SetChatEventHandler(EventHandler<string> eventHandler);

//构造请求体
    public class JsonRequest
    {
        public Header    header    { get; set; }
        public Parameter parameter { get; set; }
        public Payload   payload   { get; set; }
    }

    public class Header
    {
        public string app_id { get; set; }
        public string uid    { get; set; }
    }

    public class Parameter
    {
        public Chat chat { get; set; }
    }

    public class Chat
    {
        public string domain           { get; set; }
        public string auditing         { get; set; }
        public double random_threshold { get; set; }
        public double temperature      { get; set; }
        public int    max_tokens       { get; set; }
    }

    public class Payload
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public IList<Content> text { get; set; }
    }

    public class Content
    {
        public string role    { get; set; }
        public string content { get; set; }
    }
}
