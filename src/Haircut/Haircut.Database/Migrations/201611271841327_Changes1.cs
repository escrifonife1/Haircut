namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hairdressers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BarberShops", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BarberShops", "Name", c => c.String());
            AlterColumn("dbo.Hairdressers", "Name", c => c.String());
        }
    }
}
