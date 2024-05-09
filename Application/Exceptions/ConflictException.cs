using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ConflictException(
        string message,
        List<string> errorMessages = default,
        HttpStatusCode statusCode = HttpStatusCode.Conflict)
        : Exception(message)
    {

        public List<string> ErrorMessages { get; set; } = errorMessages;
        public HttpStatusCode StatusCode { get; set; } = statusCode;
    }
}

