namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MountingSubtypeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LightingFixtures", "MountingSubtype", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LightingFixtures", "MountingSubtype", c => c.String(nullable: false, unicode: false));
        }
    }
}
