using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Final.Filters
{
    public class LogActivityFilter : IActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User.Identity?.Name ?? "Anonymous";
            var path = context.HttpContext.Request.Path;
            _logger.LogInformation("Request: {Path} by {User}", path, user);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // İsteğin tamamlanmasından sonra ek loglama yapılabilir.
        }
    }
}