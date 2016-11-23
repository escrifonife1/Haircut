namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnInScheduleTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "LoginId", "dbo.Logins");
            DropIndex("dbo.Schedules", new[] { "LoginId" });
            RenameColumn(table: "dbo.Schedules", name: "LoginId", newName: "Login_Id");
            AddColumn("dbo.Schedules", "Available", c => c.Int(nullable: false));
            AlterColumn("dbo.Schedules", "Login_Id", c => c.Int());
            CreateIndex("dbo.Schedules", "Login_Id");
            AddForeignKey("dbo.Schedules", "Login_Id", "dbo.Logins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Login_Id", "dbo.Logins");
            DropIndex("dbo.Schedules", new[] { "Login_Id" });
            AlterColumn("dbo.Schedules", "Login_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Schedules", "Available");
            RenameColumn(table: "dbo.Schedules", name: "Login_Id", newName: "LoginId");
            CreateIndex("dbo.Schedules", "LoginId");
            AddForeignKey("dbo.Schedules", "LoginId", "dbo.Logins", "Id", cascadeDelete: true);
        }
    }
}
