using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Pty.Net;

namespace BlazorApp.Utils.Terminal;

public class TerminalHelper
{
    private static   Dictionary<string, TerminalService> map     = new();
    private readonly ILogger<TerminalHelper>             _logger = LoggingHelper<TerminalHelper>.Logger();

    public static TerminalHelper Instance => Nested.Instance;

    public TerminalService GetOrCreate(string key)
    {
        if (map.TryGetValue(key, out var executor))
        {
            return executor;
        }

        var opt = new PtyOptions
        {
            App = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Path.Combine(Environment.SystemDirectory, "cmd.exe")
                : "bash",
            Name = key,
            Cwd  = AppDomain.CurrentDomain.BaseDirectory,
            Cols = 100,
            Rows = 20
        };

        executor = new TerminalService(opt);
        var result = map.TryAdd(key, executor);
        return executor;
    }

    public void Kill(string key)
    {
        if (!map.TryGetValue(key, out var executor))
        {
            return;
        }

        map.Remove(key);
        executor.Dispose();
    }

    private class Nested
    {
        internal static readonly TerminalHelper Instance = new TerminalHelper();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }
    }
}
