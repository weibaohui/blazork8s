using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils.PortForwarding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s;

public class PortForwardService : IHostedService, IDisposable
{
    private readonly ILogger<PortForwardService> _logger;
    private readonly IKubeService                _kubeService;
    private readonly IHubContext<ChatHub>        _ctx;
    private          PortForwardExecutorHelper   _helper;

    public PortForwardService(ILogger<PortForwardService> logger, IKubeService kubeService, IHubContext<ChatHub> ctx)
    {
        _logger = logger;
        Console.WriteLine("PortForwardService 初始化" + DateTime.Now);
        _kubeService = kubeService;
        _ctx         = ctx;
        _helper      = PortForwardExecutorHelper.Instance;
        _helper.SetIHubContext(_ctx);
    }


    public Task StartAsync(CancellationToken stoppingToken)
    {

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }


    public void Dispose()
    {
    }
}
