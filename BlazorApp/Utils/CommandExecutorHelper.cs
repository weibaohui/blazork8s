using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class CommandExecutorHelper
{
    private readonly ILogger<CommandExecutorHelper> _logger = LoggingHelper<CommandExecutorHelper>.Logger();

    public static  CommandExecutorHelper               Instance => Nested.Instance;
    private static Dictionary<string, CommandExecutor> map = new();

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly CommandExecutorHelper Instance = new CommandExecutorHelper();
    }

    public CommandExecutor GetOrCreate(string key)
    {
        if (map.TryGetValue(key, out var executor))
        {
            return executor;
        }

        executor = new CommandExecutor();
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
        executor.CurrentProcess?.Close();
        executor.CurrentProcess?.Dispose();
        // executor.CurrentProcess?.Kill();
    }
}
