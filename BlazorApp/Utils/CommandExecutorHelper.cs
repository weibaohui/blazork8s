using System.Collections.Generic;

namespace BlazorApp.Utils;

public class CommandExecutorHelper
{
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

    public CommandExecutor Create(string key)
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
        executor.CurrentProcess?.Kill();
        map.Remove(key);
    }
}
