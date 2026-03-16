using System.Collections.Concurrent;

namespace PropertyMap.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private static readonly ConcurrentDictionary<string, ClientRequestInfo> _clients = new();
        private readonly int _maxRequests = 100; // Max requests per time window
        private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1); // 1 minute window

        public RateLimitingMiddleware(
            RequestDelegate next,
            ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = GetClientIdentifier(context);

            var clientInfo = _clients.GetOrAdd(clientId, new ClientRequestInfo());

            lock (clientInfo)
            {
                // Clean up old requests
                clientInfo.RequestTimes.RemoveAll(t => t < DateTime.UtcNow - _timeWindow);

                if (clientInfo.RequestTimes.Count >= _maxRequests)
                {
                    _logger.LogWarning("Rate limit exceeded for client: {ClientId}", clientId);
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers.RetryAfter = _timeWindow.TotalSeconds.ToString();
                    return;
                }

                clientInfo.RequestTimes.Add(DateTime.UtcNow);
            }

            await _next(context);
        }

        private string GetClientIdentifier(HttpContext context)
        {
            // Use IP address as identifier
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            // Also consider authenticated user if available
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst("userId")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    return $"user_{userId}";
                }
            }

            return $"ip_{ip}";
        }

        private class ClientRequestInfo
        {
            public List<DateTime> RequestTimes { get; set; } = new();
        }
    }
}