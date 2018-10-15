using Memby.Application.Miscellaneous;
using Memby.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Memby.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ApplicationExceptionBase ex)
            {
                await HandleApplicationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleApplicationExceptionAsync(HttpContext context, ApplicationExceptionBase exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
            }

            var result = JsonConvert.SerializeObject(new ExceptionResult() { Message = exception.Message, MessageCode = exception.MessageCode });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new ExceptionResult() { Message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
