using System.Diagnostics;

namespace Pre_Trainee_Task.Middleware;

public class RequestLoggingMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context); // Call the next middleware (or endpoint)

        stopwatch.Stop();

        var method = context.Request.Method;
        var path = context.Request.Path;
        var statusCode = context.Response.StatusCode;
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        _logger.LogInformation(
            $"HTTP {method} {path} responded {statusCode} in {elapsedMs}ms");
    }
}
