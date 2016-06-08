using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentNHibernate.Cfg;
using ACs.NHibernate;
using ACs.NHibernate.Next;
using ACs.Framework.Web.Map;
using ACs.Framework.Web.Data;
using ACs.Framework.Web.Core;
using FluentNHibernate.Cfg.Db;
using NHibernate.Driver;
using ACs.Angular.Next;
using ACs.ErrorHandler.Next;
using ACs.Framework.Web.Core.Events;
using ACs.Framework.Web.Core.Infra;
using ACs.Net.Mail;
using ACs.Security.Jwt;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;

namespace ACs.Framework.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddUserSecrets();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(x => DatabaseFactory.BuildSessionFactory(Configuration.GetSection("NHibernate").ToDictionary()))
                .AddSingleton<IJwtTokenProvider>(x => new JwtTokenProvider(Configuration.Get<IJwtTokenConfiguration>("Security")))
                .AddSingleton<ISmtpConfiguration>(x => Configuration.Get<SmtpConfiguration>("Smtp"))
                .AddSingleton<ISystemConfiguration, SystemConfiguration>()
                .AddSingleton<IMessageSender, MessageSender>()
                .AddScoped<IDatabaseFactory, DatabaseFactory>()
                //events
                .AddScoped<IUserEvent, UserEvent>()
                .AddScoped<IUserEvent, UserEvent>()
                //repo
                .AddScoped<IUserRepository, UserRepository>()
                ;

            // Add framework services.
            services.AddMvc(options=> options.Filters.Add(new ErrorHandleFilter<SystemLogicException>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseMiddleware<JwtMiddleware500to401Error>()
                .UseJwtBearerAuthentication(config =>
                {
                    var provider = app.ApplicationServices.GetService<IJwtTokenProvider>();
                    config.AutomaticAuthenticate = true;
                    config.TokenValidationParameters.IssuerSigningKey = provider.Key;
                    config.TokenValidationParameters.ValidAudience = provider.Audience;
                    config.TokenValidationParameters.ValidIssuer = provider.Issuer;
                })
                .UseIISPlatformHandler()
                .UseStaticContext()

                .UseStaticFiles(new StaticFileOptions
                {
                    ServeUnknownFileTypes = true,
                    DefaultContentType = "image/png"
                })
                .UseMvc()
                .UseAngularServer("/index.html");


        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
