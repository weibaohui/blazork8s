using System.Collections.Generic;
using BlazorApp.Service.AI;

namespace BlazorApp.GptWorkflow;

public class Context
{
    public string Prompt { get; set; }
    public string Command { get; set; }
    public string Result { get; set; }
    public string UserTask { get; set; }
    public IAiService AiService { get; set; }
    public IList<string> History { get; set; }
}
