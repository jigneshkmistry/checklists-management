using ChecklistsManagement.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChecklistsManagement.API
{
    /// <summary>
    /// Action filter to validate incoming model state before executing a controller action.
    /// If validation fails, it returns a structured error response with validation errors.
    /// </summary>
    public class ValidationFilterAttribute : IActionFilter
    {
        /// <summary>
        /// Called before an action executes.
        /// Checks if the model state is valid, and if not, returns a 400 Bad Request response.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the model state is invalid
            if (!context.ModelState.IsValid)
            {
                // Extract validation errors and format them into a dictionary
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                // Create a structured error response
                var errorResponse = new ErrorResponse(
                    StatusCodes.Status400BadRequest,
                    "Validation failed.",
                    errors,
                    context.HttpContext.TraceIdentifier
                );

                // Return a 400 Bad Request response with the validation errors
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        /// <summary>
        /// Called after an action executes.
        /// This method is required by IActionFilter but is not used in this implementation.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
