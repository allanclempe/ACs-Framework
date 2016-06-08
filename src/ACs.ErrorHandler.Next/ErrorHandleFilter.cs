using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace ACs.ErrorHandler.Next
{
    public class ErrorHandleFilter<T> : IExceptionFilter where T : ErrorHandlerException
    {
        public void OnException(ExceptionContext context)
        {
            var error = context.Exception as T;

            if (error == null) return;

            if (error.ErrorCode == 404)
            {
                context.Result = new HttpNotFoundResult();
                return;
            }

            context.Result = new BadRequestObjectResult(error);
        }
    }
}
