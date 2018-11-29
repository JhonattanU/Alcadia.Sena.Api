using Alcadia.Sena.Api.Extensions;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Alcadia.Sena.Api.Handlers
{
    public class JsonExceptionHandler
    {
        public async Task Invoke(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception == null) return;

            //Send the exception to telemetry
            var telemetry = new TelemetryClient();
            telemetry.TrackException(exception);

            //Serialize and send the response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(exception.Serialize()).ConfigureAwait(false);
        }
    }
}
