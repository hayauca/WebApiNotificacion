using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
   // public class DataAccessException : ApplicationException
    public class DataAccessException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public DataAccessException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public DataAccessException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, innerException)
        {
            StatusCode = statusCode;

           
        }
    }
}
