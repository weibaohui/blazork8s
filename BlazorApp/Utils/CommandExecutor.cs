#nullable enable
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public sealed class CommandExecutor
{
    private  readonly ILogger<CommandExecutor>           _logger = LoggingHelper<CommandExecutor>.Logger();
    public event EventHandler<CommandExecutedEventArgs>? CommandExecuted;
    public Process?                                      CurrentProcess;

    /// <summary>
    /// Executes a command asynchronously.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="arguments">The arguments for the command.</param>
    public async Task ExecuteCommandAsync(string command, string? arguments)
    {
        try
        {
            using var process = new Process();
            process.StartInfo.FileName               = command;
            process.StartInfo.Arguments              = arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError  = true;
            process.StartInfo.UseShellExecute        = false;
            process.StartInfo.CreateNoWindow         = true;
            var tcs = new TaskCompletionSource<bool>();

            process.ErrorDataReceived += (sender, e) =>
            {
                // _logger.LogError($"Command error: {e.Data}");
                OnCommandExecuted(new CommandExecutedEventArgs { Output = e.Data });
            };
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data == null)
                {
                    tcs.SetResult(true);
                }
                else
                {
                    // _logger.LogInformation($"Command output: {e.Data}");
                    OnCommandExecuted(new CommandExecutedEventArgs { Output = e.Data });
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            CurrentProcess = process;
            await process.WaitForExitAsync();
            // Wait for the command to finish executing
            await tcs.Task;

            _logger.LogInformation($"Command exited with code {process.ExitCode}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error executing command: {ex.Message}");
        }
    }

    private void OnCommandExecuted(CommandExecutedEventArgs e)
    {
        CommandExecuted?.Invoke(this, e);
    }

    private static async Task Example(string[] args)
    {
        var commandExecutor = new CommandExecutor();

        // 订阅事件
        commandExecutor.CommandExecuted += (sender, e) =>
        {
            Console.WriteLine($"Command executed with output: {e.Output}");
        };

        // 异步执行命令
        await commandExecutor.ExecuteCommandAsync("your_command", "your_arguments");
    }
}

public class CommandExecutedEventArgs : EventArgs
{
    public string? Output { get; set; }
}
