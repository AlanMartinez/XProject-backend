namespace XProject.API.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger(typeof(LoggerMiddleware));
        }

        public async Task Invoke(HttpContext context)
        {
            Guid guid = Guid.NewGuid();
            _logger.LogTrace($"Request {guid} iniciada");

            await _next(context);
        }
    }
}
