using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ACs.Angular.Next
{
    public static class AngularServerExtension
    {
        public static IApplicationBuilder UseAngularServer(this IApplicationBuilder builder, string rootPath, string entryPath)
        {
            var logger  = (ILoggerFactory)builder.ApplicationServices.GetService(typeof(ILoggerFactory));
            var env = (IHostingEnvironment)builder.ApplicationServices.GetService(typeof(IHostingEnvironment));

            var dir = Directory.GetCurrentDirectory();

            if (dir.IndexOf(rootPath, StringComparison.CurrentCultureIgnoreCase) == -1)
                dir = Path.Combine(dir, rootPath);

            var fileProvider = new PhysicalFileProvider(dir);

            var options = new AngularServerOptions {
                FileServerOptions = new FileServerOptions()
                {
                    EnableDirectoryBrowsing = false,
                    FileProvider = fileProvider
                },
                EntryPath = new PathString(entryPath)
            };

            builder.UseDefaultFiles(options.FileServerOptions.DefaultFilesOptions);

            return builder.Use(next => new AngularServerMiddleware(next, env, logger, options).Invoke);
        }
    }
}
