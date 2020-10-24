using EShopper.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EShopper.Business.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorResponse = new EShopperExceptionServiceErrorResponse();

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (context.Exception is EShopperException eShopperException)
            {
                errorResponse.Message = eShopperException.Message;
            }
            else
            {
                errorResponse.Message = context.Exception.Message;
            }

            context.HttpContext.Items.Add("exception", context.Exception);
            context.HttpContext.Items.Add("exceptionMessage", errorResponse.Message.ToString());
            context.Result = new JsonResult(errorResponse);
        }
    }
}