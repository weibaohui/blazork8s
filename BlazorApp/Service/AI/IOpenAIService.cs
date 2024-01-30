using System;

namespace BlazorApp.Service.AI;

public interface IOpenAiService:IAiService
{

    public class AIChatData
    {
        public Object data  { get; set; }
        public Object style { get; set; }
    }
}
