using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;

namespace BlazorApp.Pages.Settings;

public partial class Run : BlazorApp.Pages.Common.PageBase, IDisposable
{
    private TimeSpan _runningTime;

    private Timer _timer;
    [Inject] IHostApplicationLifetime ApplicationLifetime { get; set; }
    [Inject] private IMessageService MessageService { get; set; }

    [Parameter] public DateTime? Age { get; set; }


    public void Dispose()
    {
        _timer.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _timer = new Timer(1000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
    }

    private async Task OnTimerCallback()
    {
        // 获取当前进程
        Process currentProcess = Process.GetCurrentProcess();

        // 获取进程启动时间
        DateTime startTime = currentProcess.StartTime;

        // 获取当前时间
        DateTime currentTime = DateTime.Now;

        // 计算已运行时间
        _runningTime = currentTime - startTime;
        await InvokeAsync(StateHasChanged);
    }


    private Task Restart()
    {
        // 延迟重启以确保API请求完成
        Task.Run(() =>
        {
            System.Threading.Thread.Sleep(1000);
            MessageService.Success($"{L["RestartSuccess"]}");
            ApplicationLifetime.StopApplication();
        });


        return Task.CompletedTask;
    }
}
