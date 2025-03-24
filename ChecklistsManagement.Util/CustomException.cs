using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Util
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }

        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
