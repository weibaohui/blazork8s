using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace server.Middleware
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
            var logTemplate = @" 
Client IP: {clientIP} 
Request path: {requestPath} 
Request content type: {requestContentType} 
Request content length: {requestContentLength} 
Start time: {startTime} 
Duration: {duration}
-----------
Response Body:{}
";
            _logger.LogInformation(logTemplate,
                context.Connection.RemoteIpAddress?.ToString(),
                context.Request.Path,
                context.Request.ContentType,
                context.Request.ContentLength,
                startTime,
                watch.ElapsedMilliseconds,
                context.Response.Body);

            return result;
        }
    }
}
