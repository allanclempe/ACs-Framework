using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.NHibernate;
using ACs.Framework.Web.Core;
using NHibernate.Linq;

namespace ACs.Framework.Web.Data
{
    public class FooRepository  : Repository<Foo>, IFooRepository
    {
        public FooRepository(IDatabaseFactory factory)
            :base(factory)
        {
                
        }

        public IList<Foo> GetAll()
        {
            return (from foo in _factory.Session.Query<Foo>() select foo).ToList();
        }

    }
}
