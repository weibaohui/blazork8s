using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ProcessManager
{
    private static readonly ILogger<ProcessManager> Logger = LoggingHelper<ProcessManager>.Logger();
    private readonly Dictionary<string, Process> _services = new Dictionary<string, Process>();


    public static ProcessManager Instance => Nested.Instance;

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

                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.OutputDataReceived += (sender, arg) => Logger.LogInformation("[{Name}] 输出: {ArgData}", name, arg.Data);
        process.ErrorDataReceived += (sender, arg) => Logger.LogInformation("[{Name}] 错误: {ArgData}", name, arg.Data);

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        _services[name] = process;

        Logger.LogInformation("服务 {Name} 已启动", name);
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
