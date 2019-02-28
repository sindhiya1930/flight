using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace FlightSchedule.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class AuditFilterAttribute : ActionFilterAttribute
    {
        TelemetryClient client;
        public AuditFilterAttribute()
        {
            client = new TelemetryClient();
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string data = JsonConvert.SerializeObject(context.Result, Formatting.Indented);

            client.TrackTrace("Audit-Out", new Dictionary<string, string>() {
                        { "Url",context.HttpContext.Request.Path.Value},
                        { "Method",context.HttpContext.Request.Method},
                        { "Data",data}
                });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethod.Get.Method)
            {
                client.TrackTrace("Audit-In", new Dictionary<string, string>() {
                        { "Url",context.HttpContext.Request.Path.Value},
                        { "Method",context.HttpContext.Request.Method},
                        { "Data","Get Call"} });
            }
            else
            {
                var logStream = context.HttpContext.Request.Body;
                if (logStream != null && logStream.Length > 0)
                {
                    var data = new byte[logStream.Length];
                    logStream.Read(data, 0, data.Length);
                    logStream.Position = 0;
                    if (data != null && data.Length > 0)
                    {
                        var telemetryData = Encoding.Default.GetString(data);

                        client.TrackTrace("Audit-In", new Dictionary<string, string>() {
                        { "Url",context.HttpContext.Request.Path.Value},
                        { "Method",context.HttpContext.Request.Method},
                        { "Data",telemetryData }
                    });
                    }
                }
            }
        }
    }
}
