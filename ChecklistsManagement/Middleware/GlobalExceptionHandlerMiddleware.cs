using ChecklistsManagement.Util;

namespace ChecklistsManagement.API
{
    /// <summary>
    /// Middleware for handling global exceptions in the application.
    /// Captures unhandled exceptions, logs them, and returns a structured error response.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// Constructor for GlobalExceptionHandlerMiddleware.
        /// </summary>
        /// <param name="next">The next middleware component in the pipeline.</param>
        /// <param name="logger">Logger for recording exception details.</param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Middleware invocation method.
        /// Tries to process the request and catches any unhandled exceptions.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Pass the request to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred."); // Log the error
                await HandleExceptionAsync(context, ex); // Handle the exception and return an error response
            }
        }

        /// <summary>
        /// Handles exceptions and returns appropriate HTTP responses.
        /// </summary>
        /// <param name="context">HTTP context for the current request.</param>
        /// <param name="exception">The caught exception.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is CustomException customEx)
            {
                // Handle custom exceptions with specific status codes and messages
                response.StatusCode = customEx.StatusCode;
                var errorResponse = new ErrorResponse(customEx.StatusCode, customEx.ErrorMessage, null, context.Response.HttpContext.TraceIdentifier);
                return context.Response.WriteAsJsonAsync(errorResponse);
            }
            else
            {
                // Handle generic/unexpected exceptions
                response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorResponse = new ErrorResponse(response.StatusCode, "An unexpected error occurred");

                return context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
