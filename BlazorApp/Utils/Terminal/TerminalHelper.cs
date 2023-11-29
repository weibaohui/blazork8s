using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.Terminal;

public class TerminalHelper
{
    private readonly ILogger<TerminalHelper> _logger = LoggingHelper<TerminalHelper>.Logger();

    public static  TerminalHelper               Instance => Nested.Instance;
    private static Dictionary<string, TerminalService> map = new();

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly TerminalHelper Instance = new TerminalHelper();
    }

    public TerminalService GetOrCreate(string key)
    {
        if (map.TryGetValue(key, out var executor))
        {
            return executor;
        }

        executor =  new TerminalService( new TerminalOptions());
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
}
