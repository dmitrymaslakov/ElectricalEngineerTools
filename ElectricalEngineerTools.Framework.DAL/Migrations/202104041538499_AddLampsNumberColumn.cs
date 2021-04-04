namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLampsNumberColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "LampsNumber", c => c.Int());
            AlterColumn("dbo.LightingFixtures", "Length", c => c.Double());
            AlterColumn("dbo.LightingFixtures", "Width", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LightingFixtures", "Width", c => c.Double(nullable: false));
            AlterColumn("dbo.LightingFixtures", "Length", c => c.Double(nullable: false));
            DropColumn("dbo.LightingFixtures", "LampsNumber");
        }
    }
}
