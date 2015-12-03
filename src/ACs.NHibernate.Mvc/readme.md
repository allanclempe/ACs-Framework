#Dependencies

ACys Framework NHibernate 1.0
NHibernate 4.0.4
FluentNHibernate 2.0.3
Unity 4.0
Unity Mvc 4.0

#To Use (Asp.net Mvc 5.2.3)

global.asax.cs

```c#
var container = new UnityContainer();
var cfg = Fluently.Configure()
	.CurrentSessionContext<WebSessionContext>();

 /*add your database configuration. 
    cfg.Database(MsSqlConfiguration.MsSql2012.ConnectionString("yourconnectionstring")
                    .Driver<SqlClientDriver>())
                    .Mappings(map=>map.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .Mappings(map=>map.HbmMappings.AddFromAssemblyOf<User>());
*/

container.RegisterInstance<IDatabaseFactory>(new DatabaseFactory(cfg.BuildSessionFactory()));

//Register container on UnityContainerHelper.
UnityContainerHelper.Configure(container);
DependencyResolver.SetResolver(new UnityDependencyResolver(container));

```

You can use SessionRequired attribute on controller methods or configure http module on web.config to open session by request, see bellow:

```xml
<system.web>
	<httpModules>
		<add name="DatabaseFactoryHttpModule" type="ACys.Framework.NHibernate.Mvc.DatabaseFactoryHttpModule, ACys.Framework.NHibernate.Mvc, Version=1.0.0.0, Culture=neutral" />
	</httpModules>
</system.web>
<system.webServer>
	<validation validateIntegratedModeConfiguration="false"/>
	<modules>
		<remove name="DatabaseFactoryHttpModule" />
		<add name="DatabaseFactoryHttpModule" type="ACys.Framework.NHibernate.Mvc.DatabaseFactoryHttpModule, ACys.Framework.NHibernate.Mvc, Version=1.0.0.0, Culture=neutral" />
	</modules>
</system.webServer>
```

