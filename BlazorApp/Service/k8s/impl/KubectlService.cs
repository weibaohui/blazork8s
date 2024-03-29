using System;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s.impl;

public class KubectlService(ILogger<KubectlService> logger) : IKubectlService
{
    public async Task<string> Apply(string yaml)
    {
        if (string.IsNullOrWhiteSpace(yaml))
        {
            return string.Empty;
        }

        //yaml 存为文件
        var guid = Guid.NewGuid().ToString();
        var path = $"{guid}.yaml";
        await File.WriteAllTextAsync(path, yaml);
        var output = await Kubectl($"apply -f {path}");
        File.Delete(path);
        return output; // 输出命令输出结果
    }

    public async Task<string> Explain(string filed)
    {
        if (string.IsNullOrWhiteSpace(filed))
        {
            return string.Empty;
        }

        return await Kubectl($" explain {filed}");
    }

    public async Task<string> Command(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            return string.Empty;
        }

        return await Kubectl($"  {command}");
    }


    public async Task<string> Describe(string resourceAndName)
    {
        return await Kubectl($" describe {resourceAndName}");
    }

    public void SetOutputEventHandler(EventHandler<string> eventHandler)
    {
        OnCommandExecutedHandler = eventHandler;
    }

    public async Task<string> Delete(string yaml)
    {
        if (string.IsNullOrWhiteSpace(yaml))
        {
            return string.Empty;
        }

        //yaml 存为文件
        var guid = Guid.NewGuid().ToString();
        var path = $"{guid}.yaml";
        await File.WriteAllTextAsync(path, yaml);

        var output = await Kubectl($"delete -f {path}");

        File.Delete(path);
        return output; // 输出命令输出结果
    }

    public event EventHandler<string> OnCommandExecutedHandler;


    private async Task<string> Kubectl(string command)
    {
        var cmd = Cli.Wrap("kubectl")
            .WithArguments(command);

        var result = string.Empty;
        try
        {
            await foreach (var cmdEvent in cmd.ListenAsync())
                switch (cmdEvent)
                {
                    case StartedCommandEvent started:
                        // Console.WriteLine($"Process started; ID: {started.ProcessId}");
                        // result += OnResponseProcess(this, $"Process exited; Code: {started.ProcessId}");
                        break;
                    case StandardOutputCommandEvent stdOut:
                        // Console.WriteLine($"Out> {stdOut.Text}");
                        result += OnResponseProcess(this, stdOut.Text);
                        break;
                    case StandardErrorCommandEvent stdErr:
                        // Console.WriteLine($"Err> {stdErr.Text}");
                        result += OnResponseProcess(this, stdErr.Text);
                        break;
                    case ExitedCommandEvent exited:
                        // Console.WriteLine($"Process exited; Code: {exited.ExitCode}");
                        // result += OnResponseProcess(this, $"Process exited; Code: {exited.ExitCode}");
                        break;
                }
        }
        catch (Exception e)
        {
            result = e.Message;
            logger.LogError("{Message}", e.Message);
        }

        return result;
    }


    // private async Task<string> Kubectl2(string command)
    // {
    //     var process = new Process();
    //     process.StartInfo.FileName               = "kubectl"; // 设置要执行的 kubectl 命令
    //     process.StartInfo.Arguments              = command;   // 设置命令参数
    //     process.StartInfo.UseShellExecute        = false;     // 不使用操作系统的 shell
    //     process.StartInfo.RedirectStandardOutput = true;      // 重定向输出
    //     process.StartInfo.RedirectStandardError  = true;      // 重定向输出
    //     process.Start();                                      // 启动进程
    //
    //     var result = string.Empty;
    //     process.OutputDataReceived += (sender, e) => { result += OnResponseProcess(sender, e.Data); };
    //     process.ErrorDataReceived  += (sender, e) => { result += OnResponseProcess(sender, e.Data); };
    //     process.BeginOutputReadLine();
    //     process.BeginErrorReadLine();
    //     await process.WaitForExitAsync();
    //     return result;
    // }

    private string OnResponseProcess(object sender, string data)
    {
        var ret = data;
        if (data?.EndsWith('\n') is not true)
        {
            ret = data + "\n";
        }

        OnCommandExecutedHandler?.Invoke(sender, ret);
        return ret;
    }
}
