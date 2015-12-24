using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Framework.Web.Migrations
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [Migration(0)]
    public class Migration0 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Foo")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(60)
                .WithColumn("Url").AsString(255);
                
        }
    }
}
