using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentNHibernate.Cfg;
using ACs.NHibernate;
using ACs.NHibernate.Next;
using ACs.Framework.Web;
using ACs.Framework.Web.Map;
using ACs.Framework.Web.Data;
using ACs.Framework.Web.Core;
using FluentNHibernate.Cfg.Db;
using NHibernate.Driver;
using NHibernate;
using ACs.Angular.Next;
using ACs.Net.Mail;
using ACs.Framework.Web.Infra;

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
            services.AddSingleton(x => Fluently.Configure()
                  .CurrentSessionContext<Web5SessionContext>()
                    //sorry about this. But DNX not running SQLite.
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=.\\sqlexpress;Initial Catalog=foo;Connect Timeout=10;Persist Security Info=True;user id=sa;password=123")
                            .Driver<SqlClientDriver>())
                            .Mappings(map => map.FluentMappings.AddFromAssemblyOf<FooMap>())
                            .Mappings(map => map.HbmMappings.AddFromAssemblyOf<Foo>())
                            .BuildSessionFactory());

            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IFooRepository, FooRepository>();
            services.AddSingleton<ISmtpConfiguration>(x=> Configuration.Get<SmtpConfiguration>("Smtp"));
            services.AddSingleton<IMessageSender, MessageSender>();

            // Add framework services.
            services.AddMvc();
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



            //app.UseStaticFiles();

            app
                .UseIISPlatformHandler()
                .UseMvc()
                .UseAngularServer("/index.html")
                .UseStaticContext();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
