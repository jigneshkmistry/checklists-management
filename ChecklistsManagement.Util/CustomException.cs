using System;

namespace ChecklistsManagement.Util
{
    /// <summary>
    /// Custom exception class for handling application-specific errors with HTTP status codes.
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code associated with the exception.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the error message associated with the exception.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class with a specified status code and error message.
        /// </summary>
        /// <param name="statusCode">The HTTP status code representing the type of error.</param>
        /// <param name="message">The error message describing the exception.</param>
        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
