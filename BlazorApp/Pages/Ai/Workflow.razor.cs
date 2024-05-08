using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.GptWorkflow;
using BlazorApp.GptWorkflow.Workflow;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorApp.Pages.Ai;

public partial class Workflow : PageBase
{
    private readonly Context _ctx = new Context();
    private readonly List<Message> _messages = new List<Message>();
    private bool _showPrompt = false;

    private string _userInput;
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IAiService Ai { get; set; }
    [Inject] private IKubectlService Kubectl { get; set; }
    [Inject] private IWorkflowContainer Container { get; set; }

    private async Task ScrollToBottom()
    {
        await JsRuntime.InvokeVoidAsync("eval", @"
window.scrollToBottom = function (element) {
            element.scrollTop = element.scrollHeight;
        };
        window.scrollToBottom(document.getElementById('chat-box'));
");
    }

    private async Task SendMessage()
    {
        if (!string.IsNullOrWhiteSpace(_userInput))
        {
            _messages.Add(new Message { Content = _userInput, IsUser = true });
            _messages.Add(new Message { Content = _userInput, IsUser = false });
            if (_userInput.StartsWith("#"))
            {
                await Start(_userInput);
            }

            _userInput = string.Empty;
        }

        await ScrollToBottom();
    }


    protected override async Task OnInitializedAsync()
    {
        Container.RegisterWorkflow<InspectPodRepairWorkflow>();
        Container.RegisterWorkflow<EchoWorkflow>();
        await base.OnInitializedAsync();
    }

    private async Task Start(string task)
    {
        var workflowHost = Container.Host();

        _ctx.History = new List<string>();
        // _ctx.UserTask = "请检查kubernetes-dashboards命名空间下的pod运行状态";
        _ctx.UserTask = task;
        _ctx.AiService = Ai;
        _ctx.KubectlService = Kubectl;
        _ctx.Host = workflowHost;
        _ctx.OutputEventHandler = EventHandler;

        await workflowHost.StartWorkflow(InspectPodRepairWorkflow.Name, _ctx);
    }


    private async void EventHandler(object sender, string resp)
    {
        _messages.Add(new Message { Content = resp, IsUser = false });
        await InvokeAsync(StateHasChanged);
    }

    private void HandleKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
            _ = SendMessage();
        else if (e.Key == "#")
            _showPrompt = true;
        else
            _showPrompt = false;

        StateHasChanged();
    }

    private void InsertTag(string tag)
    {
        _userInput += tag;
        _showPrompt = false;
        StateHasChanged();
    }

    public class Message
    {
        public string Content { get; set; }
        public bool IsUser { get; set; }
    }
}
