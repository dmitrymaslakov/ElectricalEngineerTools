namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsExplosionProofProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "IsExplosionProof", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LightingFixtures", "IsExplosionProof");
        }
    }
}
