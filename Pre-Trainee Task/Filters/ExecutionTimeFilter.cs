using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pre_Trainee_Task.Filters;

public class ExecutionTimeFilter : IActionFilter
{
    private readonly ILogger<ExecutionTimeFilter> _logger;
    private readonly Stopwatch _stopwatch;

    public ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Restart(); // Start timing before the action executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop(); // Stop timing after action executes

        var actionName = context.ActionDescriptor.DisplayName;
        var elapsedMs = _stopwatch.ElapsedMilliseconds;

        _logger.LogInformation(
            $"Action {actionName} executed in {elapsedMs} ms");
    }
}
