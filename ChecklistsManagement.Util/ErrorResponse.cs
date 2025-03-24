using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Util
{
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP status code of the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// A human-readable message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A dictionary containing validation errors, where the key is the field name, and the value is an array of error messages.
        /// </summary>
        public Dictionary<string, string[]>? Errors { get; set; }

        /// <summary>
        /// Optional trace ID for debugging purposes.
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Constructor to initialize the error response.
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="message">Error message</param>
        /// <param name="errors">Validation errors (if applicable)</param>
        public ErrorResponse(int statusCode, string message, Dictionary<string, string[]>? errors = null, string? traceId = null)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
            TraceId = traceId;
        }
    }
}
