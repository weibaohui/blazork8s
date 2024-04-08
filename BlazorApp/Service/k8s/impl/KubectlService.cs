using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Service.k8s.impl;

public class KubectlService(ILogger<KubectlService> logger) : IKubectlService
{
    private bool _outPutAppendNewLine = false;

    /// <summary>
    ///  var gracefulCts = new CancellationTokenSource();
    ///  var token = gracefulCts.Token;
    ///  gracefulCts.CancelAfter(TimeSpan.FromSeconds(7));
    /// </summary>
    private CancellationToken Token { get; set; } = CancellationToken.None;

    public async Task<string> Apply(string yaml)
    {
        if (string.IsNullOrWhiteSpace(yaml))
        {
            return string.Empty;
        }

        //yaml 存为文件
        var guid = Guid.NewGuid().ToString();
        var path = $"{guid}.yaml";
        await File.WriteAllTextAsync(path, yaml, Token);
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

    public void SetCancellationToken(CancellationToken token)
    {
        Token = token;
    }


    public void SetOutputNewLineAppend(bool append)
    {
        _outPutAppendNewLine = append;
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
        await File.WriteAllTextAsync(path, yaml, Token);

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
            await foreach (var cmdEvent in cmd.ListenAsync(Token))
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


    private string OnResponseProcess(object sender, string data)
    {
        if (_outPutAppendNewLine && data?.EndsWith('\n') is not true) data += "\n";

        OnCommandExecutedHandler?.Invoke(sender, data);
        return data;
    }
}
