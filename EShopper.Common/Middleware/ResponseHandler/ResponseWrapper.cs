using EShopper.Contracts.V1.Responses;
using EShopper.Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EShopper.Common.Middleware.ResponseHandler
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
            }

            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                object objResult = null;
                var result = new EShopperResponse((HttpStatusCode)context.Response.StatusCode);

                if (readToEnd.ValidateJSON())
                {
                    objResult = JsonConvert.DeserializeObject(readToEnd);
                    result = EShopperResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, null);
                }
                else
                {
                    objResult = readToEnd;
                    result = EShopperResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, null);
                }

                string errorMessage = string.Empty;

                if (context.Items["exception"] != null)
                {
                    errorMessage = context.Items["exceptionMessage"].ToString();
                    result = EShopperResponse.Create((HttpStatusCode)context.Response.StatusCode, null, errorMessage);
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }

        }

    }
}