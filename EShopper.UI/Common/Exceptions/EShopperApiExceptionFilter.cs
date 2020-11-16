using EShopper.ApiContracts.ApiExceptions;
using EShopper.ApiContracts.ApiValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShopper.UI.Common.Exceptions
{
    public class EShopperApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            EShopperApiException eshopperException = null;
            if (context.Exception is EShopperApiException)
            {
                var ex = context.Exception as EShopperApiException;
                context.Exception = null;

            }
            else if (context.Exception is EShopperApiValidationError)
            {

            }
        }
    }
}
