using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ACs.NHibernate.Tests.Core;

namespace ACs.NHibernate.Tests.Mapping
{
    public class UserExampleMap : ClassMap<UserExample>
    {
        public UserExampleMap()
        {
            Table("USER");
            Id(x => x.Id).Column("Id").GeneratedBy.Native();

        }
    }
}
