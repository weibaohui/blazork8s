using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class LoggingUtils<T>
{
    public static ILogger<T> Logger()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        var                  logger  = factory.CreateLogger<T>();
        return logger;
    }
}