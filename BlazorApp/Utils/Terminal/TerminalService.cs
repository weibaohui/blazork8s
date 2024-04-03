#nullable enable
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pty.Net;

namespace BlazorApp.Utils.Terminal;

public class TerminalService(PtyOptions options)
{
    private readonly UTF8Encoding               _encoding         = new(false);
    private readonly TaskCompletionSource<uint> _processExitedTcs = new();
    public           bool                       IsRunning                       { get; private set; }
    public           bool                       IsStandardOutPutSet             => StandardOutput != null;
    private          CancellationTokenSource    TerminalCancellationTokenSource { get; } = new();
    private          CancellationToken          CancellationToken               { get; set; }
    public           int                        GerProcessId                    => Terminal?.Pid ?? -1;
    private          IPtyConnection?            Terminal                        { get; set; }
    private          PtyOptions                 Options                         { get; set; } = options;
    public event EventHandler<string>?          StandardOutput;

    public void Dispose()
    {
        if (Terminal == null || _processExitedTcs.Task.IsCompleted)
        {
            return;
        }

        try
        {
            Terminal.Dispose();
            Terminal.Kill();
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    /// <summary>
    /// 启动Pty
    /// </summary>
    public async Task Start()
    {
        if (Terminal == null || _processExitedTcs.Task.IsCompleted)
        {
            try
            {
                CancellationToken = TerminalCancellationTokenSource.Token;
                Terminal          = await PtyProvider.SpawnAsync(Options, CancellationToken);
                Terminal.ProcessExited += (sender, e) =>
                {
                    _processExitedTcs.TrySetResult((uint)Terminal.ExitCode);

                    TerminalCancellationTokenSource.Cancel();
                };
                OutputMessage();
                IsRunning = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Start failed:{e.Message}");
                IsRunning = false;
                await TerminalCancellationTokenSource.CancelAsync();
            }
        }
    }


    private void OutputMessage()
    {
        Task.Run(async () =>
        {
            while (!CancellationToken.IsCancellationRequested && !_processExitedTcs.Task.IsCompleted)
            {
                if (Terminal == null) continue;
                var bytes = new byte[4096];
                var count = await Terminal.ReaderStream.ReadAsync(bytes, CancellationToken).ConfigureAwait(false);
                if (count == 0) break;

                StandardOutput?.Invoke(this, _encoding.GetString(bytes, 0, count));
            }
        }, CancellationToken);
    }

    public async Task Write(string data)
    {
        await Write(_encoding.GetBytes(data));
    }

    private async Task Write(byte[] data)
    {
        if (IsRunning && Terminal != null)
        {
            await Terminal.WriterStream.WriteAsync(data, CancellationToken);
            await Terminal.WriterStream.FlushAsync(CancellationToken);
        }
    }
}
