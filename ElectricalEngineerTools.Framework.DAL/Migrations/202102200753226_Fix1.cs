namespace ElectricalEngineerTools.Framework.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LightingFixtures", "CableId", c => c.Guid());
            CreateIndex("dbo.LightingFixtures", "CableId");
            AddForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LightingFixtures", "CableId", "dbo.Cables");
            DropIndex("dbo.LightingFixtures", new[] { "CableId" });
            DropColumn("dbo.LightingFixtures", "CableId");
        }
    }
}
