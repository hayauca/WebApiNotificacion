using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class HandleExceptionAsync
    {
        public Task ExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string result;

            if (exception is DataAccessException dataAccessException)
            {
                statusCode = dataAccessException.StatusCode;
                result = dataAccessException.Message;
            }
            //else if (exception is ServiceException serviceException)
            //{
            //    statusCode = serviceException.StatusCode;
            //    result = serviceException.Message;
            //}
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                result = "An unexpected error occurred.";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(new { error = result }.ToString());
        }
    }
}
