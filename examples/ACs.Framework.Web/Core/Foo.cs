using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACs.NHibernate.Generic;
namespace ACs.Framework.Web.Core
{
    public class Foo  : IEntityId, IEntityRoot
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }


    }

}
