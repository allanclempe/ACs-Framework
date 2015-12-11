using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using ACs.NHibernate.Generic;
using System.Linq;


namespace ACs.NHibernate
{
    public class DatabaseFactory : IDatabaseFactory, IDisposable
    {
        private readonly IDictionary<string, string> _configuration;
        private static ISessionFactory _sessionFactory;

        public DatabaseFactory(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public DatabaseFactory(IDictionary<string, string> configuration)
        {
            _configuration = configuration;
        }
        
        public virtual IDatabaseRequest BeginRequest(bool beginTransaction = true)
        {
            if (_sessionFactory == null)
                _sessionFactory = GetConfiguration();

            ISession session = Session;

            if (session == null)
            {
                session = _sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return new DatabaseRequest(session)
                .Open(beginTransaction);
        }

        public virtual void End()
        {

            if (Session == null)
                return;

            var session = Session;

            CurrentSessionContext.Unbind(_sessionFactory);

            if (session.IsConnected)
                session.Disconnect();

            if (session.IsOpen)
                session.Close();

            session.Dispose();
        }

        public ISession Session => GetSession();
        public static ISession GetSession()
        {
            return !CurrentSessionContext.HasBind(_sessionFactory) ? null : _sessionFactory.GetCurrentSession();
        }

        protected Assembly GetAssembly(string name)
        {
            var type = Type.GetType(name);

            if (type == null)
                throw new Exception(
                    $"Cannot find assembly name {name}. Please, configure correctly mappingfluent onto config file.");

            return type.Assembly;

        }

        protected Assembly MapAssembly => GetAssembly(_configuration["mappingfluent"]);

        protected virtual ISessionFactory GetConfiguration()
        {
            return Fluently.Configure(new Configuration().AddProperties(_configuration))
                .Mappings(m => m.FluentMappings.AddFromAssembly(MapAssembly)
                    .Conventions.Add(DefaultLazy.Always()))
                .BuildSessionFactory();
        }

        public void Dispose()
        {
            End();
        }
    }
}
