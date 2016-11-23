namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableSchedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        LoginId = c.Int(nullable: false),                        
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logins", t => t.LoginId, cascadeDelete: true)
                .Index(t => t.LoginId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "LoginId", "dbo.Logins");
            DropIndex("dbo.Schedules", new[] { "LoginId" });
            DropTable("dbo.Schedules");
        }
    }
}
