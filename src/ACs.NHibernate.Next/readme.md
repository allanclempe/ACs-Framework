#Dependencies

ACys Framework NHibernate 1.0
NHibernate 4.0.0.400
FluentNHibernate 2.0.3

#To Use (Asp.net 5 vNext)

startup.cs

```c#
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    //...
    var cfg = Fluently.Configure()
                    .CurrentSessionContext<Web5SessionContext>();

    /*add your database configuration. 
    cfg.Database(MsSqlConfiguration.MsSql2012.ConnectionString("yourconnectionstring")
                    .Driver<SqlClientDriver>())
                    .Mappings(map=>map.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .Mappings(map=>map.HbmMappings.AddFromAssemblyOf<User>());
    */

    //construct databasefactory.
    services.AddSingleton<IDatabaseFactory>(x=> new DatabaseFactory(cfg.BuildSessionFactory());
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    //...
    //configure context at static helper.
    HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
}
```

