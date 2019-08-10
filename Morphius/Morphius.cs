using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System;
using System.Net;
using System.Threading.Tasks;

namespace Morphius
{
    public class Morphius
    {
        private readonly RequestDelegate _next;
        private readonly MorphiusOptions _options;

        public Morphius(RequestDelegate next, MorphiusOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await SetResponse(context, _options.GetErrorOrDefault(e), this.CreateErrorResult(e));
            }
        }

        private static async Task SetResponse(HttpContext context, HttpStatusCode statusCode, Fault result)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(result);

            await context.Response.WriteAsync(json);
        }

        public Fault CreateErrorResult(Exception e)
            => new Fault
            {
                Message = e.Message,
                Name = e.GetType().Name,
                StackTrace = e.StackTrace,
                InnerFault = e.InnerException != null ? CreateErrorResult(e.InnerException) : null
            };
    }
}