using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.GptWorkflow;
using BlazorApp.GptWorkflow.Workflow;
using BlazorApp.Pages.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorApp.Pages.Ai;

public partial class Workflow : PageBase
{
    private readonly List<Message> _messages = new List<Message>();
    private bool _showPrompt = false;
    private string _userInput;
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IWorkflowStarter WorkflowStarter { get; set; }

    private async Task ScrollToBottom()
    {
        await JsRuntime.InvokeVoidAsync("eval", @"
var chatContent = document.getElementById(""chat-box"");
chatContent.scrollTop = chatContent.scrollHeight;");
    }

    private async Task SendMessage()
    {
        if (!string.IsNullOrWhiteSpace(_userInput))
        {
            _messages.Add(new Message { Content = _userInput, IsUser = true });
            _messages.Add(new Message { Content = _userInput, IsUser = false });
            if (_userInput.StartsWith("#"))
            {
                await WorkflowStarter.Start(_userInput, InspectPodRepairWorkflow.Name, EventHandler);
            }

            _userInput = string.Empty;

            await InvokeAsync(StateHasChanged);
        }

        await ScrollToBottom();
        await ScrollToBottom();
    }


    private async void EventHandler(object sender, string resp)
    {
        _messages.Add(new Message { Content = resp, IsUser = false });
        await InvokeAsync(StateHasChanged);
        await ScrollToBottom();
        await ScrollToBottom();
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
