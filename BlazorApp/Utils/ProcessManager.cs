using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ProcessManager
{
    private static readonly ILogger<ProcessManager> Logger = LoggingHelper<ProcessManager>.Logger();
    private readonly Dictionary<string, Process> _services = new Dictionary<string, Process>();

    public static ProcessManager Instance => Nested.Instance;
    public bool IsStandardOutPutSet => StandardOutput != null;

    /// <summary>
    ///     todo 改为按name 设置，否则多个服务会使用同一个输出
    /// </summary>
    public event EventHandler<string> StandardOutput;

    public event EventHandler<string> StandardError;

    public Process GetService(string name)
    {
        return _services[name];
    }

    public void StartService(string name, string binPath, string args)
    {
        if (_services.ContainsKey(name))
        {
            Logger.LogInformation("服务 {Name} 已经在运行", name);
            return;
        }


        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = binPath,
                Arguments = args,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                EnvironmentVariables =
                {
                    ["TERM"] = "xterm"
                }
            }
        };

        process.OutputDataReceived += (sender, arg) =>
        {
            StandardOutput?.Invoke(sender, arg.Data);
            Logger.LogInformation("[{Name}] 输出: {ArgData}", name, arg.Data);
        };
        process.ErrorDataReceived += (sender, arg) =>
        {
            StandardError?.Invoke(sender, arg.Data);
            Logger.LogInformation("[{Name}] 错误: {ArgData}", name, arg.Data);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        _services[name] = process;

        Logger.LogInformation("服务 {Name} 已启动", name);
    }

    public void SendCommandToService(string name, string command)
    {
        if (_services.ContainsKey(name))
        {
            var process = _services[name];
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            Logger.LogInformation("命令已发送到服务 {Name}: {Command}", name, command);
        }
        else
        {
            Logger.LogInformation("未找到运行中的服务 {Name}", name);
        }
    }

    public void StopService(string name)
    {
        if (_services.ContainsKey(name))
        {
            _services[name].Kill();
            _services[name].Dispose();
            _services.Remove(name);
            Logger.LogInformation("服务 {Name} 已停止", name);
        }
        else
        {
            Logger.LogInformation("未找到运行中的服务 {Name}", name);
        }
    }

    private class Nested
    {
        internal static readonly ProcessManager Instance = new ProcessManager();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }
    }
}
