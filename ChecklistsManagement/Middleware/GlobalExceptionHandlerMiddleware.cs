using ChecklistsManagement.Util;

namespace ChecklistsManagement.API
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is CustomException customEx)
            {
                response.StatusCode = customEx.StatusCode;
                var errorResponse = new ErrorResponse(customEx.StatusCode, customEx.ErrorMessage, null, context.Response.HttpContext.TraceIdentifier);
                return context.Response.WriteAsJsonAsync(errorResponse);
            }
            else
            {
                // Handle other exceptions
                response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorResponse1 = new ErrorResponse(response.StatusCode, "An unexpected error occurred");

                return context.Response.WriteAsJsonAsync(errorResponse1);
            }
        }
    }
}
