using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.NHibernate.Generic;
using ACs.Framework.Web.Core;

namespace ACs.Framework.Web.Data
{
    public interface IFooRepository : IRepository<Foo>
    {
        IList<Foo> GetAll();

    }
}
