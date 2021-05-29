namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManufacturerEntity : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LightingFixtures", "ManufacturerId", c => c.Int(nullable: false));
            CreateIndex("dbo.LightingFixtures", "ManufacturerId");*/
            AddForeignKey("dbo.LightingFixtures", "ManufacturerId", "dbo.Manufacturers", "Id", cascadeDelete: true);
            DropColumn("dbo.LightingFixtures", "Manufacturer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightingFixtures", "Manufacturer", c => c.String(unicode: false));
            DropForeignKey("dbo.LightingFixtures", "Manufacturer_Id", "dbo.Manufacturers");
            DropIndex("dbo.LightingFixtures", new[] { "Manufacturer_Id" });
            DropColumn("dbo.LightingFixtures", "Manufacturer_Id");
            DropTable("dbo.Manufacturers");
        }
    }
}
