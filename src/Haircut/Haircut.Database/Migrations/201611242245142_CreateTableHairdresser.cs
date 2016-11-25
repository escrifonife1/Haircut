namespace Haircut.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableHairdresser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hairdressers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BarberShopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BarberShops", t => t.BarberShopId, cascadeDelete: true)
                .Index(t => t.BarberShopId);
            
            CreateTable(
                "dbo.BarberShops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Schedules", "HairdresserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "HairdresserId");
            AddForeignKey("dbo.Schedules", "HairdresserId", "dbo.Hairdressers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "HairdresserId", "dbo.Hairdressers");
            DropForeignKey("dbo.Hairdressers", "BarberShopId", "dbo.BarberShops");
            DropIndex("dbo.Schedules", new[] { "HairdresserId" });
            DropIndex("dbo.Hairdressers", new[] { "BarberShopId" });
            DropColumn("dbo.Schedules", "HairdresserId");
            DropTable("dbo.BarberShops");
            DropTable("dbo.Hairdressers");
        }
    }
}
