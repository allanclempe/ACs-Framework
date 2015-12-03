using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using ACs.NHibernate.Generic;


namespace ACs.NHibernate
{
    public class DatabaseFactory : IDatabaseSession
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


        public ISession Session => GetSession();
        
        public virtual IDatabaseRequest BeginRequest(bool beginTransaction = true)
        {
            if (_sessionFactory == null)
                _sessionFactory = GetConfiguration();

            return new DatabaseRequest(_sessionFactory)
                .Open(beginTransaction);
        }

        public static IDatabaseRequest GetRequest()
        {
            return new DatabaseRequest(_sessionFactory);
        }

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


    }
}
