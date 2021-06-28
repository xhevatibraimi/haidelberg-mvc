using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Infrastructure.Middlewares
{
    public class CorrelationIdMiddelware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //do our job
            string correlationId;
            if (context.Request.Headers.ContainsKey("X-Correlation-Id"))
            {
                correlationId = context.Request.Headers["X-Correlation-Id"];
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
            }

            context.Response.Headers.Add("X-Correlation-Id", correlationId);
            context.TraceIdentifier = correlationId;
            //call next middleware
            await next(context);
        }
    }
}
