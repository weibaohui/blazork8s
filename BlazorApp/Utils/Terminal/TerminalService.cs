#nullable enable
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pty.Net;

namespace BlazorApp.Utils.Terminal;

public class TerminalService
{
    /// <summary>
    /// 退出事件
    /// </summary>
    event EventHandler<PtyExitedEventArgs>? StandardExited;

    /// <summary>
    /// 异常输出
    /// </summary>
    public event EventHandler<Exception>? StandardError;

    /// <summary>
    /// 正常输出（String）
    /// </summary>
    public event EventHandler<string>? StandardOutput;

    public bool IsStandardOutPutSet => StandardOutput != null;

    /// <summary>
    /// 流输出
    /// </summary>
    public event EventHandler<byte[]>? StandardBytesOutput;


    /// <summary>
    /// 配置
    /// </summary>
    public TerminalOptions? TerminalOptions
    {
        get => new()
        {
            Name                = _options.Name,
            Rows                = _options.Rows,
            Cols                = _options.Cols,
            Cwd                 = _options.Cwd,
            App                 = _options.App,
            CommandLine         = _options.CommandLine,
            VerbatimCommandLine = _options.VerbatimCommandLine,
            ForceWinPty         = _options.ForceWinPty,
            Environment         = _options.Environment,
        };
        set
        {
            if (IsRunning)
            {
                throw new Exception("Terminal is running, can not change options");
            }

            value ??= new TerminalOptions();

            _options = new PtyOptions
            {
                Name                = value.Name,
                Rows                = value.Rows,
                Cols                = value.Cols,
                Cwd                 = value.Cwd,
                App                 = value.App,
                CommandLine         = value.CommandLine,
                VerbatimCommandLine = value.VerbatimCommandLine,
                ForceWinPty         = value.ForceWinPty,
                Environment         = value.Environment,
            };
        }
    }

    /// <summary>
    /// 取消令牌
    /// </summary>
    public CancellationTokenSource TerminalCancellationTokenSource { get; } = new();

    private CancellationToken CancellationToken { get; set; }

    /// <summary>
    /// 是否在运行
    /// </summary>
    public bool IsRunning { get; private set; }

    /// <summary>
    /// 任务完成的标记
    /// </summary>
    private readonly TaskCompletionSource<uint> _processExitedTcs = new();

    /// <summary>
    /// 编码
    /// </summary>
    public readonly UTF8Encoding Encoding = new(encoderShouldEmitUTF8Identifier: false);


    public int GerProcessId => Terminal?.Pid ?? -1;

    /// <summary>
    /// Pty
    /// </summary>
    public IPtyConnection? Terminal { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TerminalService(TerminalOptions terminalOptions)
    {
        TerminalOptions = terminalOptions;
    }

    public TerminalService()
    {
        TerminalOptions = null;
    }

    private PtyOptions _options { get; set; }


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
            Console.WriteLine(e);
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
                Terminal          = await PtyProvider.SpawnAsync(_options, CancellationToken).ConfigureAwait(false);
                Terminal.ProcessExited += (sender, e) =>
                {
                    _processExitedTcs.TrySetResult((uint)Terminal.ExitCode);
                    StandardExited?.Invoke(sender, e);
                    TerminalCancellationTokenSource.Cancel();
                };
                OutputMessage();
                IsRunning = true;
            }
            catch (Exception e)
            {
                StandardError?.Invoke(this, e);
                IsRunning = false;
                throw;
            }
        }
    }


    private void OutputMessage()
    {
        Task.Run(async () =>
        {
            while (!CancellationToken.IsCancellationRequested && !_processExitedTcs.Task.IsCompleted)
            {
                try
                {
                    var bytes = new byte[4096];
                    var count = await Terminal!.ReaderStream.ReadAsync(bytes, CancellationToken).ConfigureAwait(false);
                    if (count == 0)
                    {
                        break;
                    }

                    StandardBytesOutput?.Invoke(this, bytes);
                    StandardOutput?.Invoke(this, Encoding.GetString(bytes, 0, count));
                }
                catch (Exception e)
                {
                    StandardError?.Invoke(this, e);
                    IsRunning = false;
                    throw;
                }
            }
        }, CancellationToken).ConfigureAwait(false);
    }

    public async Task Write(string data)
    {
        await Write(Encoding.GetBytes(data)).ConfigureAwait(false);
    }

    public async Task Write(char data)
    {
        await Write(Convert.ToByte(data)).ConfigureAwait(false);
    }

    public async Task Write(byte data)
    {
        CheckTerminal();
        Terminal!.WriterStream.WriteByte(data);
        await Terminal.WriterStream.FlushAsync(CancellationToken).ConfigureAwait(false);
    }

    public async Task Write(byte[] data)
    {
        CheckTerminal();
        await Terminal!.WriterStream.WriteAsync(data, CancellationToken).ConfigureAwait(false);
        await Terminal.WriterStream.FlushAsync(CancellationToken).ConfigureAwait(false);
    }

    private void CheckTerminal()
    {
        if (!IsRunning || Terminal == null)
        {
            var exception = new Exception("Terminal not running");

            StandardError?.Invoke(this, exception);

            throw exception;
        }
    }
}
