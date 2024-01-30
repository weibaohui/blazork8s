using System.Collections.Generic;

namespace BlazorApp.Service.AI;

/// <summary>
/// 科大讯飞星火大模型
/// </summary>
public interface IXunFeiAiService : IAiService
{
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
