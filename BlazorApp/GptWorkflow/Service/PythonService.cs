using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Service.k8s.impl;
using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;

namespace BlazorApp.GptWorkflow.Service;

public class PythonService(ILogger<KubectlService> logger) : IPythonService
{
    private readonly bool _outPutAppendNewLine = true;

    /// <summary>
    ///     var gracefulCts = new CancellationTokenSource();
    ///     var token = gracefulCts.Token;
    ///     gracefulCts.CancelAfter(TimeSpan.FromSeconds(7));
    /// </summary>
    private CancellationToken Token { get; set; } = CancellationToken.None;

    public async Task<string> Run(string code)
    {
        Console.WriteLine(code);
        if (string.IsNullOrWhiteSpace(code)) return string.Empty;

        //code 存到文件夹中，进行系列初始化，包括生产requirements.txt，生成venv，安装依赖等

        var guid = Guid.NewGuid().ToString();
        Directory.CreateDirectory(guid);

        var path = $"{guid}/main.py";
        await File.WriteAllTextAsync(path, code, Token);


        MakeExecutableShell($"{guid}/run.sh");
        var output = await ShellRun("bash", $"{guid}/run.sh");
        Directory.Delete(guid, true);
        return output; // 输出命令输出结果
    }


    public void SetOutputEventHandler(EventHandler<string> eventHandler)
    {
        OnCommandExecutedHandler = eventHandler;
    }

    public void SetCancellationToken(CancellationToken token)
    {
        Token = token;
    }

    private static void MakeExecutableShell(string path)
    {
        var folder = Path.GetDirectoryName(path);
        var shell = """
                    #!/bin/bash
                    cd {folder}
                    python3 -m venv .venv
                    source .venv/bin/activate;
                    # pip3 freeze > requirements.txt;
                    #uv pip freeze > requirements.txt;
                    pip3 list --format=freeze > requirements.txt .venv/bin/activate;
                    pip3 install -r requirements.txt;
                    python3 main.py;
                    """;
        shell = shell.Replace("{folder}", folder);
        File.WriteAllText(path, shell);
    }


    public event EventHandler<string> OnCommandExecutedHandler;


    private async Task<string> ShellRun(string command, string args)
    {
        if (Token.IsCancellationRequested) Token = CancellationToken.None;

        var cmd = Cli.Wrap(command)
            .WithValidation(CommandResultValidation.None)
            .WithArguments(args);
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
            logger.LogInformation("{Message}", e.Message);
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

public interface IPythonService
{
    Task<string> Run(string code);
    void SetOutputEventHandler(EventHandler<string> eventHandler);
    void SetCancellationToken(CancellationToken token);
}
