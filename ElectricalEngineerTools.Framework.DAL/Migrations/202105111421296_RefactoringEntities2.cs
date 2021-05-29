namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringEntities2 : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.EquipmentClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngressProtections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LightingFixtures", "IPId", c => c.Int(nullable: false));
            AddColumn("dbo.LightingFixtures", "EquipmentClassId", c => c.Int(nullable: false));
            AddColumn("dbo.LightSourceInfoes", "LampsNumber", c => c.Int());
            CreateIndex("dbo.LightingFixtures", "IPId");
            CreateIndex("dbo.LightingFixtures", "EquipmentClassId");*/
            AddForeignKey("dbo.LightingFixtures", "EquipmentClassId", "dbo.EquipmentClasses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "IPId", "dbo.IngressProtections", "Id", cascadeDelete: true);
            DropColumn("dbo.LightingFixtures", "LampsNumber");
            DropColumn("dbo.LightingFixtures", "IP");
            DropColumn("dbo.LightingFixtures", "EquipmentClass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightingFixtures", "EquipmentClass", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "IP", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "LampsNumber", c => c.Int());
            DropForeignKey("dbo.LightingFixtures", "IPId", "dbo.IngressProtections");
            DropForeignKey("dbo.LightingFixtures", "EquipmentClassId", "dbo.EquipmentClasses");
            DropIndex("dbo.LightingFixtures", new[] { "EquipmentClassId" });
            DropIndex("dbo.LightingFixtures", new[] { "IPId" });
            DropColumn("dbo.LightSourceInfoes", "LampsNumber");
            DropColumn("dbo.LightingFixtures", "EquipmentClassId");
            DropColumn("dbo.LightingFixtures", "IPId");
            DropTable("dbo.IngressProtections");
            DropTable("dbo.EquipmentClasses");
        }
    }
}
