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

    /// <summary>
    /// 当进入子流程时，需要保存当前的子流程名称
    /// 当退出子流程时，需要清除当前的子流程名称
    /// </summary>
    public string CurrentSubWorkflowName { get; set; }

    /// <summary>
    /// 是否允许循环
    /// </summary>
    public bool AllowLoop { get; set; }

    /// <summary>
    /// 循环次数
    /// </summary>
    public int LoopCount { get; set; } = 1;

    /// <summary>
    /// 最大循环次数
    /// </summary>
    public int MaxLoopCount { get; set; } = 5;

    /// <summary>
    /// 当前工作流名称
    /// </summary>
    public string CurrentWorkflowName { get; set; }

    /// <summary>
    /// 记录识别到的可执行代码类型
    /// </summary>
    public WorkflowConst.CodeType CodeType { get; set; }

    /// <summary>
    /// Decide 分支判断结果
    /// </summary>
    public string DecideResult { get; set; }
}
