namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnLoginIdInTableSchedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "Login_Id", "dbo.Logins");
            DropIndex("dbo.Schedules", new[] { "Login_Id" });
            RenameColumn(table: "dbo.Schedules", name: "Login_Id", newName: "LoginId");
            AlterColumn("dbo.Schedules", "LoginId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "LoginId");
            AddForeignKey("dbo.Schedules", "LoginId", "dbo.Logins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "LoginId", "dbo.Logins");
            DropIndex("dbo.Schedules", new[] { "LoginId" });
            AlterColumn("dbo.Schedules", "LoginId", c => c.Int());
            RenameColumn(table: "dbo.Schedules", name: "LoginId", newName: "Login_Id");
            CreateIndex("dbo.Schedules", "Login_Id");
            AddForeignKey("dbo.Schedules", "Login_Id", "dbo.Logins", "Id");
        }
    }
}
