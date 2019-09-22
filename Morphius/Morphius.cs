using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

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
                var statusCode = _options.GetErrorOrDefault(e);

                var fault = CreateErrorResult(e, _options.DebugMode);
                
                if (statusCode != HttpStatusCode.OK)
                {
                    await SetResponse(context, statusCode, fault);
                }
                else
                {
                    await SetResponse(context, statusCode, new PostResult
                    {
                        Success = false,
                        Errors = new List<Fault> { fault }
                    });
                }
            }
        }

        private static async Task SetResponse(HttpContext context, HttpStatusCode statusCode, object result)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            await context.Response.WriteAsync(json);
        }

        public Fault CreateErrorResult(Exception e, bool debugMode = false)
            => new Fault
            {
                Message = e.Message,
                Name = debugMode ? e.GetType().Name : null,
                StackTrace = debugMode ? e.StackTrace : null,
                InnerFault = debugMode ? e.InnerException != null ? CreateErrorResult(e.InnerException) : null : null
            };
    }
}