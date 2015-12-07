using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Angular.Next
{
    public class AngularServerMiddleware
    {
        private readonly AngularServerOptions _options;
        private readonly RequestDelegate _next;
        private readonly StaticFileMiddleware _innerMiddleware;

        public AngularServerMiddleware(RequestDelegate next, IHostingEnvironment env, ILoggerFactory loggerFactory, AngularServerOptions options)
        {
            _next = next;
            _options = options;

            _innerMiddleware = new StaticFileMiddleware(next, env, options.FileServerOptions.StaticFileOptions, loggerFactory);
        }

        public async Task Invoke(HttpContext context)
        {
            // try to resolve the request with default static file middleware
            await _innerMiddleware.Invoke(context);
            Console.WriteLine(context.Request.Path + ": " + context.Response.StatusCode);
            // route to root path if the status code is 404
            // and need support angular html5mode
            if (context.Response.StatusCode == 404 && _options.Html5Mode)
            {
                context.Request.Path = _options.EntryPath;
                await _innerMiddleware.Invoke(context);
                Console.WriteLine(">> " + context.Request.Path + ": " + context.Response.StatusCode);
            }
        }
    }
}
