namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMountingSubtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "MountingType", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.LightingFixtures", "MountingSubtype", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.LightingFixtures", "Mounting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LightingFixtures", "Mounting", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.LightingFixtures", "MountingSubtype");
            DropColumn("dbo.LightingFixtures", "MountingType");
        }
    }
}
