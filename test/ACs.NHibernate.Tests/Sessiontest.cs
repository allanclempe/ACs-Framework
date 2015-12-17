using Xunit;
using FluentNHibernate.Cfg;
using ACs.NHibernate.Next;
using FluentNHibernate.Cfg.Db;
using NHibernate.Driver;
using ACs.NHibernate.Tests.Mapping;
using ACs.NHibernate.Tests.Core;
using Microsoft.AspNet.Http;
using Moq;
using Microsoft.AspNet.Http.Internal;
using ACs.NHibernate;

namespace ACs.NHibernate.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SessionTest
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IDatabaseFactory _factory;

        public SessionTest()
        {
            
            var accessor = new Mock<IHttpContextAccessor>();
            var defaultContext = new DefaultHttpContext();
            accessor.SetupGet(x => x.HttpContext).Returns(defaultContext);
            _httpContext = accessor.Object;

            HttpContextHelper.UseStaticContext(accessor.Object);

            _factory = new DatabaseFactory(Fluently.Configure()
                  .CurrentSessionContext<Web5SessionContext>()
                  //sorry about this. But DNX not running SQLite.
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=.\\sqlexpress;Initial Catalog=TireGauge;Connect Timeout=10;Persist Security Info=True;user id=sa;password=123")
                            .Driver<SqlClientDriver>())
                            .Mappings(map => map.FluentMappings.AddFromAssemblyOf<UserExampleMap>())
                            .Mappings(map => map.HbmMappings.AddFromAssemblyOf<UserExample>())
                            .BuildSessionFactory());
        }

        [Fact]
        public void BeginRequest()
        {
            using (var req1 = _factory.BeginRequest()) {
              
                //another logic.
                req1.CommitTransaction();
            }
                
            Assert.True(_factory.Session.IsConnected);
            Assert.True(_factory.Session.IsOpen);

            _factory.End();

            Assert.Null(_factory.Session);

        }
    }
}
