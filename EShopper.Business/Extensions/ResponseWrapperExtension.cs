using EShopper.Common.Middleware.ResponseHandler;
using Microsoft.AspNetCore.Builder;

namespace EShopper.Business.Extensions
{
    public static class ResponseWrapperExtension
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseWrapper>();
        }
    }
}