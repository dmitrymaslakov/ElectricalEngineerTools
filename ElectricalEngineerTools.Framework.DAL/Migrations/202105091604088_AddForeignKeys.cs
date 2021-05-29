namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeys : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.LightingFixtures", "ClimateApplicationId", "dbo.ClimateApplications", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LightingFixtures", "TechSpecificationsId", "dbo.TechSpecifications", "Id");
        }

        public override void Down()
        {
        }
    }
}
