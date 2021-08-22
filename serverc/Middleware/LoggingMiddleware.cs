using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Server.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
            _logger.LogInformation("init");
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var startTime = DateTime.UtcNow;
            var watch     = Stopwatch.StartNew();
            var result    = next.Invoke(context);
            watch.Stop();
            var logTemplate =
                @"Client IP: {clientIP} , Request path: {requestPath} ,Start time: {startTime} ,Duration: {duration}";
            _logger.LogInformation(logTemplate,
                context.Connection.RemoteIpAddress?.ToString(),
                context.Request.Path,
                startTime,
                watch.ElapsedMilliseconds);

            return result;
        }
    }
}
