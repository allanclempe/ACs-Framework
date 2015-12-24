using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACs.Framework.Web.Migrations
{
    [Migration(1)]
    public class Migration1 : Migration
    {

        public override void Up()
        {
            Delete.Column("Url").FromTable("Foo");
            Alter.Table("Foo").AddColumn("Email").AsString(255).NotNullable();
            
            Insert.IntoTable("Foo")
                .Row(new { Name = "Something 1", Email = "user1@domain.com.br" });

            Insert.IntoTable("Foo")
                .Row(new { Name = "Something 2", Email = "user2@domain.com.br" });
        }

        public override void Down()
        {
            Delete.FromTable("Foo").Row(new { Name = "Something 1" });
            Delete.FromTable("Foo").Row(new { Name = "Something 2" });
            Delete.Column("Email").FromTable("Foo");
            Alter.Table("Foo").AddColumn("Url").AsString(255).NotNullable();

        }

    }
}
