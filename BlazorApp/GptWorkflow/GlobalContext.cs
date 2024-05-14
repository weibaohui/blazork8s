using System;
using System.Collections.Generic;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;

namespace BlazorApp.GptWorkflow;

public class GlobalContext
{
    public readonly ILogger<GlobalContext> Logger = LoggingHelper<GlobalContext>.Logger();
    public string UserTask { get; set; }
    public Message LatestMessage { get; set; }
    public IList<string> History { get; set; }
    public IAiService AiService { get; set; }
    public IKubectlService KubectlService { get; set; }
    public IWorkflowHost Host { get; set; }
    public Action<object, Message> OutputEventHandler { get; set; }

    public string CurrentSubWorkflowName { get; set; }
}
