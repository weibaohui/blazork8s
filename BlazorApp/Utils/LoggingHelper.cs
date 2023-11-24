using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class LoggingHelper<T>
{
    public static ILogger<T> Logger()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        var                  logger  = factory.CreateLogger<T>();
        return logger;
    }
}
