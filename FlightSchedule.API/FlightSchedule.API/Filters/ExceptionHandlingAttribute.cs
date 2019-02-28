using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            TelemetryClient client = null;
            try
            {
                client = new TelemetryClient();
                client.TrackException(context.Exception);
                context.ExceptionHandled = true;
            }
            catch (Exception ex)
            {
                context.ExceptionHandled = true;
            }
        }
    }
}
