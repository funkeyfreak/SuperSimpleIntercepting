namespace Server 
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    public class Middlewear : IMiddleware
    {
        private readonly ILogger<Middlewear>  _logger;

        public Middlewear(ILogger<Middlewear> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Inbound (before the controller)
            var request = context?.Request;
            if (request == null)
            {
                await next(context);
                return;
            }

            request.EnableBuffering();  // Allows us to reuse the existing Request.Body

            // Swap the original Response.Body stream with one we can read / seek
            // var originalResponseBody = context?.Response.Body;
            // using var replacementResponseBody = new MemoryStream();
            // context.Response.Body = replacementResponseBody;            
            _logger.LogWarning($"lovely => {request.Path}");
            if (!context?.Response.HasStarted ?? false){
                var response1 = context?.Response;
                if(response1 != null) 
                {
                    response1.Headers["lovelyday"] = "yes, it is";
                }
            }

            await next(context); // Continue processing (additional middleware, controller, etc.)

            // Outbound (after the controller)
            // replacementResponseBody.Position = 0;

            // Copy the response body to the original stream
            // await replacementResponseBody.CopyToAsync(originalResponseBody).ConfigureAwait(false);
            //context.Response.Body = originalResponseBody;

            var response = context?.Response;
            if (response != null){
                if (response.StatusCode < 500)
                {
                    return;
                }

                //var requestTelemetry = context.Features.Get<RequestTelemetry>();
                /*if (requestTelemetry == null)
                {
                    return;
                }*/

                /*if (request.Body.CanRead)
                {
                    var requestBodyString = await ReadBodyStream(request.Body).ConfigureAwait(false);
                    requestTelemetry.Properties.Add("RequestBody", requestBodyString);  // limit: 8192 characters
                    TelemetryClient.TrackTrace(requestBodyString);
                }

                if (replacementResponseBody.CanRead)
                {
                    var responseBodyString = await ReadBodyStream(replacementResponseBody).ConfigureAwait(false);
                    requestTelemetry.Properties.Add("ResponseBody", responseBodyString);
                    TelemetryClient.TrackTrace(responseBodyString);
                }*/
            }
        }

        /*private async Task<string> ReadBodyStream(Stream body)
        {
            if (body.Length == 0)
            {
                return null;
            }

            body.Position = 0;

            using var reader = new StreamReader(body, leaveOpen: true);
            var bodyString = await reader.ReadToEndAsync().ConfigureAwait(false);
            body.Position = 0;

            return bodyString;
        }*/
    }
}