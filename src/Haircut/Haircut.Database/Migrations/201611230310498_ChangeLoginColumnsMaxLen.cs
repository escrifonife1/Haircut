namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLoginColumnsMaxLen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Logins", "UserName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Logins", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Logins", "Phone", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "Name", c => c.String(nullable: false));
        }
    }
}
