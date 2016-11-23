namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginAddColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Logins", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Logins", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "Phone");
            DropColumn("dbo.Logins", "Name");
            DropColumn("dbo.Logins", "Created");
        }
    }
}
