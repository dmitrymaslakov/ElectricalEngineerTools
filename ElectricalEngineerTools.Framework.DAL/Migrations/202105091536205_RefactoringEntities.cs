namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClimateApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiffuserMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dimensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Length = c.Double(),
                        Width = c.Double(),
                        Diameter = c.Double(),
                        LengthOnDwg = c.Double(),
                        WidthOnDwg = c.Double(),
                        DiameterOnDwg = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LightSourceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Power = c.String(nullable: false, unicode: false),
                        LightSourceType = c.String(nullable: false, unicode: false),
                        Socle = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mountings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MountingType = c.String(nullable: false, unicode: false),
                        MountingSubtype = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechnicalSpecifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LightingFixtures", "TechnicalSpecificationsId", c => c.Int());
            AddColumn("dbo.LightingFixtures", "MountingId", c => c.Int(nullable: false));
            AddColumn("dbo.LightingFixtures", "ClimateApplicationId", c => c.Int(nullable: false));
            AddColumn("dbo.LightingFixtures", "DiffuserMaterialId", c => c.Int(nullable: false));
            AddColumn("dbo.LightingFixtures", "LightSourceInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.LightingFixtures", "DimensionsId", c => c.Int(nullable: false));
            CreateIndex("dbo.LightingFixtures", "TechnicalSpecificationsId");
            CreateIndex("dbo.LightingFixtures", "MountingId");
            CreateIndex("dbo.LightingFixtures", "ClimateApplicationId");
            CreateIndex("dbo.LightingFixtures", "DiffuserMaterialId");
            CreateIndex("dbo.LightingFixtures", "LightSourceInfoId");
            CreateIndex("dbo.LightingFixtures", "DimensionsId");
            AddForeignKey("dbo.LightingFixtures", "ClimateApplicationId", "dbo.ClimateApplications", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "DiffuserMaterialId", "dbo.DiffuserMaterials", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "DimensionsId", "dbo.Dimensions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "LightSourceInfoId", "dbo.LightSourceInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "MountingId", "dbo.Mountings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "TechnicalSpecificationsId", "dbo.TechnicalSpecifications", "Id");
            DropColumn("dbo.LightingFixtures", "MountingType");
            DropColumn("dbo.LightingFixtures", "MountingSubtype");
            DropColumn("dbo.LightingFixtures", "Power");
            DropColumn("dbo.LightingFixtures", "ClimaticModification");
            DropColumn("dbo.LightingFixtures", "DiffuserMaterial");
            DropColumn("dbo.LightingFixtures", "LightSource");
            DropColumn("dbo.LightingFixtures", "LightSourceType");
            DropColumn("dbo.LightingFixtures", "Length");
            DropColumn("dbo.LightingFixtures", "Width");
            DropColumn("dbo.LightingFixtures", "Diameter");
            DropColumn("dbo.LightingFixtures", "LengthOnDwg");
            DropColumn("dbo.LightingFixtures", "WidthOnDwg");
            DropColumn("dbo.LightingFixtures", "DiameterOnDwg");
            DropColumn("dbo.LightingFixtures", "TechnicalSpecifications");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightingFixtures", "TechnicalSpecifications", c => c.String(unicode: false));
            AddColumn("dbo.LightingFixtures", "DiameterOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "WidthOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "LengthOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "Diameter", c => c.Double());
            AddColumn("dbo.LightingFixtures", "Width", c => c.Double());
            AddColumn("dbo.LightingFixtures", "Length", c => c.Double());
            AddColumn("dbo.LightingFixtures", "LightSourceType", c => c.String(unicode: false));
            AddColumn("dbo.LightingFixtures", "LightSource", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "DiffuserMaterial", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "ClimaticModification", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "Power", c => c.Double(nullable: false));
            AddColumn("dbo.LightingFixtures", "MountingSubtype", c => c.String(unicode: false));
            AddColumn("dbo.LightingFixtures", "MountingType", c => c.String(nullable: false, unicode: false));
            DropForeignKey("dbo.LightingFixtures", "TechnicalSpecificationsId", "dbo.TechnicalSpecifications");
            DropForeignKey("dbo.LightingFixtures", "MountingId", "dbo.Mountings");
            DropForeignKey("dbo.LightingFixtures", "LightSourceInfoId", "dbo.LightSourceInfoes");
            DropForeignKey("dbo.LightingFixtures", "DimensionsId", "dbo.Dimensions");
            DropForeignKey("dbo.LightingFixtures", "DiffuserMaterialId", "dbo.DiffuserMaterials");
            DropForeignKey("dbo.LightingFixtures", "ClimateApplicationId", "dbo.ClimateApplications");
            DropIndex("dbo.LightingFixtures", new[] { "DimensionsId" });
            DropIndex("dbo.LightingFixtures", new[] { "LightSourceInfoId" });
            DropIndex("dbo.LightingFixtures", new[] { "DiffuserMaterialId" });
            DropIndex("dbo.LightingFixtures", new[] { "ClimateApplicationId" });
            DropIndex("dbo.LightingFixtures", new[] { "MountingId" });
            DropIndex("dbo.LightingFixtures", new[] { "TechnicalSpecificationsId" });
            DropColumn("dbo.LightingFixtures", "DimensionsId");
            DropColumn("dbo.LightingFixtures", "LightSourceInfoId");
            DropColumn("dbo.LightingFixtures", "DiffuserMaterialId");
            DropColumn("dbo.LightingFixtures", "ClimateApplicationId");
            DropColumn("dbo.LightingFixtures", "MountingId");
            DropColumn("dbo.LightingFixtures", "TechnicalSpecificationsId");
            DropTable("dbo.TechnicalSpecifications");
            DropTable("dbo.Mountings");
            DropTable("dbo.LightSourceInfoes");
            DropTable("dbo.Dimensions");
            DropTable("dbo.DiffuserMaterials");
            DropTable("dbo.ClimateApplications");
        }
    }
}
