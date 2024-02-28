using System;
using System.Threading.Tasks;
using System.Timers;
using Extension;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Common.Metadata;

public partial class AgeView : ComponentBase,IDisposable
{
    [Parameter]
    public DateTime? Age { get; set; }

    [Inject]
    private ILogger<AgeView> Logger { get; set; }

    private string _kubeAge;
    private Timer  _timer;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _timer         =  new Timer(1000);
        _timer.Elapsed += async (sender, eventArgs) =>await OnTimerCallback();
        _timer.Start();
    }

    private async Task OnTimerCallback()
    {
         _kubeAge = Age.AgeFromUtc();
        await InvokeAsync(StateHasChanged);
    }
    public void Dispose()
    {
        _timer.Dispose();
    }
}
