using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Exceptions
{
    public class ApiException
    {
        public ApiException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; init; }
        public string Message { get; init; }
        public string Details { get; init; }
    }
}