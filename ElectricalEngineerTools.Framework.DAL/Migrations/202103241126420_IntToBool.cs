namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntToBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LightingFixtures", "IsFireproof", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LightingFixtures", "BPSU", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LightingFixtures", "BPSU", c => c.Int(nullable: false));
            AlterColumn("dbo.LightingFixtures", "IsFireproof", c => c.Int(nullable: false));
        }
    }
}
