using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;


namespace Application.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            string mensaje = ""; 
            string Detailmsg = "";


            // para controlar excepciones de negocio
            if (context.Exception.GetType() == typeof(DataAccessException))
            {
                if (context.Exception is DataAccessException dataAccessException)
                {
                    statusCode = dataAccessException.StatusCode;
                    mensaje = dataAccessException.Message;
                    Detailmsg = dataAccessException.Message;
                }

               // var exception = (DataAccessException)context.Exception;
                var validation = new
                {
                    //    //statusCode = 400,
                    //    //title = "Bad Request",
                    //    estado = exception.Message,
                    //    mensaje = exception.Message
                    statusCode= statusCode,
                    mensaje= mensaje,
                    Detailmsg= Detailmsg

                };

                var result = new
                {
                    result = validation,
                    //detalle = ""
                };

                context.Result = new BadRequestObjectResult(result);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            //para controlar excepciones de base de datos 
            else if (context.Exception.GetType() == typeof(OracleException))
            {
                var exception = (OracleException)context.Exception;
                var validation = new
                {
                    StatusCode = 500,
                    Title = "Internal Server Error",
                    message = exception.Message
                };


                context.Result = new ObjectResult(validation)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };


            }


            // para controlar excepciones de cualquier otro tipo 
            else
            {
                var exception = (System.Exception)context.Exception;
                var validation = new
                {
                    //StatusCode = 500,
                    //Title = "Internal Server Error",
                    estado = "false",
                    mensaje = exception.GetType().Name + ' ' + exception.Message


                };

                var result = new
                {
                    result = validation,
                    detalle = ""
                };

                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
