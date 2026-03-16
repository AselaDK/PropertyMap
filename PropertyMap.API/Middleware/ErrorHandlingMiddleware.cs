using System.Net;
using System.Text.Json;
using PropertyMap.Application.DTOs.Shared;

namespace PropertyMap.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred");

            var response = context.Response;
            response.ContentType = "application/json";

            var apiResponse = new ApiResponse<object>
            {
                Success = false,
                Message = "An error occurred while processing your request"
            };

            switch (exception)
            {
                case UnauthorizedAccessException uex:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    apiResponse.Message = uex.Message;
                    break;

                case KeyNotFoundException kex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    apiResponse.Message = kex.Message;
                    break;

                case ArgumentException argEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    apiResponse.Message = argEx.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    // Add detailed error in development
                    if (_env.IsDevelopment())
                    {
                        apiResponse.Errors = new List<string>
                        {
                            exception.Message,
                            exception.StackTrace ?? string.Empty
                        };
                    }
                    break;
            }

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonResponse = JsonSerializer.Serialize(apiResponse, jsonOptions);
            await response.WriteAsync(jsonResponse);
        }
    }
}