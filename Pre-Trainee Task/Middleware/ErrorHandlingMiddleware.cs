using System.Net;
using System.Text.Json;

namespace Pre_Trainee_Task.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new
        {
            error = "An unexpected error occurred. Please try again later."
        });
        return context.Response.WriteAsync(result);
    }
}
