using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ACs.Framework.Web.Core;

namespace ACs.Framework.Web.Map
{
    public class FooMap : ClassMap<Foo>
    {
        public FooMap()
        {
            Table("Foo");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name);

        }
    }
}
