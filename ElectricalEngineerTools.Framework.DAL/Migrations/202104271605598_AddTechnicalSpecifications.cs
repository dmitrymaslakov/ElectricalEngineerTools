namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTechnicalSpecifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "Diameter", c => c.Double());
            AddColumn("dbo.LightingFixtures", "LengthOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "WidthOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "DiameterOnDwg", c => c.Double());
            AddColumn("dbo.LightingFixtures", "TechnicalSpecifications", c => c.String(unicode: false));
            DropColumn("dbo.LightingFixtures", "DwgFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightingFixtures", "DwgFile", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.LightingFixtures", "TechnicalSpecifications");
            DropColumn("dbo.LightingFixtures", "DiameterOnDwg");
            DropColumn("dbo.LightingFixtures", "WidthOnDwg");
            DropColumn("dbo.LightingFixtures", "LengthOnDwg");
            DropColumn("dbo.LightingFixtures", "Diameter");
        }
    }
}
